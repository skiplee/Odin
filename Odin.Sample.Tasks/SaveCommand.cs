using System.IO;

namespace Odin.Sample.Tasks
{
    public class SaveCommand //: Command
    {
        private readonly Inputs _inputs;

        public SaveCommand(Inputs inputs)
        {
            _inputs = inputs;
        }

        [Action(IsDefault = true)]
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