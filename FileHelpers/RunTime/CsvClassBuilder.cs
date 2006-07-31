using System;

namespace FileHelpers.RunTime
{
	/// <summary>Used to create classes that maps to CSV records (can be quoted, multipleline quoted, etc).</summary>
	public sealed class CsvClassBuilder: DelimitedClassBuilder
	{

		/// <summary>Creates a new DelimitedClassBuilder.</summary>
		/// <param name="className">The valid class name.</param>
		public CsvClassBuilder(string className): this(className, ",")
		{}

		/// <summary>Creates a new DelimitedClassBuilder.</summary>
		/// <param name="className">The valid class name.</param>
		/// <param name="delimiter">The delimiter for that class.</param>
		public CsvClassBuilder(string className, string delimiter): this(className, delimiter, 0)
		{}

		/// <summary>Creates a new DelimitedClassBuilder.</summary>
		/// <param name="className">The valid class name.</param>
		/// <param name="delimiter">The delimiter for that class.</param>
		/// <param name="numberOfFields">The number of fields in each record.</param>
		public CsvClassBuilder(string className, string delimiter, int numberOfFields): this(className, delimiter,  numberOfFields, "Field")
		{}

		/// <summary>Creates a new DelimitedClassBuilder.</summary>
		/// <param name="className">The valid class name.</param>
		/// <param name="delimiter">The delimiter for that class.</param>
		/// <param name="numberOfFields">The number of fields in each record.</param>
		/// <param name="fieldPrefix">The prefix for the automatic created fields.</param>
		public CsvClassBuilder(string className, string delimiter, int numberOfFields, string fieldPrefix): base(className, delimiter)
		{
			IgnoreFirstLines = 1;
			AddFields(numberOfFields, fieldPrefix);
		}


		/// <summary>Add a new Delimited field to the current class.</summary>
		/// <param name="fieldName">The Name of the field.</param>
		/// <param name="fieldType">The Type of the field.</param>
		/// <returns>The just created field.</returns>
		public new DelimitedFieldBuilder AddField(string fieldName, Type fieldType)
		{
			return AddField(fieldName, fieldType);
		}

		/// <summary>Add a new Delimited field to the current class.</summary>
		/// <param name="fieldName">The Name of the field.</param>
		/// <param name="fieldType">The Type of the field.</param>
		/// <returns>The just created field.</returns>
		public new DelimitedFieldBuilder AddField(string fieldName, string fieldType)
		{
			DelimitedFieldBuilder fb = new DelimitedFieldBuilder(fieldName, fieldType);
			if (mFields.Count > 1)
			{
				fb.FieldOptional = true;
				fb.FieldQuoted = true;
				fb.QuoteMode = QuoteMode.OptionalForBoth;
				fb.QuoteMultiline = MultilineMode.AllowForBoth;
			}

			AddFieldInternal(fb);
			return fb;
		}

		/// <summary>Add a new Delimited field to the current class.</summary>
		/// <param name="fieldName">The Name of the field.</param>
		/// <returns>The just created field.</returns>
		public DelimitedFieldBuilder AddField(string fieldName)
		{
			return AddField(fieldName, "System.String");
		}

		/// <summary>
		/// Adds to the class the specified number of fileds.
		/// </summary>
		/// <param name="number">The number of fileds to add.</param>
		public void AddFields(int number)
		{
			AddFields(number, "Field");
		}

		/// <summary>
		/// Adds to the class the specified number of fileds.
		/// </summary>
		/// <param name="prefix">The prefix used for the fields.</param>
		/// <param name="number">The number of fileds to add.</param>
		public void AddFields(int number, string prefix)
		{
			int initFields = mFields.Count;

			for(int i = 0; i < number; i++)
			{
				int current = i + initFields + 1;
				AddField(prefix + (current).ToString());
				DelimitedFieldBuilder fb = new DelimitedFieldBuilder(prefix + (current).ToString(), "System.String");

				AddFieldInternal(fb);
			}
		}


	}
}
