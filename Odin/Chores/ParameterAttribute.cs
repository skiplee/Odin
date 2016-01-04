using System;

namespace Odin.Chores
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ParameterAttribute : System.Attribute
    {
        public bool IsDefault { get; set; }
    }
}