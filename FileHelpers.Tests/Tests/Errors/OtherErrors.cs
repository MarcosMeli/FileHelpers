using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
    [TestFixture]
    public class OtherErrors
    {
        [Test]
        public void FileNotFound()
        {
            var engine = new FileHelperEngine<SampleType>();
            Assert.Throws<FileNotFoundException>(()
                => engine.ReadFile("No foun343333d this file.txt"));
        }

        [Test]
        public void NullReader()
        {
            var engine = new FileHelperEngine<SampleType>();
            Assert.Throws<ArgumentNullException>(()
                => engine.ReadStream(null));
        }

        [Test]
        public void NullString()
        {
            var engine = new FileHelperEngine<SampleType>();
            engine.ReadString(null);
        }

        [Test]
        public void NullWriter()
        {
            var engine = new FileHelperEngine<SampleType>();
            Assert.Throws<ArgumentNullException>(()
                => engine.WriteStream(null, null));
        }


        [Test]
        public void NullRecords()
        {
            var engine = new FileHelperEngine<SampleType>();
            Assert.Throws<ArgumentNullException>(()
                => engine.WriteStream(new StringWriter(new StringBuilder()), null));
        }


        [Test]
        public void FileNotFound2()
        {
            var engine2 = new FileHelperAsyncEngine<SampleType>();
            Assert.Throws<FileNotFoundException>(()
                => engine2.BeginReadFile("No fouffffnd this file.txt"));
        }

        [Test]
        public void FileNotFound3()
        {
            var engine2 = new FileHelperAsyncEngine<SampleType>();
            Assert.Throws<ArgumentNullException>(()
                => engine2.BeginAppendToFile(null));
        }

        [Test]
        public void NullReader2()
        {
            var engine2 = new FileHelperAsyncEngine<SampleType>();
            Assert.Throws<ArgumentNullException>(()
                => engine2.BeginReadStream(null));
        }

        [Test]
        public void NullString2()
        {
            var engine2 = new FileHelperAsyncEngine<SampleType>();
            engine2.BeginReadString(null);
        }

        [Test]
        public void NullWriter2()
        {
            var engine2 = new FileHelperAsyncEngine<SampleType>();
            Assert.Throws<ArgumentException>(()
                => engine2.BeginWriteStream(null));
        }


        [Test]
        public void NullRecords2()
        {
            var engine2 = new FileHelperAsyncEngine<SampleType>();
            engine2.BeginWriteStream(new StringWriter(new StringBuilder()));

            Assert.Throws<ArgumentNullException>(()
                => engine2.WriteNexts(null));
        }
    }
}