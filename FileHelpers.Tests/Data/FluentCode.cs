using FileHelpers;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>
    /// Simple Generate code interface
    /// </summary>
    public interface ICodeGenerator
    {
        string GenerateCode();

    }


    public sealed class FluentCode
    {
        private Dictionary<string, FluentNamespace> mNamespaces = new Dictionary<string, FluentNamespace>(StringComparer.OrdinalIgnoreCase);

        public FluentNamespace Namespace(string name)
        {
            FluentNamespace res;

            if (!mNamespaces.TryGetValue(name, out res))
            {
                res = new FluentNamespace(name);
                mNamespaces.Add(name, res);
            }
            return res;
        }

        public string GenerateCode()
        {
            var res = "";
            foreach (var n in mNamespaces)
            {
                res += n.Value.GenerateCode();
            }

            return res;
            
        }

    }

    public sealed class FluentNamespace
    {
        public string Name { get; private set; }

        public FluentNamespace(string name)
        {
            this.Name = name;
        }

        private SortedDictionary<string, FluentClass> mClasses = new SortedDictionary<string, FluentClass>(StringComparer.OrdinalIgnoreCase);

        public FluentClass Class(string name)
        {
            FluentClass res = null;

            if (!mClasses.TryGetValue(name, out res))
            {
                res = new FluentClass(name);
                mClasses.Add(name, res);
            }

            return res;
        }


        public string GenerateCode()
        {
            var res = "namespace " + Name
                + Environment.NewLine
                + "{" 
                + Environment.NewLine;

            foreach (var c in mClasses)
            {
                res += c.Value.GenerateCode();
            }
                 
            res += Environment.NewLine +
                    "}";
            return res;
        }

    }

    /// <summary>
    /// Create a class from a template
    /// </summary>
    public sealed class FluentClass
    {
        public string Name { get; private set; }

        public FluentClass(string name)
        {
            this.Name = name;
        }

        private SortedDictionary<string, FluentClass> mClasses = new SortedDictionary<string, FluentClass>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Create a class of "name"
        /// </summary>
        /// <param name="name">Name of resulting class</param>
        /// <returns></returns>
        public FluentClass Class(string name)
        {
            FluentClass res;

            if (!mClasses.TryGetValue(name, out res))
            {
                res = new FluentClass(name);
                mClasses.Add(name, res);
            }

            return res;
        }

        public string GenerateCode()
        {
            var res = "public class " + Name
                + Environment.NewLine
                + "{"
                + Environment.NewLine;

            foreach (var c in mClasses)
            {
                res += c.Value.GenerateCode();
            }

            res += Environment.NewLine +
                mCode.ToString()
                + Environment.NewLine;

            res += Environment.NewLine +
        "}";
            return res;
        }

        /// <summary>
        /// Append a line of code to the output
        /// </summary>
        /// <param name="code">Code to add</param>
        public void AddCode(string code)
        {
            mCode.AppendLine(code);
        }

        /// <summary>
        /// Add a static readonly property with the member property having a prefix of m
        /// </summary>
        /// <param name="type">Type of varaible to create</param>
        /// <param name="name">Name of property</param>
        /// <remarks>
        /// private static String mX = new String();
        /// public static String X
        /// { get { return  mX; } }
        /// </remarks>
        public void AddStaticReadOnlyPropertyWithBackingField(string type, string name)
        {
            mCode.AppendLine("private static " + type + " m" + name + " = new " + type + "();");
            mCode.AppendLine("public static " + type + " " + name);
            mCode.AppendLine("{ get { return  m" + name + "; } }");
        }
        /// <summary>
        /// Internal result of parsing the data
        /// </summary>
        private StringBuilder mCode = new StringBuilder();
    }
}
