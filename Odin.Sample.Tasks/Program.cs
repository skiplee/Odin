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
            var chores = new MyChores();
            chores.Execute(args);
        }
    }
}
