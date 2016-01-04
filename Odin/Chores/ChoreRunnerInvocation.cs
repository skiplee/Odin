using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Odin.Chores
{
    internal class ChoreRunnerInvocation
    {
        private ChoreRunner _choreRunner;

        public ChoreRunnerInvocation()
        {
            Chores = new List<MethodInfo>();
            Parameters = new Dictionary<string, string>();
        }

        public ChoreRunnerInvocation(ChoreRunner choreRunner) : this()
        {
            this._choreRunner = choreRunner;
        }

        internal List<MethodInfo> Chores { get; private set; }
        internal Dictionary<string, string> Parameters { get; private set; }

        public bool CanInvoke()
        {
            return true;
            //return this.Parameters.Values.All(p => p.IsValueSet());

        }

        public void Invoke()
        {
            this.Chores.ForEach(c => c.Invoke(this._choreRunner, new object[0]));
        }
    }
}