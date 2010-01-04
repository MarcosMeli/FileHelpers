using FileHelpers;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace FileHelpers
{
    public interface ICodeGenerator
    {
        string GenerateCode();

    }
    public sealed class FluentCode
    {
        private SortedDictionary<string, FluentNamespace> mNamespaces = new SortedDictionary<string, FluentNamespace>(StringComparer.OrdinalIgnoreCase);

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

    public sealed class FluentClass
    {
        public string Name { get; private set; }

        public FluentClass(string name)
        {
            this.Name = name;
        }

        private SortedDictionary<string, FluentClass> mClasses = new SortedDictionary<string, FluentClass>(StringComparer.OrdinalIgnoreCase);

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

        public void AddCode(string code)
        {
            mCode.AppendLine(code);
        }

        public void AddStaticReadOnlyPropertyWithBackingField(string type, string name)
        {
            mCode.AppendLine("private static " + type + " m" + name + " = new " + type + "();");
            mCode.AppendLine("public static " + type + " " + name);
            mCode.AppendLine("{ get { return  m" + name + "; } }");
        }

        private StringBuilder mCode = new StringBuilder();
    }
}
