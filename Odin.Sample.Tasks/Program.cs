using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odin.Sample.Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = new TaskCommand();
            command.Execute(args);
        }
    }
}
