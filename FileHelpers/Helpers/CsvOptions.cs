using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.Options
{
	/// <summary>Class used to pass information to the <see cref="FileHelpers.Dynamic.CsvClassBuilder"/> and the <see cref="CsvEngine"/></summary>
	public sealed class CsvOptions
	{

		/// <summary>Create a CSV Wrapper using the specified number of fields.</summary>
		/// <param name="className">The name of the record class</param>
		/// <param name="delimiter">The delimiter for each field</param>
		/// <param name="numberOfFields">The number of fields of each record</param>
		public CsvOptions(string className, char delimiter, int numberOfFields)
			:this(className, delimiter, numberOfFields, 1)
		{
		}

		/// <summary>Create a CSV Wrapper using the specified number of fields.</summary>
		/// <param name="className">The name of the record class</param>
		/// <param name="delimiter">The delimiter for each field</param>
		/// <param name="numberOfFields">The number of fields of each record</param>
		/// <param name="headerLines">The number of lines to use as header</param>
		public CsvOptions(string className, char delimiter, int numberOfFields, int headerLines)
		{
			mHeaderLines = headerLines;
			mRecordClassName = className;
			mDelimiter = delimiter;
			mNumberOfFields = numberOfFields;
		}

		/// <summary>Create a CSV Wrapper using the specified sample file with their headers.</summary>
		/// <param name="className">The name of the record class</param>
		/// <param name="delimiter">The delimiter for each field</param>
		/// <param name="sampleFile">A sample file with a header that contains the names of the fields.</param>
		public CsvOptions(string className, char delimiter, string sampleFile)
		{
			mRecordClassName = className;
			mDelimiter = delimiter;
			mSampleFileName = sampleFile;
		}

		/// <summary>Create a CSV Wrapper using the specified sample file with their headers.</summary>
		/// <param name="className">The name of the record class</param>
		/// <param name="delimiter">The delimiter for each field</param>
		/// <param name="sampleFile">A sample file with a header that contains the names of the fields.</param>
		/// <param name="headerDelimiter">The delimiter for the header line</param>
		public CsvOptions(string className, char delimiter, char headerDelimiter, string sampleFile)
		{
			mHeaderDelimiter = headerDelimiter;
			mRecordClassName = className;
			mDelimiter = delimiter;
			mSampleFileName = sampleFile;
		}

		private string mSampleFileName = string.Empty;
		private char mDelimiter = ',';
		private char mHeaderDelimiter = char.MinValue;
		private int mHeaderLines = 1;
		private string mRecordClassName = string.Empty;
		private int mNumberOfFields = -1;
		private string mFieldsPrefix = "Field_";
		private string mDateFormat= "dd/MM/yyyy";
		private string mDecimalSeparator = ".";
		private Encoding mEncoding = Encoding.Default;
		private bool mIgnoreEmptyLines = true;

		/// <summary>A sample file from where to read the field names and number.</summary>
		public string SampleFileName
		{
			get { return mSampleFileName; }
			set { mSampleFileName = value; }
		}

		/// <summary>The delimiter for each field.</summary>
		public char Delimiter
		{
			get { return mDelimiter; }
			set { mDelimiter = value; }
		}

		/// <summary>The delimiter for each file name in the header.</summary>
		public char HeaderDelimiter
		{
			get { return mHeaderDelimiter; }
			set { mHeaderDelimiter = value; }
		}

		/// <summary>The name used for the record class (a valid .NET class).</summary>
		public string RecordClassName
		{
			get { return mRecordClassName; }
			set { mRecordClassName = value; }
		}

		/// <summary>The prefix used when you only specified the number of fields</summary>
		public string FieldsPrefix
		{
			get { return mFieldsPrefix; }
			set { mFieldsPrefix = StringHelper.ToValidIdentifier(value); }
		}

		/// <summary>The number of fields that the file contains.</summary>
		public int NumberOfFields
		{
			get { return mNumberOfFields; }
			set { mNumberOfFields = value; }
		}

		/// <summary>The number of header lines</summary>
		public int HeaderLines
		{
			get { return mHeaderLines; }
			set { mHeaderLines = value; }
		}

		/// <summary>The DateFormat used to read and write DateTime values</summary>
		public string DateFormat
		{
			get { return mDateFormat; }
			set { mDateFormat = value; }
		}

		/// <summary>The Decimal Separator used to read and write doubles, singles and decimal values</summary>
		public string DecimalSeparator
		{
			get { return mDecimalSeparator; }
			set { mDecimalSeparator = value; }
		}

		/// <summary>
		/// Encoding used when handling the CSV files.
		/// </summary>
		public Encoding Encoding
		{
			get { return mEncoding; }
			set { mEncoding = value; }
		}

		/// <summary>Should blank lines in the source file be left out of the final result?</summary>
		public bool IgnoreEmptyLines
		{
			get { return mIgnoreEmptyLines; }
			set { mIgnoreEmptyLines = value; }
		}

		ConvertHelpers.DecimalConverter mDecimalConv;
		ConvertHelpers.DoubleConverter mDoubleConv;
		ConvertHelpers.SingleConverter mSingleConv;
		ConvertHelpers.DateTimeConverter mDateConv;

		/// <summary>
		/// Convert a field to a string
		/// </summary>
		/// <param name="o">object we want to convert</param>
		/// <returns>string representation of the string</returns>
		internal string ValueToString(object o)
		{
			if (mDecimalConv == null)
			{
				mDecimalConv = new ConvertHelpers.DecimalConverter(DecimalSeparator);
				mDoubleConv = new ConvertHelpers.DoubleConverter(DecimalSeparator);
				mSingleConv = new ConvertHelpers.SingleConverter(DecimalSeparator);
				mDateConv= new ConvertHelpers.DateTimeConverter(DateFormat);
			}

			if (o == null)
				return string.Empty;
			else if (o is DateTime)
				return mDateConv.FieldToString(o);
			else if (o is Decimal)
				return mDecimalConv.FieldToString(o);
			else if (o is Double)
				return mDoubleConv.FieldToString(o);
			else if (o is Single)
				return mSingleConv.FieldToString(o);
			else
				return o.ToString();
		}
	}
}
