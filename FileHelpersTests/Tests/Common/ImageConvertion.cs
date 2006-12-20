using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using FileHelpers;
using FileHelpers.RunTime;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class ImageConverterTest
	{

		[Test]
		public void WriteReadImage()
		{
			
			ImageClass ima = new ImageClass();
			Bitmap bmp  = new Bitmap(20, 10);
			bmp.SetPixel(10, 5, Color.Gainsboro);
			bmp.SetPixel(10, 7, Color.Navy);
			ima.MyImage = bmp;

			FileHelperEngine engine = new FileHelperEngine(typeof(ImageClass));
			string data = engine.WriteString((IList) new object[] {ima});
			
			ImageClass[] res = (ImageClass[]) engine.ReadString(data);
			
			Assert.AreEqual(1, res.Length);
			Assert.IsNotNull(res[0].MyImage);
			Assert.AreEqual(typeof(Bitmap), res[0].MyImage.GetType());
			Assert.AreEqual(Color.Gainsboro, ((Bitmap)res[0].MyImage).GetPixel(10, 5));
			Assert.AreEqual(Color.Navy, ((Bitmap)res[0].MyImage).GetPixel(10, 7));

		}
		
		private class ImageClass 
		{ 
			// the rest of your class 

  
			// the library only serializes fields not properties, so you are saving the string. 

			[FieldConverter(typeof(ImageConverter))]
			public Image MyImage; 

			public class ImageConverter: ConverterBase
			{
				public override object StringToField(string from)
				{
					Byte[] bitmapData = new Byte[from.Length]; 
					bitmapData = Convert.FromBase64String(from); 
					System.IO.MemoryStream streamBitmap = new MemoryStream(bitmapData); 
					return Image.FromStream(streamBitmap); 
				}
				
				public override string FieldToString(object from)
				{
					Image ima = (Image) from;
					MemoryStream ms = new MemoryStream(); 
					ima.Save(ms, System.Drawing.Imaging.ImageFormat.Png); 
					return Convert.ToBase64String(ms.ToArray()); 
					
				}

			}
		}
	}
}