

#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

namespace FileHelpers
{
	/// <summary>
	/// Base class to provide bidirectional
	/// Field - String convertion.
	/// </summary>
	public abstract class ConverterBase
	{
		/// <summary>
		/// Convert a string in the file to a field value.
		/// </summary>
		/// <param name="from">The string to convert.</param>
		/// <returns>The field value.</returns>
		public abstract object StringToField(string from);

		/// <summary>
		/// Convert a field value to an string to write this to the file.
		/// </summary>
		/// <remarks>The basic implementation performs a: from.ToString();</remarks>
		/// <param name="from">The field values to convert.</param>
		/// <returns>The string representing the field value.</returns>
		public virtual string FieldToString(object from)
		{
			if (from == null)
				return string.Empty;
			else
				return from.ToString();
		}

		/// <summary>If the class retures false the engines don´t pass null values to the converter. If true the engines pass all the values to the converter.</summary>
		protected internal virtual bool CustomNullHandling
		{
			get { return false; }
		}
	}
}