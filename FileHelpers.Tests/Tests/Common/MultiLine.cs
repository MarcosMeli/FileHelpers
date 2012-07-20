using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class MultiLine
	{
		[Test]
		public void MultilineFull()
		{
			var engine = new FileHelperEngine<FHClient>();

            var res = engine.ReadFile(TestCommon.GetPath("Good", "MultilineFull.txt"));

            Assert.AreEqual(16, res.Length);
            Assert.AreEqual(16, engine.TotalRecords);

		}

		[DelimitedRecord(",")]
		public sealed class FHClient
		{
			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String Name;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String MailAddress;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String MailCity;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String MailStateProvince;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String MailCountry;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String MailPostal;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String DeliveryAddress;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String DeliveryCity;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String DeliveryStateProvince;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String DeliveryCountry;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String DeliveryPostal;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String ContactFirstName;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String ContactLastName;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String PrimaryPhone;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String Fax;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String AltPhone;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String EmailAddress;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String AccountNumber;

			[FieldTrim(TrimMode.Both),FieldQuoted()]
			public String WebSite;

			[FieldTrim(TrimMode.Both),FieldQuoted(MultilineMode.AllowForBoth)]
			public String GeneralNotes;

			[FieldTrim(TrimMode.Both),FieldQuoted(MultilineMode.AllowForRead)]
			public String TechNotes;

			[FieldTrim(TrimMode.Both),FieldQuoted(MultilineMode.AllowForBoth)]
			public String PopupNotes;

		}


	}
}