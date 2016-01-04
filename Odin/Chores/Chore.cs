using System;
using System.Reflection;

namespace Odin.Chores
{
    internal class Chore
    {
        public MethodInfo MethodInfo { get; set; }
        public Object MethodObject { get; set; }
    }
}