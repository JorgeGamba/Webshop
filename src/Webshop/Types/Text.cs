using System;

namespace Webshop.Types
{
    /// <summary>
    /// An example of dealing with the primitive obsessesion.
    /// Semantically it is more a value than a whole thing.
    /// </summary>
    public struct Text
    {
        private Text(string rawText)
        {
            Value = rawText;
        }

        public string Value { get; }

        public static bool TryCreate(string rawText, out Text result)
        {
            result = new Text(rawText);
            return !string.IsNullOrWhiteSpace(rawText);
        }
    }
}