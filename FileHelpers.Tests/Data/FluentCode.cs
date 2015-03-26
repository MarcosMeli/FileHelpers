using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FileHelpers
{
    public sealed class FluentCode
    {
        private readonly Dictionary<string, FluentNamespace> mNamespaces =
            new Dictionary<string, FluentNamespace>(StringComparer.OrdinalIgnoreCase);

        public FluentNamespace Namespace(string name)
        {
            FluentNamespace res;

            if (!mNamespaces.TryGetValue(name, out res)) {
                res = new FluentNamespace(name);
                mNamespaces.Add(name, res);
            }
            return res;
        }

        public string GenerateCode()
        {
            var res = "";
            foreach (var n in mNamespaces)
                res += n.Value.GenerateCode();

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

        private readonly SortedDictionary<string, FluentClass> mClasses =
            new SortedDictionary<string, FluentClass>(StringComparer.OrdinalIgnoreCase);

        public FluentClass Class(string name)
        {
            FluentClass res = null;

            if (!mClasses.TryGetValue(name, out res)) {
                res = new FluentClass(name, null, 1);
                mClasses.Add(name, res);
            }

            return res;
        }


        public string GenerateCode()
        {
            var res = new StringBuilder();
            res.AppendLine(String.Format("namespace {0}", Name));
            res.AppendLine("{");
            res.Append(String.Join(Environment.NewLine, mClasses.Select(s => s.Value.GenerateCode())));
            res.Append("}");
            return res.ToString();
        }
    }

    /// <summary>
    /// Create a class from a template
    /// </summary>
    public sealed class FluentClass
    {
        public string Name { get; private set; }
        public string Parent { get; private set; }
        public int Level { get; private set; }
        
        public FluentClass(string name, string parent, int level)
        {
            this.Name = name;
            this.Parent = parent;
            this.Level = level;
        }

        private readonly SortedDictionary<string, FluentClass> mClasses =
            new SortedDictionary<string, FluentClass>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Create a class of "name"
        /// </summary>
        /// <param name="name">Name of resulting class</param>
        /// <returns></returns>
        public FluentClass Class(string name, string parent)
        {
            FluentClass res;

            if (!mClasses.TryGetValue(name, out res)) {
                res = new FluentClass(name, parent, this.Level + 1);
                mClasses.Add(name, res);
            }

            return res;
        }

        public string GenerateCode()
        {
            var res = new StringBuilder();
            AddCode(string.Format("public partial class {0}{1}", Name, (string.IsNullOrEmpty(Parent) ? "" : " : " + Parent)), Level, res);
            AddCode("{", Level, res);

            res.Append(String.Join(Environment.NewLine, mClasses.Select(s => s.Value.GenerateCode())));
            res.Append(mCode.ToString());

            AddCode("}", Level, res);
            return res.ToString();
        }

        /// <summary>
        /// Append a line of code to the output
        /// </summary>
        /// <param name="code">Code to add</param>
        public void AddCode(string code, int level)
        {
            AddCode(code, level, mCode);
        }

        private void AddCode(string code, int level, StringBuilder builder)
        {
            builder.AppendLine(string.Format("{0}{1}", new string(' ', level * 4), code));
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
            if (mCode.Length != 0)
            {
                AddCode(null, 0);
            }

            AddCode(string.Format("private static {0} m{1} = new {0}();", type, name), Level + 1);
            AddCode(null, 0);
            AddCode(string.Format("public static {0} {1}", type, name), Level + 1);
            AddCode("{", Level + 1);
            AddCode("get { return m" + name + "; }", Level + 2);
            AddCode("}", Level + 1);
        }

        /// <summary>
        /// Internal result of parsing the data
        /// </summary>
        private readonly StringBuilder mCode = new StringBuilder();
    }
}