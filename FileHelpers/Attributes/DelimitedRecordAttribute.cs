

using System;

namespace FileHelpers
{
	/// <summary>Indicates that this class represents a delimited record. </summary>
	/// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes list</seealso>
	/// <seealso href="quick_start.html">Quick start guide</seealso>
	/// <seealso href="examples.html">Examples of use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DelimitedRecordAttribute : TypedRecordAttribute
	{
		internal string Separator;

    	/// <summary>Indicates that this class represents a delimited record. </summary>
	    /// <param name="delimiter">The separator string used to split the fields of the record.</param>
    	public DelimitedRecordAttribute(string delimiter)
	    {
		    if (Separator != String.Empty)
			    this.Separator = delimiter;
    		else
	    		throw new ArgumentException("Given delimiter cannot be <> \"\"","delimiter");
	    }
	}
}