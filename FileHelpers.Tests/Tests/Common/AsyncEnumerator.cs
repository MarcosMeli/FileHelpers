using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
    public class AsyncEnumerator
    {
        [DelimitedRecord(",")]
        public class Record
        {
            public string Id;
            public string Name;
        }

        [Test]
        public void MoreCallsToMoveNext1()
        {
            var engine = new FileHelperAsyncEngine<Record>();
            string src = "first,line\nabc,JohnDoe";
            using (engine.BeginReadString(src))
            {
                var enumerator = (engine as IEnumerable<Record>).GetEnumerator();
                enumerator.MoveNext();
                enumerator.MoveNext();
                enumerator.MoveNext();
                enumerator.MoveNext();
            }
        }

        [Test]
        public void MoreCallsToMoveNext2()
        {
            var engine = new FileHelperAsyncEngine<Record>();
            string src = "first,line\nabc,JohnDoe";
            engine.Options.IgnoreFirstLines = 1;
            using (engine.BeginReadString(src))
            {
                var enumerator = (engine as IEnumerable<Record>).GetEnumerator();
                enumerator.MoveNext();
                enumerator.MoveNext();
                enumerator.MoveNext();
                enumerator.MoveNext();
            }
        }
    }
}