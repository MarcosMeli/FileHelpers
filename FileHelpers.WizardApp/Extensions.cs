using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileHelpers.WizardApp
{
    /// <summary>
    /// Static extension classes for the wizard application
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Convert from FileHelpers language type to the fireball language enum
        /// </summary>
        /// <param name="pLanguage">FileHelpers NetLanguage</param>
        /// <returns>FireBall SyntaxLanguage</returns>
        internal static Fireball.CodeEditor.SyntaxFiles.SyntaxLanguage ToFireball(this FileHelpers.NetLanguage pLanguage)
        {
            switch (pLanguage)
            {
                case FileHelpers.NetLanguage.CSharp:
                    return Fireball.CodeEditor.SyntaxFiles.SyntaxLanguage.CSharp;
                case NetLanguage.VbNet:
                    return Fireball.CodeEditor.SyntaxFiles.SyntaxLanguage.VBNET;
                default:
                    throw new Exception("Unable to translate language type "+ pLanguage);
            }
        }
    }
}
