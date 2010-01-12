using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers
{
    internal abstract class MessageBase
    {
        protected MessageBase(string text)
        {
            SourceText = text;
        }

        protected string SourceText { get; private set; }

        public string Text
        {
            get
            {
                return GenerateText();
            }
        }

        protected abstract string GenerateText();

        public sealed override string ToString()
        {
            return Text;
        }

    }
}
