using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Odin.Chores
{
    public abstract class ChoreRunner 
    {
        private Dictionary<string, ActionMap> _actionMaps;
        public Logger Logger { get; private set; }
        public string Description { get; }


        protected ChoreRunner(Logger logger = null, Conventions conventions = null)
        {

            if (logger != null)
                Logger = logger;
            else
            {
                Logger = new Logger();
                Logger.OnInfo += Console.Write;
                Logger.OnWarning += Console.Write;
                Logger.OnError += Console.Error.Write;
            }

            //_conventions = conventions ?? new DefaultConventions();

            Description = GetDescription();
        }



        private Dictionary<string, MethodInfo> GetChoreMethods()
        {
            return GetType()
                .GetMethods()
                .Where(m => m.GetCustomAttribute<ChoreAttribute>() != null)
                .ToDictionary(m => m.Name);
        }

        private Dictionary<string, PropertyInfo> GetParameterProperties()
        {
            return GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttribute<ParameterAttribute>() != null)
                .ToDictionary(p => p.Name);
        }


        private string GetDescription()
        {
            var attribute = GetType().GetCustomAttribute<DescriptionAttribute>(inherit:true);
            return attribute != null ? attribute.Description : Name;
        }

        public string Name { get; private set; }

        protected virtual void RegisterChores()
        {

        }

        public virtual int Execute(params string[] args)
        {
            try
            {
                var invocation = GenerateInvocation(args);
                if (invocation != null && CanInvoke(invocation))
                {
                    var result = Execute(invocation);
                    return result;
                }
                else
                {
                    Logger.Error("Unrecognized chore sequence: {0}", string.Join(" ", args));
                    Help();
                    return -1;
                }
            }
            catch (Exception e)
            {
                var descriptionFormat = new StringBuilder();
                descriptionFormat.Append("Exception thrown running chore sequence: {0}\n");
                descriptionFormat.Append("    Message: {1}\n");
                descriptionFormat.Append("    Source: {2}");
                Logger.Error(descriptionFormat.ToString(), string.Join(" ", args), e.Message, e.Source);
                Help();
                return -1;
            }
        }

        private int Execute(ChoreRunnerInvocation invocation)
        {
            foreach (var parameter in invocation.Parameters)
            {
                 parameter.Key.SetValue(this, parameter.Value);
            }
            foreach (var chore in invocation.Chores)
            {
                var invoked = chore.Invoke(this, new object[0]);
                var result = (int?)invoked ?? 0;
                if (result != 0)
                    return result;
            }
            return 0;
        }
        private static bool CanInvoke(ChoreRunnerInvocation invocation)
        {
            return true;
            //return this.Parameters.Values.All(p => p.IsValueSet());
        }

        private ChoreRunnerInvocation GenerateInvocation(string[] args)
        {
            var allChores = GetChoreMethods();
            var allParameters = GetParameterProperties();

            var argumentInfos = new List<ArgumentInfo>();

            var i = 0;
            while (i < args.Length)
            {
                var arg = args[i];
                var argumentInfo = new ArgumentInfo(arg);
                if (allChores.ContainsKey(arg))
                {
                    argumentInfo.ArgumentType = ArgumentTypeEnum.Chore;
                    argumentInfo.Chore = allChores[arg];
                    argumentInfos.Add(argumentInfo);
                    i++;
                    continue;
                }
                if (allParameters.ContainsKey(arg))
                {
                    argumentInfo.ArgumentType = ArgumentTypeEnum.ParameterName;
                    argumentInfo.Parameter = allParameters[arg];
                    var potentialArgValue = args[i + 1];
                    if ((allChores.ContainsKey(potentialArgValue) || allParameters.ContainsKey(potentialArgValue)))
                    {
                        //Todo: the next argument is not a value to be added, so parameter takes default value
                    }
                    else {
                        argumentInfo.ParameterValue = potentialArgValue;
                        i++;
                    }
                    argumentInfos.Add(argumentInfo);
                    i++;
                    continue;
                }
                throw new ChoreRunnerException("Invalid argument provided");
            }
            ValidateArguments(argumentInfos);

            var invocation = new ChoreRunnerInvocation();
            var argumentChores = argumentInfos.Where(a => a.ArgumentType == ArgumentTypeEnum.Chore);
            invocation.Chores.AddRange(argumentChores.Select(a => a.Chore));
            var argumentParameters = argumentInfos.Where(a => a.ArgumentType == ArgumentTypeEnum.ParameterName);
            argumentParameters.ToList().ForEach(p => invocation.Parameters.Add(p.Parameter, p.ParameterValue));
            
            return invocation;
        }

        private void ValidateArguments(List<ArgumentInfo> argumentInfos)
        {
            // check that all arguments are either Chores, parameterNames, or values
            // check that all values are preceded by a parameterName (eg, '-param CHORE "value for param"' is invalid)
        }

        public virtual string GenerateHelp(string actionName = "")
        {

            if (!string.IsNullOrWhiteSpace(actionName))
            {
                var actionMap = _actionMaps[actionName];
                return actionMap.Help();
            }

            var builder = new StringBuilder();
            builder.AppendLine(Description);

            if (_actionMaps != null && _actionMaps.Any())
                GetMethodsHelp(builder);

            var result = builder.ToString();

            return result;
        }

        private void GetMethodsHelp(StringBuilder builder)
        {
            builder
                .AppendLine()
                .AppendLine()
                .AppendLine("ACTIONS");

            foreach (var method in _actionMaps.Values.OrderBy(m => m.Name))
            {
                var methodHelp = method.Help();;
                builder.AppendLine(methodHelp);
            }

            builder.AppendLine();
            builder.AppendLine("To get help for actions");
            builder.AppendFormat("\t{0} Help <action>", Name)
                .AppendLine();
        }

        private void GetSubCommandsHelp(StringBuilder builder)
        {
            builder
                .AppendLine()
                .AppendLine()
                .AppendLine("SUB COMMANDS");


            builder.AppendLine();
            builder.AppendLine("To get help for subcommands");
            builder.AppendFormat("\t{0} <subcommand> Help", Name);
        }

        [Action]
        public void Help(
            [Description("The name of the action to provide help for.")]
            string actionName = "")
        {
            var help = GenerateHelp(actionName);
            Logger.Info(help);
        }
    }

    public class ChoreRunnerException : Exception
    {
        public ChoreRunnerException(string message) : base(message)
        {
        }
    }

    internal enum ArgumentTypeEnum
    {
        Chore,
        ParameterName,
        ParameterValue,
    }
}