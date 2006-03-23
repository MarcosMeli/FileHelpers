using System.IO;
using System.Text;

namespace FileHelpers
{
	internal class ForwardReader
	{
		TextReader mReader;

		private string[] mFowardStrings;
		private int mForwardIndex = 0;

		private int mRemaingLines = 0;

		public int RemainingLines
		{
			get { return mRemaingLines; }
		}

		internal ForwardReader(TextReader reader)
			: this(reader, 0)
		{
		}

		internal ForwardReader(TextReader reader, int forwardLines)
		{
			mReader = reader;
			mFowardLines = forwardLines;

			mFowardStrings = new string[mFowardLines + 1];
			mRemaingLines = mFowardLines + 1;

			for (int i = 0; i < mFowardLines + 1; i++)
			{
				mFowardStrings[i] = reader.ReadLine();
				if (mFowardStrings[i] == null)
				{
					mRemaingLines = i;
					break;
				}
			}

		}

		private bool mDiscardForward = false;

		public bool DiscardForward
		{
			get { return mDiscardForward; }
			set { mDiscardForward = value; }
		}

		private int mFowardLines = 0;

		public int FowardLines
		{
			get { return mFowardLines; }
		}

		public string ReadNextLine()
		{
			if (mRemaingLines <= 0)
				return null;
			else
			{
				string res = mFowardStrings[mForwardIndex];

				if (mRemaingLines == (mFowardLines + 1))
				{
					mFowardStrings[mForwardIndex] = mReader.ReadLine();

					if (mFowardStrings[mForwardIndex] == null)
					{
						mRemaingLines--;
					}

				}
				else
				{
					mRemaingLines--;
					if (mDiscardForward)
						return null;

				}

				mForwardIndex = (mForwardIndex + 1)%(mFowardLines + 1);

				return res;
			}

		}

		public string RemainingText
		{
			get
			{
				StringBuilder sb = new StringBuilder(100);

				for (int i = 0; i < mRemaingLines + 1; i++)
				{
					sb.Append(mFowardStrings[(mForwardIndex + i)%(mFowardLines + 1)] + "\r\n");
				}

				return sb.ToString();
			}
		}


		public void Close()
		{
			mReader.Close();
		}

	}
}