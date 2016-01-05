using System.IO;
using Odin.Chores;

namespace Odin.Sample.Tasks
{
    public class SaveCommand //: Command
    {
        private readonly Inputs _inputs;

        public SaveCommand(Inputs inputs)
        {
            _inputs = inputs;
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
    }
}