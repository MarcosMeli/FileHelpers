using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
  
namespace FileHelpers
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class Joiner : Attribute {
        public string Delimiter;

        /// <summary>Indicates that this class represents a delimited record. </summary>
        /// <param name="delimiter">The separator string used to split the fields of the record.</param>
        public Joiner(string delimiter) {
                this.Delimiter = delimiter;
        }
    }
}
