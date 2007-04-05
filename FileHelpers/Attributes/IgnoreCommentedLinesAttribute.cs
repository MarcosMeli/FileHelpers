#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates that the engine must ignore commented lines while reading.</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class IgnoreCommentedLinesAttribute : Attribute
	{
		internal string mCommentMarker;
		internal bool mAnyPlace = true;

		/// <summary>Indicates that the engine must ignore commented lines while reading. (The Comment Marker can appear in any place with spaces or tabs at his left)</summary>
		/// <param name="commentMarker">The comment marker used to ignore the lines</param>
		public IgnoreCommentedLinesAttribute(string commentMarker): this(commentMarker, true)
		{
		}

		/// <summary>Indicates that the engine must ignore commented lines while reading.</summary>
		/// <param name="commentMarker">The comment marker used to ignore the lines</param>
        /// <param name="anyPlace">Indicates if the comment can have spaces or tabs at left (true by default)</param>
		public IgnoreCommentedLinesAttribute(string commentMarker, bool anyPlace)
		{
			if (commentMarker == null ||  commentMarker.Trim().Length == 0)
				throw new BadUsageException("The comment string parameter cant be null or empty.");
			
			mCommentMarker = commentMarker.Trim();
            mAnyPlace = anyPlace;
		}

	}
}
