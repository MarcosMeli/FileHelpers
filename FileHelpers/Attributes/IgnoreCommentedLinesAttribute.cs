using System;

namespace FileHelpers
{
	/// <summary>Indicates that the engine must ignore commented lines while reading.</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class IgnoreCommentedLinesAttribute : Attribute
	{
        /// <summary>
        /// Comment marker string
        /// </summary>
		internal string mCommentMarker;

        /// <summary>
        /// Can the comment marker have preceding spaces
        /// </summary>
		internal bool mAnyPlace = true;

		/// <summary>Indicates that the engine will ignore commented lines while reading.
        /// (The Comment Marker can have any number of spaces or tabs to the left)</summary>
		/// <param name="commentMarker">The comment marker used to ignore the lines</param>
		public IgnoreCommentedLinesAttribute(string commentMarker): this(commentMarker, true)
		{
		}

		/// <summary>Indicates that the engine will ignore commented lines while reading.</summary>
		/// <param name="commentMarker">The comment marker used to ignore the lines</param>
        /// <param name="anyPlace">Indicates if the comment can have spaces or tabs to the left (true by default)</param>
		public IgnoreCommentedLinesAttribute(string commentMarker, bool anyPlace)
		{
			if (commentMarker == null ||  commentMarker.Trim().Length == 0)
				throw new BadUsageException("The comment string parameter can't be null or empty.");
			
			mCommentMarker = commentMarker.Trim();
            mAnyPlace = anyPlace;
		}
	}
}
