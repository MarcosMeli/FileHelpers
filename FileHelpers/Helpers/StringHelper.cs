#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.ComponentModel;
using System.Text;

namespace FileHelpers
{
	/// <summary>Helper Class to manipulate Strings.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class StringHelper
	{

		#if ! MINI
			public static readonly string NewLine = Environment.NewLine;
		#else
			public static readonly string NewLine = "\r\n";
		#endif


		private StringHelper()
		{
		}

		#region "  ExtractQuotedString  "

		/// <summary>Used to extract a quoted string and de-escape the quote char.</summary>
		/// <param name="source">Source string</param>
		/// <param name="quoteChar">The quoted char.</param>
		/// <param name="index">An output parameter of the index of the end of the string.</param>
		/// <returns>The estracted string</returns>
		internal static ExtractedInfo ExtractQuotedString(string source, ForwardReader reader, char quoteChar)
		{
			StringBuilder res = new StringBuilder(32);
			int lines = 0;

			bool beginEscape = false;

			if (reader == null)
				throw new BadUsageException("The reader can´t be null");

			if (source ==null || source.Length == 0)
				throw new BadUsageException("An empty String found and can be parsed like a QuotedString try to use SafeExtractQuotedString");


			if (source[0] != quoteChar)
				throw new BadUsageException("The source string not begins with the quote char: " + quoteChar);

			int i = 1;
			bool mustContinue = true;

			while (mustContinue && source != null)
			{
				while (i < source.Length)
				{
					if (source[i] == quoteChar)
					{
						if (beginEscape == true)
						{
							beginEscape = false;
							res.Append(quoteChar);
						}
						else
						{
							beginEscape = true;
						}
					}
					else
					{
						if (beginEscape)
						{
							// End of the String
							ExtractedInfo ei = new ExtractedInfo(res.ToString(), i, lines);
							if (lines != 0)
								ei.NewRestOfLine = source.Substring(i);
							return ei;

						}
						else
						{
							res.Append(source[i]);
						}
					}
					i++;
				}


				if (beginEscape)
				{
					return new ExtractedInfo(res.ToString(), i+1, lines);
				}
				else
				{
					source = reader.ReadNextLine();
					res.Append(StringHelper.NewLine);
					lines++;
					i = 0;
				}
			}

			throw new BadUsageException("The current field has an UnClosed quoted string. Complete Filed String: " + res.ToString());
		}

		#endregion

		#region "  CreateQuotedString  "

		/// <summary>Used to generate a quoted string and escape the quote char.</summary>
		/// <param name="source">Source string</param>
		/// <param name="quoteChar">The quoted char.</param>
		/// <returns>The quoted string</returns>
		public static string CreateQuotedString(string source, char quoteChar)
		{
			if (source == null) source = string.Empty;

			string escapedString = source.Replace(quoteChar.ToString(), quoteChar.ToString() + quoteChar);
			return quoteChar + escapedString + quoteChar;
		}

		#endregion

		#region "  RemoveBlanks  "

		/// <summary>Removes the blank characters (space, tabs, etc) from the string.</summary>
		/// <param name="source">The string to be parsed.</param>
		/// <returns>A string thats results of remove the blanks chars from the source string.</returns>
		public static string RemoveBlanks(string source)
		{
			if (source == null || source.Length == 0)
				return source;

			StringBuilder sb = new StringBuilder(source.Length);
			int i = 0;
			while (i < source.Length && Char.IsWhiteSpace(source[i]))
			{
				i++;
			}

			if (i < source.Length && (source[i] == '+' || source[i] == '-'))
			{
				sb.Append(source[i]);
				i++;
				while (i < source.Length && Char.IsWhiteSpace(source[i]))
				{
					i++;
				}
			}

			if (i < source.Length)
				sb.Append(source.Substring(i));

			return sb.ToString();
		}

		#endregion

		#region "  ExtractQuotedString  "

		/// <summary>Used to extract a quoted string and de-escape the quote char.</summary>
		/// <param name="source">Source string</param>
		/// <param name="quoteChar">The quoted char.</param>
		/// <param name="index">An output parameter of the index of the end of the string.</param>
		/// <returns>The estracted string</returns>
		public static string ExtractQuotedString(string source, char quoteChar, out int index)
		{
			StringBuilder res = new StringBuilder(32);
			bool beginEscape = false;




			if (source == null || source.Length == 0)


				throw new BadUsageException("An empty String found and can be parsed like a QuotedString try to use SafeExtractQuotedString");


			if (source[0] != quoteChar)
				throw new BadUsageException("The source string not begins with the quote char: " + quoteChar);

			index = 0;
			int i = 1;
			while (i < source.Length)
			{
				if (source[i] == quoteChar)
				{
					if (beginEscape == true)
					{
						beginEscape = false;
						res.Append(quoteChar);
					}
					else
					{
						beginEscape = true;
					}
				}
				else
				{
					if (beginEscape)
					{
						// End of the String
						index = i;
						return res.ToString();
					}
					else
					{
						res.Append(source[i]);
					}
				}

				i++;
			}
			if (beginEscape)
			{
				index = i;
				return res.ToString();
			}
			else
				throw new BadUsageException("The current field has an UnClosed quoted string. Complete line: " + source);
		}

		#endregion

	}
}