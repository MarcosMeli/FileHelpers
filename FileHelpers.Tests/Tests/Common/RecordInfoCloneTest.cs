using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FileHelpers;
using FileHelpers.Options;
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

		    engine.mRecordInfo.AssertDifferentObjectAs(engine2.mRecordInfo);

		    CompareRecordInfo(engine.mRecordInfo, engine2.mRecordInfo);

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

            engine.mRecordInfo.AssertDifferentObjectAs(engine2.mRecordInfo);

            CompareRecordInfo(engine.mRecordInfo, engine2.mRecordInfo);

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

            engine.mRecordInfo.AssertDifferentObjectAs(engine2.mRecordInfo);

            CompareRecordInfo(engine.mRecordInfo, engine2.mRecordInfo);

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