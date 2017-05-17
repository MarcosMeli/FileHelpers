namespace FileHelpers
{
    /// <summary>
    /// Base class of all messages
    /// </summary>
    /// <remarks>
    /// Messages may have substitution of $var$ applied
    /// </remarks>
    internal abstract class MessageBase
    {
        /// <summary>
        /// Create a message for given text
        /// </summary>
        /// <param name="text">Text of the message, may have $var$ variables</param>
        protected MessageBase(string text)
        {
            SourceText = text;
        }

        /// <summary>
        /// Base of messages without substitution
        /// </summary>
        protected string SourceText { get; private set; }

        /// <summary>
        /// Message text after substitution happens
        /// </summary>
        public string Text
        {
            get { return GenerateText(); }
        }

        /// <summary>
        /// Convert the source text using substitution
        /// </summary>
        /// <returns></returns>
        protected abstract string GenerateText();

        /// <summary>
        /// Send message out with conversion applied
        /// </summary>
        /// <returns>message after substitution</returns>
        public override sealed string ToString()
        {
            return Text;
        }
    }
}