using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Fields inherited from base classes will be ignored.</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/must_read">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IgnoreInheritedClassAttribute : Attribute
    {
        /// <summary>Fields inherited from base classes will be ignored.</summary>
        public IgnoreInheritedClassAttribute() {}
    }
}