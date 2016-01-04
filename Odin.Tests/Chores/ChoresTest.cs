using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Odin.Chores;

namespace Odin.Tests.Chores
{
    [TestFixture]
    class ChoresTest
    {
        [Test]
        public void InvocationExecutesBasedOnArguments()
        {
            var choreRunner = new TestChores();

            choreRunner.Execute("Chore2");

            Assert.That(choreRunner.Chore1Executed, Is.EqualTo(false));
            Assert.That(choreRunner.Chore2Executed, Is.EqualTo(true));
        }

//        [Test]
//        public void AChoresRequiredParametersAreValidated()
//        {
//            Assert.Inconclusive();
//        }
//
//        [Test]
//        public void IfNoParameterValueIsSpecifiedTheDefaultParameterValueIsUsed()
//        {
//            Assert.Inconclusive();
//        }
//
//        [Test]
//        public void IfChoresFailMinusOneIsReturned()
//        {
//            Assert.Inconclusive();
//        }
//
//        [Test]
//        public void IfNoChoreIsSpecifiedTheDefaultChoreRuns()
//        {
//            Assert.Inconclusive();
//        }
    }
}
