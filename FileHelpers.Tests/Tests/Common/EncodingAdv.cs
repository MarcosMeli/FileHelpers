using System;
using System.Text;
using NUnit.Framework;
using FileHelpers;
using System.Net;
using System.IO;
using FileHelpers.Tests;

namespace FileHelpers.Tests
{

		[TestFixture]
		public class EncodingAdvanced
		{
			private const string MSWSDataUrl_Format = "http://data.msws.net/temp_precip/temp_precip{0}.txt";//http://data.msws.net/temp_precip/temp_precip7-20-06.txt
			private const string MSWSDataURL_DateFormat = "M-d-yy";

			[Test]
			public void GetMSWSReportsFromFile_20060709_28Records()
			{
				var res = FileTest.Good.EncodingAdv1.ReadWithEngine<MSWSDailyReportRecord>();
            
				Assert.AreEqual(res.Length, 28);
			}

			[Test]
			public void GetMSWSReportsFromFile_20060720_32Records()
			{
                var res = FileTest.Good.EncodingAdv2.ReadWithEngine<MSWSDailyReportRecord>();
			
				Assert.AreEqual(res.Length, 32);
			}
             
			[Test]
            [Ignore]
			public void GetMSWSReportsFromURL_AsData_20060709_28Records()
			{
				DateTime date = new DateTime(2006, 7, 20);
				string url = string.Format(MSWSDataUrl_Format, date.ToString(MSWSDataURL_DateFormat));
				MSWSDailyReportRecord[] res = null;
				FileHelperEngine engine = new FileHelperEngine(typeof(MSWSDailyReportRecord));
					byte[] data;
				using (WebClient webClient = new WebClient())
				{
					//webClient.Encoding = System.Text.Encoding.ASCII;                
					data = webClient.DownloadData(url);
					System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
					string dataString = encoding.GetString(data);
					res = (MSWSDailyReportRecord[]) engine.ReadString(dataString);
				}
				
				Assert.AreEqual(res.Length, 32);
			}

			[Test]
            [Ignore]
			public void GetMSWSReportsFromURL_AsString_20060709_28Records()
			{
				DateTime date = new DateTime(2006, 7, 20);
				string url = string.Format(MSWSDataUrl_Format, date.ToString(MSWSDataURL_DateFormat));
				MSWSDailyReportRecord[] res = null;
				FileHelperEngine engine = new FileHelperEngine(typeof(MSWSDailyReportRecord));

					using (WebClient webClient = new WebClient())
					{
						webClient.DownloadFile(url, "tempotemp.txt");
						res = (MSWSDailyReportRecord[]) engine.ReadFile("tempotemp.txt");
					}

				Assert.AreEqual(res.Length, 32);
			}

			[Test]
            [Ignore]
			public void GetMSWSReportsFromURL_AsStream_20060709_28Records()
			{
				DateTime date = new DateTime(2006, 7, 20);
				string url = string.Format(MSWSDataUrl_Format, date.ToString(MSWSDataURL_DateFormat));
				MSWSDailyReportRecord[] res = null;
				FileHelperEngine engine = new FileHelperEngine(typeof(MSWSDailyReportRecord));

					// make request 
					HttpWebRequest webReq = null;
				HttpWebResponse webResp = null;
				StreamReader reader = null;
				try
				{
					webReq = (HttpWebRequest)HttpWebRequest.Create(url);
					webResp = (HttpWebResponse)webReq.GetResponse();
					Encoding encode = Encoding.GetEncoding("utf-8");
					reader = new StreamReader(webResp.GetResponseStream(), encode);
					res = (MSWSDailyReportRecord[]) engine.ReadStream(reader);
				}
				catch 
				{
					throw;
				}
				finally
				{
					if (webReq != null) webReq = null;
					if (webResp != null) webResp.Close();
					if (reader != null) reader.Close();
				}
                        
				Assert.AreEqual(res.Length, 32);
			}

		}

	[FixedLengthRecordAttribute()]
	[IgnoreFirst(7)]
	[IgnoreLast(5)]
	public sealed class MSWSDailyReportRecord
	{
		[FieldFixedLength(26)]
		public String Location;
		[FieldFixedLength(12)]
		public String County;
		[FieldFixedLength(5)]
		public int Elev;
		[FieldFixedLength(4)]
		public int Hi;
		[FieldFixedLength(4)]
		public int Lo;

		[FieldFixedLength(6)]
		[FieldTrim(TrimMode.Both)]
		public string PCPN;
		[FieldFixedLength(5)]
		public decimal SNOW;
		[FieldFixedLength(5)]
		public decimal SNDPT;
		[FieldFixedLength(6)]
		public decimal MONTH;

	}

}