using System.IO;
using Odin;

namespace Odin.Sample.Tasks
{
    internal class TaskCommand : Command
    {
        private Inputs _inputs;

        public TaskCommand()
        {
            _inputs = new Inputs();
            //this.RegisterSubCommand(new SaveCommand(_inputs));
        }

        [Action(IsDefault = true)]
        public void Default(string value)
        {
            _inputs.InputString = value;
        }

        [Action]
        public void Save()
        {
            using (var file = File.OpenWrite($"{_inputs.InputString}.txt"))
            {
                var writer = new StreamWriter(file);
                writer.WriteLine(_inputs.InputString);
            }

        }
    }
}