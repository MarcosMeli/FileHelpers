using System;
using System.Text;

namespace FileHelpers
{
	public class FieldBuilder
	{
		private string mFieldName;
		private Type mFieldType;

		internal FieldBuilder(string fieldName, Type fieldType)
		{
			mFieldName = fieldName;
			mFieldType = fieldType;
		}

		#region TrimMode

		private TrimMode mTrimMode = TrimMode.None;

		public TrimMode TrimMode
		{
			get { return mTrimMode; }
			set { mTrimMode = value; }
		}
		#endregion

		private AlignMode mAlignMode = AlignMode.Left;

		public AlignMode AlignMode
		{
			get { return mAlignMode; }
			set { mAlignMode = value; }
		}


		internal int mFieldIndex = -1;

		public int FieldIndex
		{
			get { return mFieldIndex; }
		}

		private bool mFieldInNewLine = false;

		public bool FieldInNewLine
		{
			get { return mFieldInNewLine; }
			set { mFieldInNewLine = value; }
		}

		private bool mFieldIgnored = false;

		public bool FieldIgnored
		{
			get { return mFieldIgnored; }
			set { mFieldIgnored = value; }
		}

		private bool mFieldOptional = false;

		public bool FieldOptional
		{
			get { return mFieldOptional; }
			set { mFieldOptional = value; }
		}


		internal string GetFieldDef(NetLenguage leng)
		{
			StringBuilder sb = new StringBuilder(100);
			switch(leng)
			{
				case NetLenguage.CSharp:

					break;

			}

			return sb.ToString();
		}
	}
}
