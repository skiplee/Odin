using System.Collections.Generic;
using System.Reflection;

namespace Odin.Chores
{
    internal class ChoreRunnerInvocation
    {
        public ChoreRunnerInvocation()
        {
            Chores = new List<MethodInfo>();
            Parameters = new Dictionary<PropertyInfo, string>();
        }

        internal List<MethodInfo> Chores { get; private set; }
        internal Dictionary<PropertyInfo, string> Parameters { get; private set; }
    }
}