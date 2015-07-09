using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Indicates that the engine must ignore commented lines while reading.</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IgnoreCommentedLinesAttribute : Attribute
    {
        /// <summary>
        /// Comment marker string
        /// </summary>
        public string CommentMarker { get; private set; }

        /// <summary>
        /// Can the comment marker have preceding spaces
        /// </summary>
        public bool AnyPlace { get; private set; }

        /// <summary>Indicates that the engine will ignore commented lines while reading.
        /// (The Comment Marker can have any number of spaces or tabs to the left)</summary>
        /// <param name="commentMarker">The comment marker used to ignore the lines</param>
        public IgnoreCommentedLinesAttribute(string commentMarker)
            : this(commentMarker, true) {}

        /// <summary>Indicates that the engine will ignore commented lines while reading.</summary>
        /// <param name="commentMarker">The comment marker used to ignore the lines</param>
        /// <param name="anyPlace">Indicates if the comment can have spaces or tabs to the left (true by default)</param>
        public IgnoreCommentedLinesAttribute(string commentMarker, bool anyPlace)
        {
            if (commentMarker == null ||
                commentMarker.Trim().Length == 0)
                throw new BadUsageException("The comment string parameter can't be null or empty.");

            CommentMarker = commentMarker.Trim();
            AnyPlace = anyPlace;
        }
    }
}