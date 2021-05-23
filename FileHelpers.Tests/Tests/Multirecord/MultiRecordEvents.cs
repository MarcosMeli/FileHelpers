using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers.Events;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
    public class MultiRecordEvents
    {
        [SetUp]
        public void Clear()
        {
            mCityEventCount = 0;
        }

        [Test]
        public void GivenOnlyFirstTypeHasNotifyWrite_WhenWritingToFile_EventIsRaised()
        {
            var lobamba = new NotifyCity {Name = "Lobamba", Population = 11000};
            var rhine = new River {Name = "Rhine", LengthKilometers = 1230};
            var geography = new object[] {lobamba, rhine};

            var cityRiverEngine = new MultiRecordEngine(typeof(NotifyCity), typeof(River));
            cityRiverEngine.WriteFile("geography.txt", geography);

            Assert.AreEqual(2, mCityEventCount);
        }

        [Test]
        public void GivenStreaming_EventIsRaised()
        {
            var lobamba = new NotifyCity {Name = "Lobamba", Population = 11000};
            var rhine = new River {Name = "Rhine", LengthKilometers = 1230};
            var geography = new object[] {lobamba, rhine};

            var cityRiverEngine = new MultiRecordEngine(typeof(NotifyCity), typeof(River));
            cityRiverEngine.BeginWriteFile("geography.txt");
            cityRiverEngine.WriteNexts(geography);

            Assert.AreEqual(2, mCityEventCount);
        }

        private static int mCityEventCount;

        [DelimitedRecord("|")]
        public class NotifyCity : INotifyWrite
        {
            public string Name;
            public int Population;

            public void BeforeWrite(BeforeWriteEventArgs e)
            {
                mCityEventCount++;
            }

            public void AfterWrite(AfterWriteEventArgs e)
            {
                mCityEventCount++;
            }
        }

        [DelimitedRecord("|")]
        public class River
        {
            public decimal LengthKilometers;
            public string Name;
        }
    }
}