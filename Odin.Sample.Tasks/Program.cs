using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odin.Sample.Tasks
{
    class Program
    {
        static int Main(string[] args)
        {
            var chores = new MyChores();
            return chores.Execute(args);
        }
    }
}
