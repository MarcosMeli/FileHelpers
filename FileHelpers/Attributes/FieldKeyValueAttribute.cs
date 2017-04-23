using System;
using System.Collections.Generic;

namespace FileHelpers
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class FieldKeyValueAttribute : Attribute
    {
        public KeyValuePair<string,string> KeyValue { get; private set; }

        public FieldKeyValueAttribute(object value, object key)
        {
            KeyValue = new KeyValuePair<string, string>(value.ToString(), key.ToString());
        }
    }
}
