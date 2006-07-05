

#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

namespace FileHelpers
{
	/// <summary>Indicates the convertion used in the <see cref="T:FileHelpers.FieldConverterAttribute"/>.</summary>
	public enum ConverterKind
	{

		/// <summary>Null Converter.</summary>
		None = 0,
		/// <summary>
		/// <para>Convert from/to <b>Date</b> values.</para>
		/// <para>Params: arg1 is the <b>string</b> with the date format.</para>
		/// </summary>
		Date,
		/// <summary>Convert from/to <b>Boolean</b> values.</summary>
		Boolean,
		/// <summary>Convert from/to <b>Byte</b> values.</summary>
		Byte,
		/// <summary>Convert from/to <b>Int16</b> values.</summary>
		Int16,
		/// <summary>Convert from/to <b>Int32</b> values.</summary>
		Int32,
		/// <summary>Convert from/to <b>Int64</b> values.</summary>
		Int64,
		/// <summary>Convert from/to <b>Decimal</b> values.</summary>
		Decimal,
		/// <summary>Convert from/to <b>Double</b> values.</summary>
		Double,
		/// <summary>Convert from/to <b>Single</b> values.</summary>
		Single
	}
}