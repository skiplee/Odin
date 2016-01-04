using System.IO;
using Odin;
using Odin.Chores;

namespace Odin.Sample.Tasks
{
    internal class MyChores : ChoreRunner
    {
        private Inputs _inputs;

        public MyChores() : base()
        {
            _inputs = new Inputs();
        }

        [Chore]
        public void Save()
        {
            using (var file = File.OpenWrite($"{_inputs.InputString}.txt"))
            {
                var writer = new StreamWriter(file);
                writer.WriteLine(_inputs.InputString);
            }

        }
        [Chore]
        public void FlipFlop()
        {
            using (var file = File.OpenWrite($"{_inputs.InputString}.txt"))
            {
                var writer = new StreamWriter(file);
                writer.WriteLine(_inputs.InputString);
            }

        }
        [Chore]
        public void Reverse()
        {
            using (var file = File.OpenWrite($"{_inputs.InputString}.txt"))
            {
                var writer = new StreamWriter(file);
                writer.WriteLine(_inputs.InputString);
            }
        }

        [Parameter]
        public string Value { get; set; }
    }
}