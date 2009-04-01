#if ! MINI

using System;
using System.Data.OleDb;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;
using FileHelpers.Mapping;

namespace FileHelpersTests.Mapping
{
	[TestFixture]
    [Explicit]
	public class DataMapperBasicTests
	{
	
		[Test]
		public void RecurseGetFields()
		{
            DataMapper mapper = new DataMapper(typeof(FinalMapperClass));
            mapper.AddMapping(0, "InBaseClass");
            mapper.AddMapping(1, "InAdvancedClass");
            mapper.AddMapping(2, "InFinalClass");


            Assert.IsNotNull(mapper);
		}

        [Test]
        public void RecurseGetFieldsNotFound()
        {
            DataMapper mapper = new DataMapper(typeof(FinalMapperClass));
            mapper.AddMapping(0, "InBaseClass");
            mapper.AddMapping(1, "InAdvancedClassNotFound");
            
            Assert.IsNotNull(mapper);
        }
        private class BasicMapperClass
        {
            public string InBaseClass;
        }

        private class AdvancedMapperClass
            :BasicMapperClass
        {
           // [FieldIgnored()]
            private int InAdvancedClass;
        }
        
        [DelimitedRecord("\t")]
        private class FinalMapperClass
                : AdvancedMapperClass
        {
            public bool InFinalClass;
        }

    }

}

#endif