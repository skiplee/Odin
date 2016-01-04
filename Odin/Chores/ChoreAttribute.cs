using System;

namespace Odin.Chores
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ChoreAttribute : System.Attribute
    {
        public bool IsDefault { get; set; }
    }
}