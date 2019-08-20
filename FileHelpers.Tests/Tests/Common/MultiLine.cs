using System;
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
            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string Name;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string MailAddress;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string MailCity;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string MailStateProvince;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string MailCountry;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string MailPostal;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string DeliveryAddress;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string DeliveryCity;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string DeliveryStateProvince;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string DeliveryCountry;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string DeliveryPostal;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string ContactFirstName;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string ContactLastName;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string PrimaryPhone;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string Fax;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string AltPhone;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string EmailAddress;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string AccountNumber;

            [FieldTrim(TrimMode.Both), FieldQuoted]
            public string WebSite;

            [FieldTrim(TrimMode.Both), FieldQuoted(MultilineMode.AllowForBoth)]
            public string GeneralNotes;

            [FieldTrim(TrimMode.Both), FieldQuoted(MultilineMode.AllowForRead)]
            public string TechNotes;

            [FieldTrim(TrimMode.Both), FieldQuoted(MultilineMode.AllowForBoth)]
            public string PopupNotes;
        }
    }
}