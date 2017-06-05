using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FileHelpers
    // ReSharper restore CheckNamespace
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class FieldTitleAttribute : Attribute {
        public FieldTitleAttribute(string name) {
            if (name == null) throw new ArgumentNullException("name");
            Name = name;
        }

        public string Name { get; private set; }
    }
}
