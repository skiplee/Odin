using System.Reflection;

namespace Odin.Chores
{
    internal class ArgumentInfo
    {
        public ArgumentInfo(string arg)
        {
            Name = arg;
        }

        public MethodInfo Chore { get; set; }
        public PropertyInfo Parameter { get; set; }
        public string ParameterValue { get; set; }
        public ArgumentTypeEnum ArgumentType { get; set; }
        public string Name { get; set; }
    }
}