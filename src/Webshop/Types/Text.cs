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
            return MeetsTheConstraints(rawText);
        }

        public static Text Create(string rawText)
        {
            if (MeetsTheConstraints(rawText))
                return new Text(rawText);

            throw new Exception("The provided text does not meet the rules.");
        }


        private static bool MeetsTheConstraints(string rawText) => !string.IsNullOrWhiteSpace(rawText);
    }
}