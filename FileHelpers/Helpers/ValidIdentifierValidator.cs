    using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers
{
    internal static class ValidIdentifierValidator
    {
        internal static bool ValidIdentifier(string id)
        {
            return ValidIdentifier(id, false);
        }

        internal static bool ValidIdentifier(string id, bool isType)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            if (Char.IsLetter(id[0]) == false && id[0] != '_')
                return false;

            for (int i = 1; i < id.Length; i++)
            {
                if (isType)
                {
                    if (id[i] == '.' || id[i] == '<' || id[i] == '>' || id[i] == '?' || id[i] == ',')
                        continue;
                }

                if (id[i] != '_' && Char.IsLetterOrDigit(id[i]) == false)
                    return false;
            }

            return true;
        }
    }
}
