using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Odin.Chores;

namespace Odin.Tests.Chores
{
    class TestChores : ChoreRunner
    {
        [Chore]
        public void Chore1()
        {
            Chore1Executed = true;
        }

        public bool Chore1Executed { get; private set; }

        [Chore]
        public void Chore2()
        {
            Chore2Executed = true;
        }

        public bool Chore2Executed { get; private set; }
    }
}
