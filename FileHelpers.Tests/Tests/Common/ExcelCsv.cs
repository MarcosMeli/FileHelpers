using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class ExcelCsv
	{

        [Test]
		public void ReadExcelCsv1()
		{
			var engine = new FileHelperEngine<ExcelCsv1Type>();

            ExcelCsv1Type[] res = TestCommon.ReadTest<ExcelCsv1Type>(engine, "Good", "ExcelCsv1.txt");

			Assert.AreEqual(4, res.Length);

			Assert.AreEqual("AllwaysOnTop", res[0].OrganizationName);
			Assert.AreEqual("COMPUTERS, HARDWARE", res[1].OrganizationName);
			Assert.AreEqual("4S Consulting, Inc.", res[2].OrganizationName);
			Assert.AreEqual("SmartSolutions", res[3].OrganizationName);

			Assert.AreEqual("Test 1,", res[0].TestField);
			Assert.AreEqual("Test, 2", res[1].TestField);
			Assert.AreEqual(" Test 3", res[2].TestField);
			Assert.AreEqual("Test 4", res[3].TestField);
			
		}

		[Test]
		public void ReadExcelCsv2()
		{
			var engine = new FileHelperEngine<ExcelCsv2Type>();

            ExcelCsv2Type[] res = TestCommon.ReadTest<ExcelCsv2Type>(engine, "Good", "ExcelCsv2.txt");

			Assert.AreEqual(4, res.Length);

			Assert.AreEqual("AllwaysOnTop", res[0].OrganizationName);
			Assert.AreEqual("COMPUTERS, HARDWARE", res[1].OrganizationName);
			Assert.AreEqual("4S Consulting, Inc.", res[2].OrganizationName);
			Assert.AreEqual("SmartSolutions", res[3].OrganizationName);

			Assert.AreEqual("Test 1,", res[0].TestField);
			Assert.AreEqual("Test, 2", res[1].TestField);
			Assert.AreEqual(" Test 3", res[2].TestField);
			Assert.AreEqual("Test 4", res[3].TestField);
			
		}

		[Test]
		public void WriteExcelCsv1()
		{
			var arr = new List<ExcelCsv1Type>();
			ExcelCsv1Type record;

			record = new ExcelCsv1Type();
			record.OrganizationName = "AllwaysOnTop";
			record.TestField = "Test 1,";
			arr.Add(record);

			record = new ExcelCsv1Type();
			record.OrganizationName = "COMPUTERS, HARDWARE";
			record.TestField = "Test, 2";
			arr.Add(record);

			record = new ExcelCsv1Type();
			record.OrganizationName = "4S Consulting, Inc.";
			record.TestField = " Test 3";
			arr.Add(record);

			record = new ExcelCsv1Type();
			record.OrganizationName = "SmartSolutions";
			record.TestField = "Test 4";
			arr.Add(record);

			var engine = new FileHelperEngine<ExcelCsv1Type>();

			string tmp = engine.WriteString(arr.ToArray());
			ExcelCsv1Type[] res = engine.ReadString(tmp);
            
			Assert.AreEqual(4, res.Length);

			Assert.AreEqual("AllwaysOnTop", res[0].OrganizationName);
			Assert.AreEqual("COMPUTERS, HARDWARE", res[1].OrganizationName);
			Assert.AreEqual("4S Consulting, Inc.", res[2].OrganizationName);
			Assert.AreEqual("SmartSolutions", res[3].OrganizationName);

			Assert.AreEqual("Test 1,", res[0].TestField);
			Assert.AreEqual("Test, 2", res[1].TestField);
			Assert.AreEqual(" Test 3", res[2].TestField);
			Assert.AreEqual("Test 4", res[3].TestField);
			
		}

		[Test]
		public void WriteExcelCsv2()
		{
            var arr = new List<ExcelCsv2Type>();
			ExcelCsv2Type record;

			record = new ExcelCsv2Type();
			record.OrganizationName = "AllwaysOnTop";
			record.TestField = "Test 1,";
			arr.Add(record);

			record = new ExcelCsv2Type();
			record.OrganizationName = "COMPUTERS, HARDWARE";
			record.TestField = "Test, 2";
			arr.Add(record);

			record = new ExcelCsv2Type();
			record.OrganizationName = "4S Consulting, Inc.";
			record.TestField = " Test 3";
			arr.Add(record);

			record = new ExcelCsv2Type();
			record.OrganizationName = "SmartSolutions";
			record.TestField = "Test 4";
			arr.Add(record);

			var engine = new FileHelperEngine<ExcelCsv2Type>();

			string tmp = engine.WriteString(arr.ToArray());
			ExcelCsv2Type[] res = engine.ReadString(tmp);
            
			Assert.AreEqual(4, res.Length);

			Assert.AreEqual("AllwaysOnTop", res[0].OrganizationName);
			Assert.AreEqual("COMPUTERS, HARDWARE", res[1].OrganizationName);
			Assert.AreEqual("4S Consulting, Inc.", res[2].OrganizationName);
			Assert.AreEqual("SmartSolutions", res[3].OrganizationName);

			Assert.AreEqual("Test 1,", res[0].TestField);
			Assert.AreEqual("Test, 2", res[1].TestField);
			Assert.AreEqual(" Test 3", res[2].TestField);
			Assert.AreEqual("Test 4", res[3].TestField);
			
		}

		[Test]
		public void ReadExcelCsv3()
		{
			var engine = new FileHelperEngine<ExcelCsv3Type>();
            TestCommon.ReadTest<ExcelCsv3Type>(engine, "Good", "ExcelCsv2.txt");
		}


        [Test]
        public void ReadIrregularExcelCsv()
        {
            var engine = new FileHelperEngine<RecipientImport>();
            var records = (RecipientImport[]) engine.ReadString(mSampleData);

            for (int i = 0; i < records.Length; i++)
            {
                if (! records[i].Email.EndsWith("@test.com"))
                    Assert.Fail("Not ends with test.com");
            }
        }

        private string mSampleData =
	        @"Email,FirstName,MiddleName,LastName,Title,Company,WorkPhone,HomePhone,Address1,Address2,City,State,ZIP,Country 
test1@test.com,kh,kh,kjh,kj,hkj,h,kjh,kj,hk,,,, 
test2@test.com,ljk,lj,lk,jl,,j,lj,j,,,,, 
test3@test.com,""m,m"",hb,kjn,nk,nkn,nkn,knkj,knkj,,,,, 
test4@test.com,sdf,sdf,sdf,,,,,,,,,, 
test5@test.com,wer,,,,,,,,,,,, 
test6@test.com,rw,erw,,,,,,,,,,, 
test7@test.com,,erw,w,,,,,,,,,, 
test8@test.com,,,erw,,,,,,,,,, 
test9@test.com,,,,er,w,,,,,,,, 
test10@test.com,,,,,e,,,,,,,, 
test11@test.com,,,,,erwer,,,,,,,, 
test12@test.com,,,wer,wer,,,,,,,,, 
test13@test.com,wer,we,wer,,,,,,,,,, 
test14@test.com,,we,,,,,,,,,,, 
test15@test.com,,wer,,,,,,,,,,, 
test16@test.com,,w, 
test17@test.com,,erw,werwe 
test18@test.com,,,r 
test19@test.com,,, 
test20@test.com,,,wer 
test21@test.com,,,wer 
test22@test.com,,,er 
test23@test.com,,, 
test24@test.com,,, 
test25@test.com,,, 
test26@test.com,,, 
test27@test.com,,, 
test28@test.com,,, 
test29@test.com,,, 
test30@test.com,,, 
test31@test.com,,, 
test32@test.com
test33@test.com
test34@test.com
test35@test.com
test36@test.com
test37@test.com
test38@test.com
test39@test.com
test40@test.com
test41@test.com
test42@test.com
test43@test.com
test44@test.com
test45@test.com
test46@test.com
test47@test.com
test48@test.com
test49@test.com
test50@test.com
test51@test.com
test52@test.com
test53@test.com
test54@test.com
test55@test.com
test56@test.com
test57@test.com
test58@test.com
test59@test.com
test60@test.com
test61@test.com
test62@test.com
test63@test.com
test64@test.com
test65@test.com
test66@test.com
test67@test.com
test68@test.com
""test69@test.com""
test70@test.com
test71@test.com
test72@test.com
test73@test.com
test74@test.com
test75@test.com
test76@test.com
test77@test.com
test78@test.com
test79@test.com
test80@test.com
test81@test.com
test82@test.com
test83@test.com
test84@test.com
test85@test.com
""test86@test.com""
test87@test.com
test88@test.com
test89@test.com
test90@test.com
test91@test.com
test92@test.com
test93@test.com
test94@test.com
test95@test.com
test96@test.com
test97@test.com
test98@test.com
test99@test.com
test100@test.com
test101@test.com
test102@test.com
test103@test.com
test104@test.com
test105@test.com
test106@test.com
test107@test.com
test108@test.com
test109@test.com
test110@test.com
test111@test.com
test112@test.com
test113@test.com
test114@test.com
test115@test.com
test116@test.com
test117@test.com
test118@test.com
test119@test.com
test120@test.com
test121@test.com
""test122@test.com""
test123@test.com
test124@test.com
test125@test.com
test126@test.com
test127@test.com
test128@test.com
test129@test.com
test130@test.com
test131@test.com
test132@test.com
test133@test.com
test134@test.com
test135@test.com
test136@test.com
test137@test.com
test138@test.com
test139@test.com
test140@test.com
test141@test.com
test142@test.com
test143@test.com
test144@test.com
test145@test.com
test146@test.com
test147@test.com
test148@test.com
test149@test.com
test150@test.com";

	}

	[DelimitedRecord(",")]
	public sealed class ExcelCsv1Type
	{

		public int Id;
	
		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string OrganizationName;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string TestField;

	} 

	[DelimitedRecord(",")]
	public sealed class ExcelCsv2Type
	{

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string OrganizationName;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string TestField;

	} 

	[DelimitedRecord(",")]
	public sealed class ExcelCsv3Type
	{

		[FieldQuoted('"')]
		public string OrganizationName;

		[FieldQuoted('"')]
		public string TestField;

	} 


[IgnoreFirst()]
[DelimitedRecord(",")]
public class RecipientImport
{

    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    public string Email;
	[FieldOptional()]
	public string FirstName;
	[FieldOptional()]
	public string MiddleName;
	[FieldOptional()]
	public string LastName;
	[FieldOptional()]
	public string Title;
	[FieldOptional()]
	public string Company;
	[FieldOptional()]
	public string WorkPhone;
	[FieldOptional()]
	public string HomePhone;
	[FieldOptional()]
	public string Address1;
	[FieldOptional()]
	public string Address2;
	[FieldOptional()]
	public string City;
	[FieldOptional()]
	public string State;
	[FieldOptional()]
	public string ZIP;
	[FieldOptional()]
	public string Country;
	[FieldOptional()]
	public string FileName;

}


}
