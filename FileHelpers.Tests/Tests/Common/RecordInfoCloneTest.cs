using System;
using System.Collections;
using System.Collections.Generic;
using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class RecordInfoCloneTest
	{
		[Test]
		public void CloneSimple()
		{
			var engine = new FileHelperEngine<SampleType>();
            var engine2 = new FileHelperEngine<SampleType>();

		    engine.RecordInfo.AssertDifferentObjectAs(engine2.RecordInfo);

		    CompareRecordInfo(engine.RecordInfo, engine2.RecordInfo);

		}
        
        [Test]
        public void CloneWithOptionsChanged()
        {
            var engine = new FileHelperEngine<SampleType>();
            engine.Options.IgnoreFirstLines = 3;
            engine.Options.IgnoreLastLines = 5;
            var engine2 = new FileHelperEngine<SampleType>();
            engine.Options.IgnoreFirstLines = 0;
            engine.Options.IgnoreLastLines = 0;

            engine.RecordInfo.AssertDifferentObjectAs(engine2.RecordInfo);

            CompareRecordInfo(engine.RecordInfo, engine2.RecordInfo);

        }

           [Test]
        public void CloneWithOptionsChanged2()
        {
            var engine = new FileHelperEngine<SampleType>();
            engine.Options.RecordCondition.Condition = RecordCondition.IncludeIfBegins;
            engine.Options.RecordCondition.Selector = "F";
            var engine2 = new FileHelperEngine<SampleType>();
            engine.Options.RecordCondition.Condition = RecordCondition.None;
            engine.Options.RecordCondition.Selector = string.Empty;

            engine.RecordInfo.AssertDifferentObjectAs(engine2.RecordInfo);

            CompareRecordInfo(engine.RecordInfo, engine2.RecordInfo);

        }


	    private void CompareRecordInfo(IRecordInfo recordInfo1, IRecordInfo recordInfo2)
	    {
	        var comp = new CompareObjects();
	        comp.CompareChildren = true;
            comp.ElementsToIgnore.Add("Cache");
            comp.ElementsToIgnore.Add("RecordType");
            comp.ElementsToIgnore.Add("FieldTypeInternal");
            comp.ElementsToIgnore.Add("FieldInfo");
            comp.ElementsToIgnore.Add("Operations");
            
            comp.ElementsToIgnore.Add("FieldType");
            comp.ElementsToIgnore.Add("TypeId");
            
            comp.CompareFields  = true;
            //comp.ComparePrivateFields = true;
            comp.ComparePrivateProperties= true;
            comp.CompareReadOnly = true;
            
	        comp.Compare(recordInfo1, recordInfo2).AssertIsTrue(comp.DifferencesString);
	    }

       
	}

	

}