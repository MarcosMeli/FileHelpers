using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FileHelpers;
using FileHelpers.RunTime;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
    public class ClassFromFile
    {
        [Test]
        public void ReadFile()
        {
            Type t = ClassBuilder.ClassFromSourceFile(TestCommon.GetPath("Classes", "ClassFromFile.cs"));

            var engine = new FileHelperEngine(t);

            DataTable dt = engine.ReadStringAsDT("");
            Assert.AreEqual(0, dt.Rows.Count);

        }
    }
}
