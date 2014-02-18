using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>
    /// Validate that the identifier is valid for the language
    /// </summary>
    internal static class ValidIdentifierValidator
    {
        /// <summary>
        /// Validate that the identifier is valid for the language
        /// </summary>
        /// <param name="id">Name of identifier</param>
        /// <returns>Is Valid</returns>
        internal static bool ValidIdentifier(string id)
        {
            return ValidIdentifier(id, false);
        }

        /// <summary>
        /// Validate that the identifier is valid for the language
        /// </summary>
        /// <param name="id">Name of identifier</param>
        /// <param name="isType">Is it a type statement, allows for dots, ? for nullable, etc</param>
        /// <returns>Is Valid</returns>
        internal static bool ValidIdentifier(string id, bool isType)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            if (char.IsLetter(id[0]) == false &&
                id[0] != '_')
                return false;

            for (int i = 1; i < id.Length; i++) {
                if (isType) {
                    if (id[i] == '.' ||
                        id[i] == '<' ||
                        id[i] == '>' ||
                        id[i] == '?' ||
                        id[i] == ',')
                        continue;
                }

                if (id[i] != '_' &&
                    char.IsLetterOrDigit(id[i]) == false)
                    return false;
            }

            return true;
        }
    }
}