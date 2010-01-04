using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using FileHelpers;
using FileHelpers.Dynamic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
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
			Assert.AreEqual(Color.Gainsboro.R, ((Bitmap)res[0].MyImage).GetPixel(10, 5).R);
			Assert.AreEqual(Color.Gainsboro.G, ((Bitmap)res[0].MyImage).GetPixel(10, 5).G);
			Assert.AreEqual(Color.Gainsboro.B, ((Bitmap)res[0].MyImage).GetPixel(10, 5).B);
			Assert.AreEqual(Color.Navy.R, ((Bitmap)res[0].MyImage).GetPixel(10, 7).R);
			Assert.AreEqual(Color.Navy.G, ((Bitmap)res[0].MyImage).GetPixel(10, 7).G);
			Assert.AreEqual(Color.Navy.B, ((Bitmap)res[0].MyImage).GetPixel(10, 7).B);

		}

		[Test]
		public void ReadImageData()
		{
			string data = "iVBORw0KGgoAAAANSUhEUgAAABQAAAAKCAYAAAC0VX7mAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAB1JREFUOE9jYBgFQzcE7ty583+wu75h0LuQuBAEAD0wBRO2X3TEAAAAAElFTkSuQmCC";
			
			FileHelperEngine engine = new FileHelperEngine(typeof(ImageClass));
			ImageClass[] res = (ImageClass[]) engine.ReadString(data);
			
			Assert.AreEqual(1, res.Length);
			Assert.IsNotNull(res[0].MyImage);
			Assert.AreEqual(20, res[0].MyImage.Width);
			Assert.AreEqual(10, res[0].MyImage.Height);
			Assert.AreEqual(typeof(Bitmap), res[0].MyImage.GetType());
			Assert.AreEqual(Color.Gainsboro.R, ((Bitmap)res[0].MyImage).GetPixel(10, 5).R);
			Assert.AreEqual(Color.Gainsboro.G, ((Bitmap)res[0].MyImage).GetPixel(10, 5).G);
			Assert.AreEqual(Color.Gainsboro.B, ((Bitmap)res[0].MyImage).GetPixel(10, 5).B);
			Assert.AreEqual(Color.Navy.R, ((Bitmap)res[0].MyImage).GetPixel(10, 7).R);
			Assert.AreEqual(Color.Navy.G, ((Bitmap)res[0].MyImage).GetPixel(10, 7).G);
			Assert.AreEqual(Color.Navy.B, ((Bitmap)res[0].MyImage).GetPixel(10, 7).B);

		}

		
		[DelimitedRecord(",")]
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
					Byte[] bitmapData; 
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