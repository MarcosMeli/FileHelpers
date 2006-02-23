#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System.IO;
using System.Text;

namespace FileHelpers
{
	internal sealed class StreamHelper
	{
		private StreamHelper()
		{
		}

		internal static TextWriter CreateFileAppender(string fileName, Encoding encode, bool correctEnd)
		{
			return CreateFileAppender(fileName, encode, correctEnd, true);
		}

		internal static TextWriter CreateFileAppender(string fileName, Encoding encode, bool correctEnd, bool disposeStream)
		{
			TextWriter res;

			if (correctEnd)
			{
				FileStream fs = null;

				try
				{
					fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
					fs.Seek(-2, SeekOrigin.End);

					if (fs.ReadByte() == 13 && fs.ReadByte() == 10)
					{
						int nowRead;
						do
						{
							fs.Seek(-2, SeekOrigin.Current);
							nowRead = fs.ReadByte();
						} while (nowRead == 13 || nowRead == 10);
					}

					fs.WriteByte(13);
					fs.WriteByte(10);

					res = new StreamWriter(fs, encode);

				}
				finally
				{
					if (disposeStream && fs != null)
						fs.Close();
				}
			}
			else
			{
				res = new StreamWriter(fileName, true, encode);
			}

			return res;
		}

	}
}