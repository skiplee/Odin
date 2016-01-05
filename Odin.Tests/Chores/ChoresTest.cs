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
    class ChoreTests
    {
        [Test]
        public void InvocationExecutesBasedOnArguments()
        {
            var choreRunner = new TestChores();

            choreRunner.Execute("Chore2");

            Assert.That(choreRunner.Chore1Executed, Is.EqualTo(false));
            Assert.That(choreRunner.Chore2Executed, Is.EqualTo(true));
        }
        [Test]
        public void InvocationExecutesMultipleChores()
        {
            var choreRunner = new TestChores();

            choreRunner.Execute("Chore1", "Chore2");

            Assert.That(choreRunner.Chore1Executed, Is.EqualTo(true));
            Assert.That(choreRunner.Chore2Executed, Is.EqualTo(true));
        }

        [Test]
        public void ChoresCanAccessParameters()
        {
            var choreRunner = new TestChores();

            choreRunner.Execute("ChoreUsesParameter", "Parameter1", "p1value");

            Assert.That(choreRunner.Parameter1, Is.EqualTo("p1value"));
            Assert.That(choreRunner.ChoreUsesParameterValue, Is.EqualTo("p1value"));
        }

        [Test]
        public void AllChoresMustHaveNoParametersAndReturnInt()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void AChoresRequiredParametersAreValidated()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void IfNoParameterValueIsSpecifiedTheDefaultParameterValueIsUsed()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void FirstNonZeroChoreResponseIsReturned()
        {
            var choreRunner = new TestChores();
            var result = choreRunner.Execute("Chore1", "ChoreReturns21", "Chore2");
            Assert.That(choreRunner.Chore1Executed, Is.EqualTo(true));
            Assert.That(choreRunner.Chore2Executed, Is.EqualTo(false));
            Assert.That(result, Is.EqualTo(21));
        }

        [Test]
        public void IfChoresThrowUnhandledExceptionZeroIsNotReturned()
        {
            var choreRunner = new TestChores();
            var result = choreRunner.Execute("ChoreThrowsException");
            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void IfNoChoreIsSpecifiedTheDefaultChoreRuns()
        {
            Assert.Inconclusive();
        }
    }
}
