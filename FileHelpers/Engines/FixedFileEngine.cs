using System;

namespace FileHelpers
{

	public sealed class FixedFileEngine : FileHelperEngine
	{
		#region "  Constructor  "

		public FixedFileEngine(Type recordType)
			: base(recordType)
		{
			if (mRecordInfo.mFields[0] is FixedLengthField  == false)
				throw new BadUsageException("The FixedFileEngine only accepts Record Types marked with FixedLengthRecord attribute");
		}

		#endregion

		
		/// <summary>Allow changes in the record layout like delimiters and others settings.</summary>
		public new FixedRecordOptions Options
		{
			get { return (FixedRecordOptions) mOptions; }
			
		}
	}
}
