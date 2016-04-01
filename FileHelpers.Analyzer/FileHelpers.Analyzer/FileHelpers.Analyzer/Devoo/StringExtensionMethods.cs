using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace System
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class StringExtensionMethods
    {
        public static bool EqualsIgnoreCase(this string text, string toCheck)
        {
            return string.Equals(text, toCheck, StringComparison.OrdinalIgnoreCase);
        }

        public static bool EqualsToAnyIgnoreCase(this string text, params string[] toCheck)
        {
            if (toCheck == null)
                return false;

            foreach (var t in toCheck)
            {
                if (string.Equals(text, t, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        //public static bool EqualsIgnoreCase(this char c, char toCheck)
        //{
        //    var cup = char.ToUpper(c);
        //    var toCheckUp = char.ToUpper(toCheck);
        //    return cup.Equals(toCheckUp);
        //}

        //public static bool EqualsToAnyIgnoreCase(this char c, params char[] toCheck)
        //{
        //    var clow = char.ToUpper(c);

        //    for (int i = 0; i < toCheck.Length; i++)
        //    {
        //        if (clow == char.ToUpper(toCheck[i]))
        //            return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// Indica si el string contine el texto toCheck lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toCheck">Texto a busar</param>
        /// <returns>True if the string contains the text toCheck ignoring case.</returns>
        public static bool ContainsIgnoreCase(this string text, string toCheck)
        {
            if (toCheck.IsNullOrEmpty())
                throw new ArgumentException("El parametro 'toCheck' es vacio");

            return text.IndexOfIgnoreCase(toCheck) >= 0;
        }

        public static int IndexWholePhrase(this string text, string toCheck, int startIndex = 0)
        {
            if (toCheck.IsNullOrEmpty())
                throw new ArgumentException("El parametro 'toCheck' es vacio");

            //var startIndex = 0;
            while (startIndex <= text.Length)
            {
                var index = text.IndexOfIgnoreCase(startIndex, toCheck);
                if (index < 0)
                    return -1;

                var indexPreviousCar = index - 1;
                var indexNextCar = index + toCheck.Length;
                if ((index == 0
                     || !Char.IsLetter(text[indexPreviousCar]))
                    && (index + toCheck.Length == text.Length
                        || !Char.IsLetter(text[indexNextCar])))
                    return index;

                startIndex = index + toCheck.Length;
            }
            return -1;
        }


        /// <summary>
        /// Take all the words in the input string and separate them.
        /// </summary>
        /// 
        /// 

        private static readonly Regex mSplitWords = new Regex(@"\W+");

        public static string[] SplitInWords(this string s)
        {

            //
            // Split on all non-word characters.
            // ... Returns an array of all the words.
            //
            return mSplitWords.Split(s);
            // @      special verbatim string syntax
            // \W+    one or more non-word characters together
        }

        public static List<string> SplitInWordsLongerThan(this string s, int wordlength)
        {
            var res = new List<string>();
            var splitwords = mSplitWords.Split(s);

            foreach (var word in splitwords)
            {
                if (word.Length > wordlength)
                {
                    res.Add(word);
                }
            }

            return res;
        }

        public static int TotalWords(this string text)
        {
            text = text.Trim();

            if (text.IsNullOrEmpty())
                return 0;

            var res = 0;

            var prevCharIsSeparator = true;
            foreach (var c in text)
            {
                if (char.IsSeparator(c) || char.IsPunctuation(c) || char.IsWhiteSpace(c))
                {
                    if (!prevCharIsSeparator)
                        res++;
                    prevCharIsSeparator = true;
                }
                else
                    prevCharIsSeparator = false;
            }
            if (!prevCharIsSeparator)
                res++;

            return res;
        }

        public static IEnumerable<string> EnumerateLines(this string s)
        {
            var index = 0;

            while (true)
            {
                var newIndex = s.IndexOf(Environment.NewLine, index, StringComparison.Ordinal);
                if (newIndex < 0)
                {
                    if (s.Length > index)
                        yield return s.Substring(index);

                    yield break;
                }

                var currentString = s.Substring(index, newIndex - index);
                index = newIndex + 2;

                yield return currentString;
            }
        }


        public static int CountLines(this string s)
        {
            var index = 0;
            var lines = 0;

            while (true)
            {
                var newIndex = s.IndexOf(Environment.NewLine, index, StringComparison.Ordinal);
                if (newIndex < 0)
                {
                    if (s.Length > index)
                        lines++;

                    return lines;
                }

                index = newIndex + 2;
                lines++;
            }
        }

        public static string[] SplitInLines(this string s)
        {
            return s.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public static T[] SplitInLinesTyped<T>(this string s) where T : IComparable
        {
            return SplitTyped<T>(s, Environment.NewLine);
        }

        public static string[] SplitInLinesRemoveEmptys(this string s)
        {
            return s.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }


        public static Tuple<string, string> SplitByIndex(this string text, int index)
        {
            if (text.IsNullOrEmpty())
                return new Tuple<string, string>("", "");

            if (index >= text.Length)
                return new Tuple<string, string>(text, "");

            if (index <= 0)
                return new Tuple<string, string>("", text);

            return new Tuple<string, string>(text.Substring(0, index - 1), text.Substring(index - 1));
        }

        public static Tuple<string, string> SplitWordsByIndex(this string text, int index)
        {
            var splitByIndex = text.SplitByIndex(index);
            var res = new Tuple<string, string>(splitByIndex.Item1, splitByIndex.Item2);

            var wordsInItem1 = res.Item1.SplitInWords();
            res = new Tuple<string, string>(wordsInItem1.Take(wordsInItem1.Length - 1).JoinToString(" ").Trim(), wordsInItem1.Last() + splitByIndex.Item2);

            return res;
        }

        public static bool ContainsWholeWord(this string text, string toCheck)
        {
            if (text.IsNullOrEmpty())
                return false;

            if (toCheck.IsNullOrEmpty())
                throw new ArgumentException("El parametro 'toChek' es vacio");

            var partes = text.SplitInWords();
            foreach (var parte in partes)
            {
                if (parte.EqualsIgnoreCase(toCheck))
                    return true;
            }
            return false;
        }

        public static bool ContainsAnyWholeWord(this string text, params string[] toCheck)
        {
            if (text.IsNullOrEmpty())
                return false;

            if (toCheck == null || toCheck.Length == 0)
                throw new ArgumentException("El parametro 'toChek' es vacio");

            var partes = text.SplitInWords();
            foreach (var parte in partes)
            {
                foreach (var check in toCheck)
                {
                    if (parte.EqualsIgnoreCase(check))
                        return true;
                }
            }
            return false;
        }

        public static bool ContainsWholePhrase(this string text, string toCheck)
        {
            if (toCheck.IsNullOrEmpty())
                throw new ArgumentException("El parametro 'toChek' es vacio");

            var startIndex = 0;
            while (startIndex <= text.Length)
            {
                var index = text.IndexOfIgnoreCase(startIndex, toCheck);
                if (index < 0)
                    return false;

                var indexPreviousCar = index - 1;
                var indexNextCar = index + toCheck.Length;
                if ((index == 0
                     || !Char.IsLetter(text[indexPreviousCar]))
                    && (index + toCheck.Length == text.Length
                        || !Char.IsLetter(text[indexNextCar])))
                    return true;

                startIndex = index + toCheck.Length;
            }
            return false;
        }

        public static bool ContainsWholePhraseAny(this string text, params string[] toCheck)
        {
            foreach (var phrase in toCheck)
            {
                if (text.ContainsWholePhrase(phrase)) return true;
            }
            return false;
        }

        public static bool ContainsWholePhraseAll(this string text, params string[] toCheck)
        {
            foreach (var phrase in toCheck)
            {
                if (!text.ContainsWholePhrase(phrase)) return false;
            }
            return true;
        }

        public static string FindFirstPhrase(this string text, params string[] phrasesToCheck)
        {
            if (phrasesToCheck == null || phrasesToCheck.Length == 0)
                throw new ArgumentException("El parametro 'phrasesToCheck' es vacio");

            return phrasesToCheck.FirstOrDefault(checking => text.ContainsWholePhrase(checking));
        }

        public static bool IsSameWords(this string text, string check)
        {
            if (check.IsNullOrEmpty())
                return text.IsNullOrEmpty();

            var wordsText = text.SplitInWords();
            var wordsCheck = check.SplitInWords();

            var wordsTextNotInCheck = wordsText.FindAll(x => !x.IsOn(wordsCheck));
            if (wordsTextNotInCheck.Length > 0)
                return false;

            var wordsCheckNotInText = wordsCheck.FindAll(x => !x.IsOn(wordsText));
            if (wordsCheckNotInText.Length > 0)
                return false;

            return true;
        }


        /// <summary>
        /// Indica si el string contine algunos de los textos toCheck lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toCheck">Texto a busar</param>
        /// <returns>True if the string contains the text toCheck ignoring case.</returns>
        public static bool ContainsAnyIgnoreCase(this string text, params string[] toCheck)
        {
            if (toCheck == null || toCheck.Length == 0)
                throw new ArgumentException("El parametro 'toChek' es vacio");

            foreach (var checking in toCheck)
            {
                if (text.IndexOfIgnoreCase(checking) >= 0) return true;
            }
            return false;
        }

        /// <summary>
        /// Indica si el string contine todos los textos toCheck, lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toCheck">Texto a buscar</param>
        /// <returns>True if the string contains all the texts toCheck ignoring case.</returns>
        public static bool ContainsAllIgnoreCase(this string text, params string[] toCheck)
        {
            if (toCheck == null || toCheck.Length == 0)
                throw new ArgumentException("El parametro 'toChek' es vacio");

            foreach (var checking in toCheck)
            {
                if (text.IndexOfIgnoreCase(checking) < 0) return false;
            }
            return true;
        }

        public static bool ContainsOnlyThisChar(this string text, char toCheck)
        {
            if (text.Length == 0)
                return false;

            foreach (var t in text)
            {
                if (t != toCheck) return false;
            }
            return true;
        }



        /// <summary>
        /// Indica en que lugar el string contine el texto toCheck lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toCheck">Texto a busar</param>
        /// <returns>El indice donde aparece por primera vez la cadena toCheck ignorando el case.</returns>
        public static int IndexOfIgnoreCase(this string text, string toCheck)
        {
            return text.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase);
        }


        /// <summary>
        /// Indica el ultimo indixe donde el string contine el texto toCheck lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toCheck">Texto a busar</param>
        /// <returns>El indice donde aparece por primera vez la cadena toCheck ignorando el case.</returns>
        public static int LastIndexOfIgnoreCase(this string text, string toCheck)
        {
            return text.LastIndexOf(toCheck, StringComparison.OrdinalIgnoreCase);
        }


        /// <summary>
        /// Indica el ultimo indixe donde el string contine el texto toCheck lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toCheck">Texto a busar</param>
        /// <returns>El indice donde aparece por primera vez la cadena toCheck ignorando el case.</returns>
        public static int LastIndexOfIgnoreCase(this string text, string toCheck, int startIndex, int count)
        {
            return text.LastIndexOf(toCheck, startIndex, count, StringComparison.OrdinalIgnoreCase);
        }


        /// <summary>
        /// Indica en que lugar el string contine el texto toCheck lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startIndex">La posicion inicial de la busqueda</param>
        /// <param name="toCheck">Texto a busar</param>
        /// <returns>El indice donde aparece por primera vez la cadena toCheck ignorando el case.</returns>
        public static int IndexOfIgnoreCase(this string text, int startIndex, string toCheck)
        {
            return text.IndexOf(toCheck, startIndex, StringComparison.OrdinalIgnoreCase);
        }

        public static string FindFirstOcurrence(this string text, params string[] toCheck)
        {
            if (toCheck == null || toCheck.Length == 0)
                throw new ArgumentException("El parametro 'toCheck' es vacio");

            return toCheck.FirstOrDefault(checking => text.ContainsIgnoreCase(checking));
        }

        public static int LastIndexOfAny(this string text, params string[] toCheck)
        {
            if (toCheck == null || toCheck.Length == 0)
                throw new ArgumentException("El parametro 'toCheck' es vacio");

            var res = -1;
            foreach (var checking in toCheck)
            {
                var index = text.LastIndexOf(checking, StringComparison.OrdinalIgnoreCase);
                if (index >= 0
                    && index > res)
                    res = index;
            }
            return res;
        }


        public static string FindAndReplaceFirstOcurrence(this string text, string newValue, params string[] oldValues)
        {
            if (oldValues == null || oldValues.Length == 0)
                throw new ArgumentException("El parametro 'oldValues' es vacio");

            foreach (var oldValue in oldValues)
            {
                if (text.ContainsIgnoreCase(oldValue))
                    return text.ReplaceIgnoringCase(oldValue, newValue);
            }
            return text;
        }

        public static string FindAndInsertBeforeFirstOcurrence(this string text, string textInsert, params string[] oldValues)
        {
            if (oldValues == null || oldValues.Length == 0)
                throw new ArgumentException("El parametro 'oldValues' es vacio");

            foreach (var oldValue in oldValues)
            {
                if (text.ContainsIgnoreCase(oldValue))
                    return text.ReplaceIgnoringCase(oldValue, textInsert + oldValue);
            }
            return text;
        }

        /// <summary>
        /// Indica si el string comienza con el texto toCheck lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toCheck">Texto a busar</param>
        public static bool StartsWithIgnoreCase(this string text, string toCheck)
        {
            if (toCheck.IsNullOrEmpty())
                return true;

            if (text.IsNullOrEmpty())
                return toCheck.IsNullOrEmpty();

            return text.StartsWith(toCheck, StringComparison.OrdinalIgnoreCase);
        }

        public static bool StartsWithAnyIgnoreCase(this string text, params string[] toCheck)
        {
            return StartsWithAnyIgnoreCase(text, (IEnumerable<string>)toCheck);
        }

        public static bool StartsWithAnyIgnoreCase(this string text, IEnumerable<string> toCheck)
        {
            if (text.IsNullOrEmpty())
                return false;

            foreach (var check in toCheck)
            {
                if (text.StartsWith(check, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        public static bool EndsWithIgnoreCase(this string text, string toCheck)
        {
            if (toCheck.IsNullOrEmpty())
                return true;

            if (text.IsNullOrEmpty())
                return toCheck.IsNullOrEmpty();

            return text.EndsWith(toCheck, StringComparison.OrdinalIgnoreCase);
        }

        public static bool EndsWithAnyIgnoreCase(this string text, params string[] toCheck)
        {
            return EndsWithAnyIgnoreCase(text, (IEnumerable<string>)toCheck);
        }

        public static bool EndsWithAnyIgnoreCase(this string text, IEnumerable<string> toCheck)
        {
            if (text.IsNullOrEmpty())
                return false;

            foreach (var check in toCheck)
            {
                if (text.EndsWith(check, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        //private static readonly CultureInfo CultureHelper.CultureUSA = new CultureInfo("en-us");
        //private static CultureInfo CultureHelper.Argentina = new CultureInfo("es-ar");


        public static string ToLink(this string text, string href, string target = "")
        {
            return "<a href='" + href + "' " + (target.IsNullOrEmpty() ? "" : " target='" + target + "'") + ">" + text +
                   "</a>";
        }


        /// <summary>
        /// Remueve la cadena especificada si se encuentra
        /// </summary>
        /// <param name="text"></param>
        /// <param name="removeText">Texto a ser removido</param>
        /// <returns>Texto sin la cadena a ser removida</returns>
        public static string Remove(this string text, string removeText)
        {
            return text.Replace(removeText, String.Empty);
        }

        public static string RemoveFromIgnoreCase(this string text, string removeFromThis)
        {
            int index = text.IndexOfIgnoreCase(removeFromThis);

            if (index < 0)
                return text;
            else
                return text.Substring(0, index);
        }

        /// <summary>
        /// Determina si una cadena es Nula o Vacia
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Verdadero o Falso</returns>
        public static bool IsNullOrEmpty(this string texto)
        {
            return String.IsNullOrEmpty(texto);
        }

        /// <summary>
        /// Determina si una cadena NO es Nula o Vacia
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Verdadero o Falso</returns>
        public static bool NotIsNullOrEmpty(this string texto)
        {
            return !String.IsNullOrEmpty(texto);
        }


        /// <summary>
        /// Determina si una cadena es Nula, vacia o solo tiene caracteres blancos (tab, space, enter)
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Verdadero o Falso</returns>
        public static bool IsNullOrWhite(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return true;

            texto = texto.Trim();
            return string.IsNullOrEmpty(texto);
        }

        /// <summary>
        /// Determina si una cadena NO es Nula, vacia o solo tiene caracteres blancos (tab, space, enter)
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Verdadero o Falso</returns>
        public static bool NotIsNullOrWhite(this string texto)
        {
            return !texto.IsNullOrWhite();
        }

        /// <summary>
        /// Determina si una cadena es Vacia
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Verdadero o Falso</returns>
        public static bool IsEmpty(this string texto)
        {
            return texto.Equals(String.Empty);
        }

        public static string Substring(this string text, string startText)
        {
            int indice = text.IndexOf(startText);
            if (indice == -1) throw new ArgumentException("No se encontro: " + startText);
            return text.Substring(indice);
        }

        public static string Right(this string text, int length)
        {
            if (length < 0)
            {
                throw new ArgumentException("Length > 0", "length");
            }
            if ((length == 0) || (text == null))
            {
                return "";
            }
            int strLength = text.Length;
            if (length >= strLength)
            {
                return text;
            }
            return text.Substring(strLength - length, length);
        }

        public static string Left(this string text, int length)
        {
            if (length < 0)
            {
                throw new ArgumentException("Length > 0", "length");
            }
            if ((length == 0) || (text == null))
            {
                return "";
            }
            if (length >= text.Length)
            {
                return text;
            }
            return text.Substring(0, length);
        }

        /// <summary>
        ///  Inidica si alguno de los valores esta en la cadena
        /// </summary>
        /// <param name="text"></param>
        /// <param name="values">Valores a verificar</param>
        /// <returns>Verdadero o Falso</returns>
        public static bool Contains(this string text, string[] values)
        {
            bool contain = false;
            foreach (String s in values)
            {
                if (text.Contains(s)) contain = true;
            }
            return contain;
        }

        /// <summary>
        ///  Le hace un Trim a todas las cadenas de la lista
        /// </summary>
        /// <param name="textos"></param>
        /// <returns></returns>
        public static void TrimAll(this IList<string> textos)
        {
            for (int i = 0; i < textos.Count; i++)
            {
                textos[i] = textos[i].Trim();
            }
        }
        /// <summary>
        /// Es como llamar a String.Format pero direcmante sobre el string  "Hola {0} hoy es: {1}".Fill("Juan", 12)
        /// </summary>
        /// <param name="original"></param>
        /// <param name="values">Los mismos valores que se les manda al String.Format</param>
        /// <returns></returns>
        public static string Fill(this string original, params object[] values)
        {
            string texto =
                original.Replace("\\n", Environment.NewLine)
                        .Replace("<br>", Environment.NewLine)
                        .Replace("<BR>", Environment.NewLine);

            return string.Format(texto, values);
        }

        public static string ReplaceIgnoringCase(this string original, string oldValue, string newValue)
        {
            return Replace(original, oldValue, newValue, StringComparison.OrdinalIgnoreCase);
        }

        public static string ReplaceOnlyWholePhrase(this string original, string oldValue, string newValue)
        {
            if (original.IsNullOrEmpty()
                || oldValue.IsNullOrEmpty())
                return original;

            var index = original.IndexWholePhrase(oldValue);
            var lastIndex = 0;

            var buffer = new StringBuilder(original.Length);

            while (index >= 0)
            {
                buffer.Append(original, startIndex: lastIndex, count: index - lastIndex);
                buffer.Append(newValue);

                lastIndex = index + oldValue.Length;

                index = original.IndexWholePhrase(oldValue, startIndex: index + 1);
            }
            buffer.Append(original, lastIndex, original.Length - lastIndex);

            return buffer.ToString();
        }

        public static string ReplaceFirstOccurrence(this string original, string oldValue, string newValue)
        {
            if (oldValue.IsNullOrEmpty())
                return original;

            var index = original.IndexOfIgnoreCase(oldValue);

            if (index < 0)
                return original;

            else if (index == 0)
                return newValue + original.Substring(oldValue.Length);
            else
                return original.Substring(0, index) + newValue + original.Substring(index + oldValue.Length);
        }

        public static string ReplaceLastOccurrence(this string original, string oldValue, string newValue)
        {
            if (oldValue.IsNullOrEmpty())
                return original;

            var index = original.LastIndexOfIgnoreCase(oldValue);

            if (index < 0)
                return original;
            else if (index == 0)
                return newValue + original.Substring(oldValue.Length);
            else
                return original.Substring(0, index) + newValue + original.Substring(index + oldValue.Length);
        }

        public static string ReplaceOnlyAtEndIgnoreCase(this string original, string oldValue, string newValue)
        {
            if (oldValue.IsNullOrEmpty())
                return original;

            if (original.EndsWithIgnoreCase(oldValue))
                return original.Substring(0, original.Length - oldValue.Length) + newValue;

            return original;
        }


        public static string Replace(this string original, string oldValue, string newValue, StringComparison comparisionType)
        {
            if (original.IsNullOrEmpty())
                return original;

            string result = original;

            if (!string.IsNullOrEmpty(oldValue))
            {
                int index = -1;
                int lastIndex = 0;

                var buffer = new StringBuilder(original.Length);

                while ((index = original.IndexOf(oldValue, index + 1, comparisionType)) >= 0)
                {
                    buffer.Append(original, lastIndex, index - lastIndex);
                    buffer.Append(newValue);

                    lastIndex = index + oldValue.Length;
                }
                buffer.Append(original, lastIndex, original.Length - lastIndex);

                result = buffer.ToString();
            }

            return result;
        }

        public static string Truncate(this string original, int maxLength)
        {
            if (original.IsNullOrEmpty() || maxLength == 0)
                return string.Empty;
            if (original.Length <= maxLength)
                return original;
            else if (maxLength <= 3)
                return original.Substring(0, 2) + ".";
            else
                return original.Substring(0, maxLength - 3) + "...";
        }

        public static string ReplaceRecursive(this string original, string oldValue, string newValue)
        {
            const int MaxTries = 1000;

            string ante, res;

            res = original.Replace(oldValue, newValue);

            var i = 0;
            do
            {
                i++;
                ante = res;
                res = ante.Replace(oldValue, newValue);

            } while (ante != res || i > MaxTries);

            return res;
        }

#if !SILVERLIGHT
        public static string ToValidIdentifier(this string original)
        {
            original = original.ToCapitalCase();

            if (original.Length == 0)
                return "_";

            StringBuilder res = new StringBuilder(original.Length + 1);
            if (!char.IsLetter(original[0]) && original[0] != '_')
                res.Append('_');

            for (int i = 0; i < original.Length; i++)
            {
                char c = original[i];
                if (char.IsLetterOrDigit(c) || c == '_')
                    res.Append(c);
                else
                    res.Append('_');
            }

            return res.ToString().ReplaceRecursive("__", "_").Trim('_');
        }


        public static string ToCapitalCase(this string original)
        {
            var words = original.Split(' ');
            var result = new List<string>();
            foreach (var word in words)
            {
                if (word.Length == 0 || AllCapitals(word))
                    result.Add(word);
                else if (word.Length == 1)
                    result.Add(word.ToUpper());
                else
                    result.Add(Char.ToUpper(word[0]) + word.Remove(0, 1).ToLower());
            }

            return string.Join(" ", result).Replace(" Y ", " y ")
                .Replace(" De ", " de ")
                .Replace(" O ", " o ");

        }

        static bool AllCapitals(string input)
        {
            return input.ToCharArray().All(Char.IsUpper);
        }


#endif
        public static string ToCamelCase(this string original)
        {
            if (original.Length <= 1)
                return original.ToLower();

            return char.ToLower(original[0]) + original.Substring(1);
        }

#if !SILVERLIGHT
        public static string UseAsSeparatorFor<T>(this string separator, IEnumerable<T> list)
        {
            return list.JoinToString(separator);
        }
#endif
        public static string AvoidNull(this string original)
        {
            if (original == null)
                return string.Empty;

            return original;
        }


        /// <summary>
        /// Repite un string la cantidad de veces indicada
        /// </summary>
        /// <param name="text"></param>
        /// <param name="times">Texto a busar</param>
        /// <returns>The <paramref name="text"/> repited the number of times indicates in the parameters</returns>
        public static string Repeat(this string text, int times)
        {
            if (text.IsNullOrEmpty() || times == 0)
                return string.Empty;

            if (text.Length == 1)
                return new string(text[0], times);

            if (times == 1)
                return text;
            else if (times == 2)
                return string.Concat(text, text);
            else if (times == 3)
                return string.Concat(text, text, text);
            else if (times == 4)
                return string.Concat(text, text, text, text);

            StringBuilder res = new StringBuilder(text.Length * times);
            for (int i = 0; i < times; i++)
            {
                res.Append(text);
            }
            return res.ToString();
        }

        /// <summary>
        /// Extrae el texto alrededor de un indice de un string
        /// </summary>
        /// <param name="text"></param>
        public static string ExtractAround(this string text, int index, int left, int right)
        {
            if (text.IsNullOrEmpty())
                return string.Empty;

            if (index >= text.Length)
                throw new IndexOutOfRangeException("The parameter index is outside the limits of the string.");

            var startIndex = Math.Max(0, index - left);
            var length = Math.Min(text.Length - startIndex, index - startIndex + right);

            return text.Substring(startIndex, length);
        }


        public static string TrimPhrase(this string text, string phrase)
        {
            var res = TrimPhraseStart(text, phrase);
            res = TrimPhraseEnd(res, phrase);
            return res;
        }

        public static string TrimPhraseStart(this string text, string phrase)
        {
            if (text.IsNullOrEmpty())
                return string.Empty;

            if (phrase.IsNullOrEmpty())
                return text;

            while (text.StartsWith(phrase))
            {
                text = text.Substring(phrase.Length);
            }

            return text;
        }

        public static string TrimPhraseEnd(this string text, string phrase)
        {
            if (text.IsNullOrEmpty())
                return string.Empty;

            if (phrase.IsNullOrEmpty())
                return text;

            while (text.EndsWithIgnoreCase(phrase))
            {
                text = text.Substring(0, text.Length - phrase.Length);
            }

            return text;
        }


        //public static string TrimPhraseEndIgnoreCase(this string text, string phrase)
        //{
        //    if (text.IsNullOrEmpty())
        //        return string.Empty;

        //    if (phrase.IsNullOrEmpty())
        //        return text;

        //    while (text.EndsWithIgnoreCase(phrase))
        //    {
        //        text = text.Substring(0, text.Length - phrase.Length);
        //    }

        //    return text;
        //}

        public static bool IsNumber(this string original)
        {
            long res;

            if (long.TryParse(original, out res))
                return true;
            else
                return false;
        }


        public static bool Like(this string me, string pattern)
        {
            //Turn a SQL-like-pattern into regex, by turning '%' into '.*'
            //Doesn't handle SQL's underscore into single character wild card '.{1,1}',
            // or the way SQL uses square brackets for escaping.
            //(Note the same concept could work for DOS-style wildcards (* and ?)
            var regex = new Regex("^" + pattern
                           .Replace(".", "\\.")
                           .Replace("*", ".*")
                           .Replace("%", ".*")
                           .Replace("\\.*", "\\%")
                           + "$", RegexOptions.IgnoreCase);

            return regex.IsMatch(me);
        }


        public static bool MatchRegEx(this string me, string pattern)
        {
            //Turn a SQL-like-pattern into regex, by turning '%' into '.*'
            //Doesn't handle SQL's underscore into single character wild card '.{1,1}',
            // or the way SQL uses square brackets for escaping.
            //(Note the same concept could work for DOS-style wildcards (* and ?)
            return Regex.IsMatch(me, pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }


        public static string RemoveDuplicateSpaces(this string me)
        {
            if (me.IsNullOrEmpty())
                return me;

            string ante = null;
            while (ante != me)
            {
                ante = me;
                me = me.Replace("  ", " ");
            }
            return me;
        }

        public static string RemoveDuplicateChar(this string me, char charRemove)
        {
            if (me.IsNullOrEmpty())
                return me;

            var strChar = charRemove.ToString();
            var charRep = strChar + strChar;

            string ante = null;
            while (ante != me)
            {
                ante = me;
                me = me.Replace(charRep, strChar);
            }
            return me;
        }

#if !SILVERLIGHT
        public static T[] SplitTyped<T>(this string me, char delimiter)
            where T : IComparable
        {
            if (me.IsNullOrWhite())
                return new T[] { };

            me = me.Trim();

            var parts = me.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

            var res = new T[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                res[i] = (T)Convert.ChangeType(parts[i], typeof(T));
            }
            return res;
        }

        public static T[] SplitTyped<T>(this string me, string delimiter)
         where T : IComparable
        {
            if (me.IsNullOrWhite())
                return new T[] { };

            me = me.Trim();

            var parts = me.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

            var res = new T[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                res[i] = (T)Convert.ChangeType(parts[i], typeof(T));
            }
            return res;
        }
#endif
        public static string LastWord(this string me)
        {
            if (me.IsNullOrEmpty())
                return string.Empty;

            for (int i = me.Length - 1; i >= 0; i--)
            {
                if (char.IsSeparator(me, i))
                {
                    if (i == me.Length - 1) // Si el separador es el ultimo entonces vacio
                        return string.Empty;
                    else
                        return me.Substring(i + 1);
                }

            }
            return me;
        }
#if !SILVERLIGHT
        public static string SecondWord(this string me)
        {
            if (me.IsNullOrEmpty())
                return string.Empty;

            var parts = me.SplitInWords();
            if (parts.Length >= 2)
                return parts[1];
            else
                return string.Empty;
        }
#endif

        public static string FirstWord(this string me)
        {
            if (me.IsNullOrEmpty())
                return string.Empty;

            for (int i = 0; i < me.Length; i++)
            {
                if (char.IsSeparator(me, i))
                {
                    if (i == 0) // Si el separador es el ultimo entonces vacio
                        return string.Empty;
                    else
                        return me.Substring(0, i);
                }

            }
            return me;
        }

        public static string RemoveChars(this string me, params char[] toRemove)
        {
            var res = new StringBuilder(me);
            foreach (var remove in toRemove)
            {
                res.Replace(remove, char.MinValue);
            }
            res.Replace(char.MinValue.ToString(), string.Empty);
            return res.ToString();
        }

        public static string ReplaceCharsWithSpace(this string me, params char[] toReplace)
        {
            var res = new StringBuilder(me);
            foreach (var replace in toReplace)
            {
                res.Replace(replace, ' ');
            }
            return res.ToString();
        }

        public static string ReplaceNumbersWith(this string me, char toReplace)
        {
            var res = new StringBuilder(me.Length);
            foreach (var caracter in me)
            {
                if (caracter.IsOn('1', '2', '3', '4', '5', '6', '7', '8', '9', '0'))
                    res.Append(toReplace);
                else
                    res.Append(caracter);
            }
            return res.ToString();
        }

        public static string SubstringFrom(this string me, string from)
        {
            if (me.IsNullOrEmpty())
                return string.Empty;

            var index = me.IndexOfIgnoreCase(from);
            if (index < 0)
                return string.Empty;

            return me.Substring(index + from.Length);
        }

        public static string SubstringTo(this string me, string from)
        {
            if (me.IsNullOrEmpty())
                return string.Empty;

            var index = me.IndexOfIgnoreCase(from);
            if (index < 0)
                return string.Empty;

            return me.Substring(0, index);
        }

        #region Contains digits and letters revisar redundancias


        public static string OnlyLettersNumbers(this string text)
        {
            var res = new StringBuilder(text.Length);

            foreach (char car in text)
            {
                if (char.IsLetterOrDigit(car))
                    res.Append(car);
            }

            return res.ToString();
        }
        public static string FilterChars(this string text, Predicate<char> onlyThese)
        {
            var res = new StringBuilder(text.Length);

            foreach (char car in text)
            {
                if (onlyThese(car))
                    res.Append(car);
            }

            return res.ToString();
        }


        public static bool IsUpper(this string text)
        {
            foreach (var ch in text)
            {
                if (!char.IsLetter(ch)
                    || char.IsLower(ch))
                    return false;
            }

            return true;
        }

        public static bool IsLower(this string text)
        {
            foreach (var ch in text)
            {
                if (char.IsLetter(ch)
                    && char.IsUpper(ch))
                    return false;
            }

            return true;
        }

        public static bool ContainsOnlyDigits(this string text) //ver
        {
            foreach (var car in text)
            {
                if (!char.IsDigit(car)) return false;
            }
            return true;
        }

        public static string OnlyDigits(this string text) //ver
        {
            return text.OnlyDigits(null);
        }

        public static bool NotContainsDigits(this string text) //Ver
        {
            foreach (var car in text)
            {
                if (char.IsDigit(car)) return false;
            }
            return true;
        }

        public static bool ContainsDigit(this string text) //ver
        {
            foreach (var car in text)
            {
                if (char.IsDigit(car)) return true;
            }
            return false;
        }

        public static string OnlyDigits(this string text, IEnumerable<char> excepciones) //ver
        {
            var res = new StringBuilder();
            foreach (char car in text)
            {
                if (char.IsDigit(car)
                    || excepciones.Contains(car))
                    res.Append(car);
            }

            return res.ToString();
        }

        public static bool IncludeDigits(this string text) //ver
        {
            return text.IncludeDigits(0);
        }

        public static bool IncludeDigits(this string text, int minCount) //ver
        {
            var count = 0;
            foreach (var car in text)
            {
                if (char.IsDigit(car))
                    count++;

                if (count >= minCount)
                    return true;
            }

            return false;
        }

        public static bool IncludeLetters(this string text) //ver
        {
            return text.IncludeLetters(0);
        }

        public static bool IncludeLetters(this string text, int minCount) //ver
        {
            var count = 0;
            foreach (var car in text)
            {
                if (char.IsLetter(car))
                    count++;

                if (count >= minCount)
                    return true;
            }

            return false;
        }

        public static int TotalDigits(this string text)
        {
            if (text.IsNullOrEmpty())
                return 0;

            return text.ToCharArray().FindAll(x => char.IsDigit(x)).Length;
        }

        public static int TotalLetters(this string text)
        {
            if (text.IsNullOrEmpty())
                return 0;

            return text.ToCharArray().FindAll(x => char.IsLetter(x)).Length;
        }

        public static int TotalLowerLetters(this string text)
        {
            if (text.IsNullOrEmpty())
                return 0;

            return text.ToCharArray().FindAll(x => char.IsLetter(x) && char.IsLower(x)).Length;
        }

        public static int TotalUpperLetters(this string text)
        {
            if (text.IsNullOrEmpty())
                return 0;

            return text.ToCharArray().FindAll(x => char.IsLetter(x) && char.IsUpper(x)).Length;
        }

        #endregion

        public static int CountOccurrences(this string text, char toCheck)
        {
            return text.CountOccurrences(toCheck.ToString());
        }

        public static int CountOccurrences(this string text, string toCheck)
        {
            if (toCheck.IsNullOrEmpty())
                return 0;

            int res = 0;
            int posIni = 0;
            while ((posIni = text.IndexOfIgnoreCase(posIni, toCheck)) != -1)
            {
                posIni += toCheck.Length;
                res++;
            }

            return res;
        }

        public static bool DiffOnlyOneChar(this string text, string toCheck)
        {
            if (text.Length != toCheck.Length)
                throw new ArgumentException("Los parametros para DiffOnlyOneChar deben tener la misma longitud");

            return text.DiffCharsCount(toCheck) == 1;
        }

        public static int DiffCharsCount(this string text, string toCheck)
        {
            if (text.Length != toCheck.Length)
                throw new ArgumentException("Los parametros para DiffOnlyOneChar deben tener la misma longitud");

            int res = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != toCheck[i])
                    res++;
            }
            return res;
        }

        public static int DiffCharsCountIgnoreCase(this string text, string toCheck)
        {
            if (text.Length != toCheck.Length)
                throw new ArgumentException("Los parametros para DiffOnlyOneChar deben tener la misma longitud");

            int res = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (!text[i].EqualsIgnoreCase(toCheck[i]))
                    res++;
            }
            return res;
        }

        public static bool OneAbsentChar(this string text, string toCheck)
        {
            if (text.Length > 1
                && toCheck.Length > 1
                && Math.Abs(text.Length - toCheck.Length) != 1) //las long deben diferir en 1, y ambas ser mayor que 1
                return false;

            var textWithChar = (text.Length > toCheck.Length ? text : toCheck);
            var textNoChar = (text.Length > toCheck.Length ? toCheck : text);

            //chequear si es el ultimo
            if (textWithChar[textWithChar.Length - 1] != textNoChar[textNoChar.Length - 1])
                return textWithChar.Substring(0, textWithChar.Length - 1).EqualsIgnoreCase(textNoChar);

            for (int i = 0; i < textNoChar.Length; i++)
            {
                if (textWithChar[i] != textNoChar[i])
                {
                    //a partir del car distinto, el resto debe coincidir
                    return textWithChar.Substring(i + 1).EqualsIgnoreCase(textNoChar.Substring(i));
                }
            }
            return false;
        }

        public static double EvaluarSimilitud(this string text, string toCheck, double similitudMinima)
        {
            const int difEncontradas = 0;
            return EvaluarSimilitud(text, toCheck, similitudMinima, difEncontradas);
        }

        private const int mMaxDifToleradas = 2;
        public static double EvaluarSimilitud(this string text, string toCheck, double similitudMinima, int difEncontradas)
        {
            //si tiene demasiadas diferencias ya no seguir comparando
            if (difEncontradas >= mMaxDifToleradas)
                return 0.0;

            //ignorar espacios
            text = text.RemoveChars(' ');
            toCheck = toCheck.RemoveChars(' ');

            //si son iguales 100%
            if (text.EqualsIgnoreCase(toCheck))
                return 1;

            //ignorar texto restante
            var porcionText = text;
            var porcionToCheck = toCheck;
            if (text.Length != toCheck.Length)
            {
                if (text.Length > toCheck.Length)
                    porcionText = text.Substring(0, toCheck.Length);
                else if (toCheck.Length > text.Length)
                    porcionToCheck = toCheck.Substring(0, text.Length);
                if (porcionText.EqualsIgnoreCase(porcionToCheck))
                    return 0.75;
            }

            //evaluar las diferencias
            var totalDiferencias = 0;
            var posDiferencia = -1;
            for (var i = 0; i < text.Length; i++)
            {
                if (i >= toCheck.Length
                    || text.ToCharArray()[i] != toCheck.ToCharArray()[i])
                    totalDiferencias++;
                if (posDiferencia < 0 && totalDiferencias == 1)
                    posDiferencia = i;
            }

            //sino retornar porcentaje segun la cantidad de diferencias
            var res = Convert.ToDouble(text.Length - totalDiferencias) / Convert.ToDouble(text.Length);

            if (totalDiferencias <= mMaxDifToleradas)
                return res;

            //suponer que se encontro solo 1 diferencia mas
            var diferencias = difEncontradas + 1;
            //considerar si la dif es un caracter ausente en texto2
            var resConCarAusenteEnTexto2 = text.Substring(posDiferencia + 1).EvaluarSimilitud(toCheck.Substring(posDiferencia), similitudMinima, diferencias);
            //considerar si la dif es un caracter ausente en texto1
            var resConCarAusenteEnTexto1 = text.Substring(posDiferencia).EvaluarSimilitud(toCheck.Substring(posDiferencia + 1), similitudMinima, diferencias);
            //considerar si la dif es un caracter cambiado
            var resConCarCambiado = text.Substring(posDiferencia + 1).EvaluarSimilitud(toCheck.Substring(posDiferencia + 1), similitudMinima, diferencias);
            //si alguna de las 3 es valida calcular similitud como
            //  simParte1 + max(simParte2) - 0.1 / 2
            if (resConCarAusenteEnTexto2 > similitudMinima
                || resConCarAusenteEnTexto1 > similitudMinima
                || resConCarCambiado > similitudMinima)
                return (1.0 + Math.Max(resConCarAusenteEnTexto2, Math.Max(resConCarAusenteEnTexto1, resConCarCambiado)) - 0.10) / 2.0;

            return res;
        }

        public enum TipoSimilitud
        {
            Ninguna,
            Igual,
            MayorLong,
            MenorLong,
        }

        public static TipoSimilitud EvaluarTipoSimilitud(this string text, string toCheck)
        {
            //ignorar espacios
            text = text.RemoveChars(' ');
            toCheck = toCheck.RemoveChars(' ');

            //si son iguales 100%
            if (text.EqualsIgnoreCase(toCheck))
                return TipoSimilitud.Igual;

            //ignorar texto restante
            var porcionText = text;
            var porcionToCheck = toCheck;
            if (text.Length != toCheck.Length)
            {
                if (text.Length > toCheck.Length)
                    porcionText = text.Substring(0, toCheck.Length);
                else if (toCheck.Length > text.Length)
                    porcionToCheck = toCheck.Substring(0, text.Length);
                if (porcionText.EqualsIgnoreCase(porcionToCheck))
                    return (text.Length > toCheck.Length ? TipoSimilitud.MayorLong : TipoSimilitud.MenorLong);
            }

            return TipoSimilitud.Ninguna;
        }


        public static string RemoveAcentosIgnoreCaseAndÑ(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;

            return text.RemoveAcentosIgnoreCase().Replace('Ñ', 'N').Replace('ñ', 'n');
        }

        public static string RemoveAcentosIgnoreCase(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;

            return text.Replace('Á', 'A')
                        .Replace('É', 'E')
                        .Replace('Í', 'I')
                        .Replace('Ó', 'O')
                        .Replace('Ú', 'U')
                        .Replace('ü', 'u')
                        .Replace('Ü', 'U')
                        .Replace('á', 'a')
                        .Replace('é', 'e')
                        .Replace('í', 'i')
                        .Replace('ó', 'o')
                        .Replace('ú', 'u');
        }

        public static string SafeGroupValue(this Match match, string name)
        {
            var group = match.Groups[name];

            if (group == null)
                return null;

            return match.Groups[name].Value;
        }
        


        #region "  Char Extensions  "

        public static bool EqualsIgnoreCase(this char text, char toCheck)
        {
            return char.ToUpper(text) == char.ToUpper(toCheck);
        }

        public static bool EqualsIgnoreCase(this char? text, char toCheck)
        {
            if (text == null)
                return false;

            return char.ToUpper(text.Value) == char.ToUpper(toCheck);
        }

        /// <summary>
        /// Return the AscII value of a char
        /// </summary>
        /// <param name="c"></param>
        /// <returns>AscII value of the char</returns>
        public static int ASCIIValue(this char c)
        {
            int num;
            int num2 = Convert.ToInt32(c);
            if (num2 < 0x80)
            {
                return num2;
            }

            byte[] buffer;
            Encoding fileIOEncoding = Encoding.UTF8;

            char[] chars = new char[] { c };
            if (fileIOEncoding.GetMaxByteCount(1) == 1)
            {
                buffer = new byte[1];
                int num3 = fileIOEncoding.GetBytes(chars, 0, 1, buffer, 0);
                return buffer[0];
            }
            buffer = new byte[2];
            if (fileIOEncoding.GetBytes(chars, 0, 1, buffer, 0) == 1)
            {
                return buffer[0];
            }
            if (BitConverter.IsLittleEndian)
            {
                byte num4 = buffer[0];
                buffer[0] = buffer[1];
                buffer[1] = num4;
            }
            num = BitConverter.ToInt16(buffer, 0);

            return num;
        }

        #endregion

    }


}
