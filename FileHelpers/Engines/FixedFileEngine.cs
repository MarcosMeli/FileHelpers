

using System;
using System.Diagnostics;
using System.Text;
using FileHelpers.Options;

namespace FileHelpers
{

	/// <summary>
	/// Is a version of the <see cref="FileHelperEngine"/> exclusive for 
	/// fixed length records that allow you to change the delimiter an other options at runtime
	/// </summary>
	/// <remarks>
	/// Useful when you need to export or import the same info with little different options.
	/// </remarks>
    [DebuggerDisplay("FixedFileEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
    public sealed class FixedFileEngine : FileHelperEngine
	{

        #region "  Constructor  "

		/// <summary>
		/// Creates a version of the <see cref="FileHelperEngine"/> exclusive for 
		/// fixed length records that allow you to change the delimiter an other options at runtime
		/// </summary>
		/// <remarks>
		/// Useful when you need to export or import the same info with little different options.
		/// </remarks>
		/// <param name="recordType">The record mapping class.</param>
		public FixedFileEngine(Type recordType)
			: base(recordType)
		{
			if (mRecordInfo.IsDelimited)
				throw new BadUsageException("The FixedFileEngine only accepts Record Types marked with FixedLengthRecord attribute");
		}

        public FixedFileEngine(Type recordType, Encoding encoding)
            : this(recordType)
        {
            Encoding = encoding;
        }

		#endregion
		
		/// <summary>Allow changes some fixed length options and others common settings.</summary>
		public new FixedRecordOptions Options
		{
            get { return (FixedRecordOptions)base.Options; }
		}
	}


	/// <summary>
	/// Is a version of the <see cref="FileHelperEngine"/> exclusive for 
	/// fixed length records that allow you to change the delimiter an other options at runtime
	/// </summary>
	/// <remarks>
	/// Useful when you need to export or import the same info with little different options.
	/// </remarks>
    [DebuggerDisplay("FixedFileEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
    public sealed class FixedFileEngine<T>
        : FileHelperEngine<T>
        where T: class
	{

	#region "  Constructor  "

		/// <summary>
		/// Creates a version of the <see cref="FileHelperEngine"/> exclusive for 
		/// fixed length records that allow you to change the delimiter an other options at runtime
		/// </summary>
		/// <remarks>
		/// Useful when you need to export or import the same info with little different options.
		/// </remarks>
		public FixedFileEngine()
			: base()
		{
			if (mRecordInfo.IsDelimited)
				throw new BadUsageException("The FixedFileEngine only accepts Record Types marked with FixedLengthRecord attribute");
		}

        public FixedFileEngine(Encoding encoding)
            : this()
        {
            Encoding = encoding;
        }

	#endregion

		
		/// <summary>Allow changes some fixed length options and others common settings.</summary>
		public new FixedRecordOptions Options
		{
            get { return (FixedRecordOptions)base.Options; }
		}
	}
}
