#if ! MINI

using System;
using System.Collections;
using System.Collections.Generic;

#if V_3_0

using FileHelpers.Mapping;

namespace FileHelpers.Tests.Mapping
{
	[TestFixture]
    [Ignore]
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
            public int InAdvancedClass;
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

#endif