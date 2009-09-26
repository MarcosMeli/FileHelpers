using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;
using System.Collections;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class InheritedTests
	{
		FileHelperEngine engine;

		[Test]
		public void Inherited1()
		{
			engine = new FileHelperEngine(typeof (SampleInheritType));

            Assert.AreEqual(3, engine.Options.FieldCount);

            Assert.AreEqual("Field1", engine.Options.FieldsNames[0]);
            Assert.AreEqual("Field2", engine.Options.FieldsNames[1]);
            Assert.AreEqual("Field3", engine.Options.FieldsNames[2]);

			SampleInheritType[] res;
			res = (SampleInheritType[]) TestCommon.ReadTest(engine, "Good", "Test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual("901", res[0].Field2);
			Assert.AreEqual(234, res[0].Field3);

			Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
			Assert.AreEqual("012", res[1].Field2);
			Assert.AreEqual(345, res[1].Field3);

		}

		[Test]
		public void InheritedEmpty()
		{
			engine = new FileHelperEngine(typeof (SampleInheritEmpty));

			SampleInheritEmpty[] res;
			res = (SampleInheritEmpty[]) TestCommon.ReadTest(engine, "Good", "Test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual("901", res[0].Field2);
			Assert.AreEqual(234, res[0].Field3);

			Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
			Assert.AreEqual("012", res[1].Field2);
			Assert.AreEqual(345, res[1].Field3);

		}




		[Test]
		public void Inherited2()
		{
			engine = new FileHelperEngine(typeof (DelimitedSampleInheritType));


		}

		[Test]
		public void InheritedEmptyDelimited()
		{
			engine = new FileHelperEngine(typeof (DelimitedSampleInheritEmpty));
		}


		[FixedLengthRecord]
			public class SampleBase
		{
			[FieldFixedLength(8)]
			[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
			public DateTime Field1;

			[FieldFixedLength(3)]
			[FieldAlign(AlignMode.Left, ' ')]
			[FieldTrim(TrimMode.Both)]
			public string Field2;

		}

		[FixedLengthRecord]
		public class SampleInheritType
			: SampleBase
		{
			[FieldFixedLength(3)]
			[FieldAlign(AlignMode.Right, '0')]
			[FieldTrim(TrimMode.Both)]
			public int Field3;
		}

		[FixedLengthRecord]
		public class SampleInheritEmpty
		: SampleInheritType
		{
			[FieldIgnored]
			public int Field5854;
		}

		




		[FixedLengthRecord]
			public class DelimitedSampleBase
		{
			[FieldFixedLength(8)]
			[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
			public DateTime Field1;

			[FieldFixedLength(3)]
			[FieldAlign(AlignMode.Left, ' ')]
			[FieldTrim(TrimMode.Both)]
			public string Field2;

		}

		[FixedLengthRecord]
			public class DelimitedSampleInheritType
			: DelimitedSampleBase
		{
			[FieldFixedLength(3)]
			[FieldAlign(AlignMode.Right, '0')]
			[FieldTrim(TrimMode.Both)]
			public int Field3;
		}

		[FixedLengthRecord]
			public class DelimitedSampleInheritEmpty
			: DelimitedSampleInheritType
		{
			[FieldIgnored]
			public int Field5854;
		}


        [FixedLengthRecord(FixedMode.AllowMoreChars)]
        public class Type1RecordBase
        {

            public class MoneyConverter : ConverterBase
            {
                private const int DECIMAL_PLACES = 2;

                public override string FieldToString(object from)
                {
                    Decimal v = Convert.ToDecimal(from);
                    v *= (10 ^ DECIMAL_PLACES);
                    return Convert.ToInt64(v).ToString();
                }

                public override object StringToField(string from)
                {
                    return Convert.ToDecimal(Decimal.Parse(from) / (10 ^ DECIMAL_PLACES));
                }
            }

            #region Fields
            [FieldFixedLength(AmexFieldLengths.RecordType)]
            public string RecordType;

            [FieldFixedLength(AmexFieldLengths.RequestingControlAccount)]
            public string RequestingControlAccount;

            [FieldFixedLength(AmexFieldLengths.BasicControlAccount)]
            public string BasicControlAccount;

            [FieldFixedLength(AmexFieldLengths.CardholderAccountNumber)]
            public string CardholderAccountNumber;

            [FieldFixedLength(AmexFieldLengths.SENumber)]
            public string SENumber;

            [FieldFixedLength(AmexFieldLengths.ROCID)]
            public string ROCID;

            [FieldFixedLength(AmexFieldLengths.DBCRIndicator)]
            public string DBCRIndicator;

            [FieldFixedLength(AmexFieldLengths.TransactionTypeCode)]
            public string TransactionTypeCode;

            [FieldFixedLength(AmexFieldLengths.FinancialCategory)]
            public string FinancialCategory;

            [FieldFixedLength(AmexFieldLengths.BatchNumber)]
            public string BatchNumber;

            [FieldFixedLength(AmexFieldLengths.DateOfCharge)]
            [FieldConverter(ConverterKind.Date, "MMddyy")]
            public DateTime DateOfCharge;

            [FieldFixedLength(AmexFieldLengths.LocalCurrencyAmount)]
            [FieldAlign(AlignMode.Right, '0')]
            [FieldConverter(typeof(MoneyConverter))]
            public decimal LocalCurrencyAmount;

            [FieldFixedLength(AmexFieldLengths.CurrencyCode)]
            public string CurrencyCode;

            [FieldFixedLength(AmexFieldLengths.CaptureDate)]
            public string CaptureDate;

            [FieldFixedLength(AmexFieldLengths.ProcessDate)]
            [FieldConverter(ConverterKind.Date, "MMddyy")]
            public DateTime ProcessDate;

            [FieldFixedLength(AmexFieldLengths.BillingDate)]
            [FieldConverter(ConverterKind.Date, "MMddyy")]
            public DateTime BillingDate;

            [FieldFixedLength(AmexFieldLengths.BillingAmount)]
            [FieldAlign(AlignMode.Right, '0')]
            [FieldConverter(typeof(MoneyConverter))]
            public decimal BillingAmount;

            [FieldFixedLength(AmexFieldLengths.SalesTaxAmount)]
            [FieldAlign(AlignMode.Right, '0')]
            [FieldConverter(typeof(MoneyConverter))]
            public decimal SalesTaxAmount;

            [FieldFixedLength(AmexFieldLengths.TipAmount)]
            [FieldAlign(AlignMode.Right, '0')]
            [FieldConverter(typeof(MoneyConverter))]
            public decimal TipAmount;

            [FieldFixedLength(AmexFieldLengths.CardmemberName)]
            [FieldTrim(TrimMode.Right)]
            public string CardmemberName;

            [FieldFixedLength(AmexFieldLengths.SpecialBillInd)]
            public string SpecialBillInd;

            [FieldFixedLength(AmexFieldLengths.OriginatingBCA)]
            public string OriginatingBCA;

            [FieldFixedLength(AmexFieldLengths.OriginatingAccountNumber)]
            public string OriginatingAccountNumber;

            [FieldFixedLength(AmexFieldLengths.CMReferenceNumber)]
            public string CMReferenceNumber;

            [FieldFixedLength(AmexFieldLengths.SupplierReferenceNumber)]
            public string SupplierReferenceNumber;

            [FieldFixedLength(AmexFieldLengths.ShipToZip)]
            public string ShipToZip;

            [FieldFixedLength(AmexFieldLengths.SICCode)]
            public string SICCode;

            [FieldFixedLength(AmexFieldLengths.CostCenter)]
            [FieldConverter(ConverterKind.Int32)]
            [FieldAlign(AlignMode.Right, ' ')]
            public int CostCenter;

            [FieldFixedLength(AmexFieldLengths.EmployeeID)]
            [FieldConverter(ConverterKind.Int32)]
            [FieldAlign(AlignMode.Right, ' ')]
            public int EmployeeID;

            [FieldFixedLength(AmexFieldLengths.SocialSecurityNumber)]
            public string SocialSecurityNumber;

            [FieldFixedLength(AmexFieldLengths.UniversalNumber)]
            public string UniversalNumber;

            [FieldFixedLength(AmexFieldLengths.Street)]
            [FieldTrim(TrimMode.Right)]
            public string Street;

            [FieldFixedLength(AmexFieldLengths.City)]
            [FieldTrim(TrimMode.Right)]
            public string City;

            [FieldFixedLength(AmexFieldLengths.State)]
            [FieldTrim(TrimMode.Right)]
            public string State;

            [FieldFixedLength(AmexFieldLengths.Zip)]
            public string Zip;

            [FieldFixedLength(AmexFieldLengths.TransLimit)]
            public string TransLimit;

            [FieldFixedLength(AmexFieldLengths.MonthlyLimit)]
            public string MonthlyLimit;

            [FieldFixedLength(AmexFieldLengths.ExposureLimit)]
            public string ExposureLimit;

            [FieldFixedLength(AmexFieldLengths.RevCode)]
            public string RevCode;

            [FieldFixedLength(AmexFieldLengths.CompanyName)]
            [FieldTrim(TrimMode.Right)]
            public string CompanyName;

            [FieldFixedLength(AmexFieldLengths.ChargeDescriptionLine1)]
            [FieldTrim(TrimMode.Right)]
            public string ChargeDescriptionLine1;

            #endregion
        }

        /// <summary>
        /// Summary description for Type1Default.
        /// </summary>
        [FixedLengthRecord(FixedMode.AllowMoreChars)]
        public class Type1RecordDefault : Type1RecordBase
        {
            #region Type 1 Fields

            [FieldFixedLength(AmexFieldLengths.ChargeDescriptionLine2)]
            [FieldTrim(TrimMode.Right)]
            public string ChargeDescriptionLine2;

            [FieldFixedLength(AmexFieldLengths.ChargeDescriptionLine3)]
            [FieldTrim(TrimMode.Right)]
            public string ChargeDescriptionLine3;

            [FieldFixedLength(AmexFieldLengths.ChargeDescriptionLine4)]
            [FieldTrim(TrimMode.Right)]
            public string ChargeDescriptionLine4;

            [FieldFixedLength(AmexFieldLengths.IndustryCode)]
            public string IndustryCode;

            [FieldFixedLength(AmexFieldLengths.SequenceNumber)]
            public string SequenceNumber;

            [FieldFixedLength(AmexFieldLengths.MercatorKey)]
            public string MercatorKey;

            [FieldFixedLength(AmexFieldLengths.TransactionFeeIndicator)]
            [FieldOptional]
            public string TransactionFeeIndicator;

            [FieldFixedLength(AmexFieldLengths.TailFiller)]
            [FieldOptional]
            public string TailFiller;

            #endregion


        }



        internal sealed class AmexFieldLengths
        {
            private AmexFieldLengths() { }

            // Type 1 and Shared
            internal const int RecordType = 1;
            internal const int RequestingControlAccount = 15;
            internal const int BasicControlAccount = 15;
            internal const int CardholderAccountNumber = 15;
            internal const int SENumber = 10;
            internal const int ROCID = 13;
            internal const int DBCRIndicator = 1;
            internal const int TransactionTypeCode = 2;

            internal const int FinancialCategory = 1;
            internal const int BatchNumber = 3;
            internal const int DateOfCharge = 6;
            internal const int LocalCurrencyAmount = 9;
            internal const int CurrencyCode = 3;
            internal const int CaptureDate = 5;
            internal const int ProcessDate = 6;
            internal const int BillingDate = 6;
            internal const int BillingAmount = 9;
            internal const int SalesTaxAmount = 9;
            internal const int TipAmount = 9;
            internal const int CardmemberName = 20;
            internal const int SpecialBillInd = 1;
            internal const int OriginatingBCA = 15;
            internal const int OriginatingAccountNumber = 15;
            internal const int CMReferenceNumber = 17;
            internal const int SupplierReferenceNumber = 11;

            internal const int ShipToZip = 6;
            internal const int SICCode = 4;
            internal const int CostCenter = 10;
            internal const int EmployeeID = 10;
            internal const int SocialSecurityNumber = 10;
            internal const int UniversalNumber = 25;
            internal const int Street = 20;
            internal const int City = 18;
            internal const int State = 2;
            internal const int Zip = 10;
            internal const int TransLimit = 5;
            internal const int MonthlyLimit = 7;
            internal const int ExposureLimit = 7;
            internal const int RevCode = 1;
            internal const int CompanyName = 20;
            internal const int ChargeDescriptionLine1 = 42;
            internal const int ChargeDescriptionLine2 = 42;
            internal const int ChargeDescriptionLine3 = 42;
            internal const int ChargeDescriptionLine4 = 42;

            internal const int IndustryCode = 2;
            internal const int SequenceNumber = 7;
            internal const int MercatorKey = 21;
            internal const int TransactionFeeIndicator = 3;

            internal const int CarRentalCustomerName = 20;
            internal const int CarRentalCity = 18;
            internal const int CarRentalState = 2;
            internal const int CarRentalDate = 6;
            internal const int CarReturnCity = 18;
            internal const int CarReturnState = 2;
            internal const int CarReturnDate = 6;
            internal const int CarRentalDays = 2;

            internal const int HotelArrivalDate = 6;
            internal const int HotelCity = 18;
            internal const int HotelState = 2;
            internal const int HotelDepartDate = 6;
            internal const int HotelStayDuration = 2;
            internal const int HotelRoomRate = 7;

            internal const int AirAgencyNumber = 8;
            internal const int AirTicketIssuer = 25;
            internal const int AirClassOfService = 8;
            internal const int AirCarrierCode = 16;
            internal const int AirRouting = 27;
            internal const int AirDepartureDate = 6;
            internal const int AirPassengerName = 20;

            internal const int TeleDateOfCall = 6;
            internal const int TeleFromCity = 18;
            internal const int TeleFromState = 2;
            internal const int TeleCallLength = 4;
            internal const int TeleReferenceNumber = 8;
            internal const int TeleTimeOfCall = 4;
            internal const int TeleToNumber = 10;

            internal const int TailFiller = 2;
            internal const int CarRentalFiller = 52;
            internal const int HotelFiller = 85;
            internal const int AirFiller = 16;
            internal const int TeleFiller = 74;

            // Type 2
            internal const int CMName = 20;
            internal const int CMPreviousBalance = 11;
            internal const int CMPreviousBalanceSign = 1;
            internal const int CMNewCharges = 11;
            internal const int CMNewChargesSign = 1;
            internal const int CMOtherDebits = 11;
            internal const int CMOtherDebitsSign = 1;
            internal const int CMPayments = 11;
            internal const int CMPaymentsSign = 1;
            internal const int CMOtherCredits = 11;
            internal const int CMOtherCreditsSign = 1;
            internal const int CMBalance = 11;
            internal const int CMBalanceSign = 1;
            internal const int CID = 6;
            internal const int GroupID = 20;
            internal const int Type2Filler = 405;

            // Type 3
            internal const int BCAName = 20;
            internal const int BCADebits = 11;
            internal const int BCADebitsSign = 1;
            internal const int BCACredits = 11;
            internal const int BCACreditsSign = 1;
            internal const int BCATransLimit = 5;
            internal const int BCAMonthlyLimit = 7;
            internal const int BCAExposureLimit = 7;
            internal const int BCABudgetaryLimit = 9;
            internal const int BCAPreviousBalance = 11;
            internal const int BCAPreviousBalanceSign = 1;
            internal const int Type3Filler = 448;

            // Type 4
            internal const int RequestingControlAccountName = 20;
            internal const int ControlDebits = 11;
            internal const int ControlDebitsSign = 1;
            internal const int ControlCredits = 11;
            internal const int ControlCreditsSign = 1;
            internal const int TransactionLimit = 5;
            internal const int BudgetaryLimit = 9;
            internal const int ControlPreviousBalance = 11;
            internal const int ControlPreviousBalanceSign = 1;
            internal const int Type4Filler = 463;

            // Type 5
            internal const int SEName1 = 25;
            internal const int SEName2 = 25;
            internal const int SEStreet1 = 25;
            internal const int SEStreet2 = 25;
            internal const int SECity = 25;
            internal const int SEState = 2;
            internal const int SEZip = 10;
            internal const int SECountry = 25;
            internal const int SEPhone = 13;
            internal const int SEInductryCode = 2;
            internal const int SESubIndustryCode = 3;
            internal const int FederalTaxID = 9;
            internal const int DandBNumber = 9;
            internal const int OwnerTypeCode = 2;
            internal const int PurchasingCardCode = 2;
            internal const int CorporationStatusIndicator = 1;
            internal const int Type5Filler = 357;

        }


        [FixedLengthRecord]
        [IgnoreInheritedClass]
        public class SampleInheritIgnoreType
            : CollectionBase
        {
            [FieldFixedLength(8)]
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime Field1;

            [FieldFixedLength(3)]
            [FieldAlign(AlignMode.Left, ' ')]
            [FieldTrim(TrimMode.Both)]
            public string Field2;
            
            [FieldFixedLength(3)]
            [FieldAlign(AlignMode.Right, '0')]
            [FieldTrim(TrimMode.Both)]
            public int Field3;
        }

        [FixedLengthRecord]
        public class SampleInheritIgnoreType2
            : SampleInheritIgnoreType
        {
            [FieldOptional]
            [FieldNullValue(2)]
            [FieldFixedLength(3)]
            public int Field4;
        }


        [Test]
        public void IgnoreInherited1()
        {
            engine = new FileHelperEngine(typeof(SampleInheritIgnoreType));

            Assert.AreEqual(3, engine.Options.FieldCount);

            SampleInheritIgnoreType[] res;
            res = (SampleInheritIgnoreType[])TestCommon.ReadTest(engine, "Good", "Test1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
            Assert.AreEqual("901", res[0].Field2);
            Assert.AreEqual(234, res[0].Field3);

            Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
            Assert.AreEqual("012", res[1].Field2);
            Assert.AreEqual(345, res[1].Field3);

        }


        [Test]
        public void IgnoreInherited2()
        {
            engine = new FileHelperEngine(typeof(SampleInheritIgnoreType2));

            Assert.AreEqual(4, engine.Options.FieldCount);
            Assert.AreEqual("Field4", engine.Options.FieldsNames[3]);

            SampleInheritIgnoreType2[] res;
            res = (SampleInheritIgnoreType2[])TestCommon.ReadTest(engine, "Good", "Test1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
            Assert.AreEqual("901", res[0].Field2);
            Assert.AreEqual(234, res[0].Field3);

            Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
            Assert.AreEqual("012", res[1].Field2);
            Assert.AreEqual(345, res[1].Field3);

        }

	}
}