using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;

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

            this.Description = GetDescription();
        }



        private Dictionary<string, MethodInfo> GetChoreMethods()
        {
            return this
                .GetType()
                .GetMethods()
                .Where(m => m.GetCustomAttribute<ChoreAttribute>() != null)
                .ToDictionary(m => m.Name);
        }

        private Dictionary<string, PropertyInfo> GetParameterProperties()
        {
            return this
                .GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttribute<ParameterAttribute>() != null)
                .ToDictionary(p => p.Name);
        }


        private string GetDescription()
        {
            var attribute = this.GetType().GetCustomAttribute<DescriptionAttribute>(inherit:true);
            return attribute != null ? attribute.Description : this.Name;
        }

        public string Name { get; private set; }

        protected virtual void RegisterChores()
        {

        }

        public virtual int Execute(params string[] args)
        {
            
            var invocation = this.GenerateInvocation(args);
            if (invocation != null && invocation.CanInvoke())
            {
                invocation.Invoke();
                return 0;
            }
            else
            {
                this.Logger.Error("Unrecognized command sequence: {0}", string.Join(" ", args));
                this.Help();
                return -1;
            }
        }

        private ChoreRunnerInvocation GenerateInvocation(string[] args)
        {
            var allChores = this.GetChoreMethods();
            var allParameters = this.GetParameterProperties();

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
                    continue;
                }
                throw new ChoreRunnerException("Invalid argument provided");
            }
            ValidateArguments(argumentInfos);

            var invocation = new ChoreRunnerInvocation(this);
            var argumentChores = argumentInfos.Where(a => a.ArgumentType == ArgumentTypeEnum.Chore);
            invocation.Chores.AddRange(argumentChores.Select(a => a.Chore));
            var argumentParameters = argumentInfos.Where(a => a.ArgumentType == ArgumentTypeEnum.ParameterName);
            argumentParameters.ToList().ForEach(p => invocation.Parameters.Add(p.Name, p.ParameterValue));
            
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
            builder.AppendLine(this.Description);

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
            builder.AppendFormat("\t{0} Help <action>", this.Name)
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
            builder.AppendFormat("\t{0} <subcommand> Help", this.Name);
        }

        [Action]
        public void Help(
            [Description("The name of the action to provide help for.")]
            string actionName = "")
        {
            var help = this.GenerateHelp(actionName);
            this.Logger.Info(help);
        }
    }

    public class ChoreRunnerException : Exception
    {
        public ChoreRunnerException(string message) : base(message)
        {
        }
    }

    internal class ArgumentInfo
    {
        public ArgumentInfo(string arg)
        {
            this.Name = arg;
        }

        public MethodInfo Chore { get; set; }

        public string ParameterValue { get; set; }
        public ArgumentTypeEnum ArgumentType { get; set; }
        public string Name { get; set; }
    }

    internal enum ArgumentTypeEnum
    {
        Chore,
        ParameterName,
        ParameterValue,
    }
}