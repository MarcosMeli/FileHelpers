#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System.Reflection;

namespace FileHelpers.Fields
{
	internal sealed class FieldFactory
	{
		#region "  Avoid Creation  "
		private FieldFactory()
		{}
		#endregion

		public static FieldBase CreateField(FieldInfo fi, TypedRecordAttribute recordAttribute)
		{

			// If ignored, return null
			if (fi.IsDefined(typeof(FieldIgnoredAttribute), true))
				return null;

			FieldBase res = null;

			FieldAttribute[] attributes;
			FieldAttribute fieldAttb;

			attributes = (FieldAttribute[]) fi.GetCustomAttributes(typeof (FieldAttribute), true);

			// CHECK USAGE ERRORS !!!

			if (attributes.Length > 1)
				throw new BadUsageException("The field: " + fi.Name + " has more than one FieldAttribute (left only one or none)");

			if (attributes.Length == 0 && recordAttribute is FixedLengthRecordAttribute)
				throw new BadUsageException("The record class marked with the FixedLengthRecord attribute must include a FixedLength attribute in each field.");

			if (recordAttribute is DelimitedRecordAttribute && fi.IsDefined(typeof (FieldAlignAttribute), true))
				throw new BadUsageException("The AlignAttribute is only valid for fixed length records and are used only for write purpouse.");


			// PROCESS IN NORMAL CONDITIONS

			if (attributes.Length > 0)
			{
				fieldAttb = attributes[0];

				if (fieldAttb is FieldFixedLengthAttribute)
				{
					if (recordAttribute is DelimitedRecordAttribute)
						throw new BadUsageException("The FixedLengthAttribute is only for the FixedLengthRecords not for the delimited ones.");


					FieldFixedLengthAttribute attb = ((FieldFixedLengthAttribute) fieldAttb);

					FieldAlignAttribute[] alignAttbs = (FieldAlignAttribute[]) fi.GetCustomAttributes(typeof (FieldAlignAttribute), true);
					FieldAlignAttribute align = null;

					if (alignAttbs.Length > 0)
						align = alignAttbs[0];

					res = new FixedLengthField(fi, attb.Length, align);
				}
				else if (fieldAttb is FieldDelimiterAttribute)
				{
					if (recordAttribute is FixedLengthRecordAttribute)
						throw new BadUsageException("The DelimitedAttribute is only for DelimitedRecords not for the fixed ones.");

					res = new DelimitedField(fi, ((FieldDelimiterAttribute) fieldAttb).Separator);

				}
				else
					throw new BadUsageException("Custom TypedRecords not currently supported.");
			}
			else // attributes.Length == 0
			{
				if (recordAttribute is DelimitedRecordAttribute)
					res = new DelimitedField(fi, ((DelimitedRecordAttribute) recordAttribute).Separator);
			}

			//-----  TRIMMING

			if (res != null)
			{
				object[] trim = fi.GetCustomAttributes(typeof (FieldTrimAttribute), true);
				if (trim.Length > 0)
				{
//					if (res.FieldType != typeof(string))
//						throw new BadUsageException("Trim attribute can only be applied to a string or date.")

					res.mTrimMode = ((FieldTrimAttribute) trim[0]).TrimMode;
					res.mTrimChars = ((FieldTrimAttribute) trim[0]).TrimChars;
				}

			}

			if (res != null)
			{
				FieldQuotedAttribute[] quotedAttributes = (FieldQuotedAttribute[]) fi.GetCustomAttributes(typeof (FieldQuotedAttribute), true);
				if (quotedAttributes.Length > 0)
				{
					if (res is FixedLengthField)
						throw new BadUsageException("The QuotedAttribute can't be used in FixedLength fields.");

					((DelimitedField) res).mQuoteChar = quotedAttributes[0].QuoteChar;
				}

			}

			return res;
		}
	}
}