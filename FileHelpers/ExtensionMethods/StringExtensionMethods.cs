using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace System
{
  

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class StringExtensionMethods
    {
        /// <summary>
        /// Indica si el string contine el texto toCheck lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="toCheck">Texto a busar</param>
        /// <returns>True if the string contains the text toCheck ignoring case.</returns>
        public static bool ContainsIgnoreCase(this string Text, string toCheck)
        {
            if (toCheck.IsNullOrEmpty())
                return true;

            return Text.IndexOfIgnoreCase(toCheck) >= 0;
        }

        public static bool ContainsOnlyThisChar(this string Text, char toCheck)
        {
            if (Text.Length == 0)
                return false;

            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] != toCheck)
                    return false;
            }
            
            return true;
        }


        
        /// <summary>
        /// Indica en que lugar el string contine el texto toCheck lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="toCheck">Texto a busar</param>
        /// <returns>El indice donde aparece por primera vez la cadena toCheck ignorando el case.</returns>
        public static int IndexOfIgnoreCase(this string Text, string toCheck)
        {
            return Text.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Indica en que lugar el string contine el texto toCheck lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="startIndex">La posicion inicial de la busqueda</param>
        /// <param name="toCheck">Texto a busar</param>
        /// <returns>El indice donde aparece por primera vez la cadena toCheck ignorando el case.</returns>
        public static int IndexOfIgnoreCase(this string Text, int startIndex, string toCheck)
        {
            return Text.IndexOf(toCheck, startIndex, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Indica si el string comienza con el texto toCheck lo busca con OrdinalIgnoringCase
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="toCheck">Texto a busar</param>
        public static bool StartsWithIgnoreCase(this string Text, string toCheck)
        {
            return Text.StartsWith(toCheck, StringComparison.OrdinalIgnoreCase);
        }

        public static bool EndsWithIgnoreCase(this string Text, string toCheck)
        {
            return Text.EndsWith(toCheck, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Convierte un texto a decimal de ser posible
        /// </summary>
        /// <param name="Text"></param>
        /// <returns>Texto convertido a decimal</returns>
        public static decimal ToDecimal(this string Text)
        {
            decimal d;
            decimal.TryParse(Text, out d);
            return d;
            //try
            //{
            //    return decimal.Parse(Text);
            //}
            //catch
            //{
            //    return 0;
            //}
        }

        /// <summary>
        ///  Convierte un texto a entero de ser posible
        /// </summary>
        /// <param name="Text"></param>
        /// <returns>Texto convertido a entero</returns>
        public static int ToInt(this string Text)
        {
            int entero;
            int.TryParse(Text, out  entero);
            return entero;

            //try
            //{
            //    return int.Parse(Text);
            //}
            //catch
            //{
            //    return 0;
            //}
        }

        /// <summary>
        ///  Convierte un texto a byte de ser posible
        /// </summary>
        /// <param name="Text"></param>
        /// <returns>Texto convertido a byte</returns>
        public static byte ToByte(this string Text)
        {
            byte numero;
            byte.TryParse(Text, out  numero);
            return numero;
        }

        /// <summary>
        /// Remueve la cadena especificada si se encuentra
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="removeText">Texto a ser removido</param>
        /// <returns>Texto sin la cadena a ser removida</returns>
        public static string Remove(this string Text, string removeText)
        {
            return Text.Replace(removeText, String.Empty);
        }

        /// <summary>
        /// Determina si una cadena es Nula o Vacia
        /// </summary>
        /// <param name="Texto"></param>
        /// <returns>Verdadero o Falso</returns>
        public static bool IsNullOrEmpty(this string Texto)
        {
            return String.IsNullOrEmpty(Texto);
        }

        /// <summary>
        /// Determina si una cadena NO es Nula o Vacia
        /// </summary>
        /// <param name="Texto"></param>
        /// <returns>Verdadero o Falso</returns>
        public static bool NotIsNullOrEmpty(this string Texto)
        {
            return ! String.IsNullOrEmpty(Texto);
        }

        /// <summary>
        /// Determina si una cadena es Vacia
        /// </summary>
        /// <param name="Texto"></param>
        /// <returns>Verdadero o Falso</returns>
        public static bool IsEmpty(this string Texto)
        {
            return Texto.Equals(String.Empty);
        }

        public static string Substring(this string Text, string startText)
        {
            int indice = Text.IndexOf(startText);
            if (indice == -1) throw new ArgumentException("No se encontro: " + startText);
            return Text.Substring(indice);
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






        public static string Left(this string Text, int length)
        {
            if (length < 0)
            {
                throw new ArgumentException("Length > 0", "length");
            }
            if ((length == 0) || (Text == null))
            {
                return "";
            }
            if (length >= Text.Length)
            {
                return Text;
            }
            return Text.Substring(0, length);
        }






        /// <summary>
        ///  Inidica si alguno de los valores esta en la cadena
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="values">Valores a verificar</param>
        /// <returns>Verdadero o Falso</returns>
        public static bool Contains(this string Text, string[] values)
        {
            bool contain = false;
            foreach (String s in values)
            {
                if (Text.Contains(s)) contain = true;
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

        public static string Replace(this string original, string oldValue, string newValue, StringComparison comparisionType)
        {
            string result = original;

            if (!string.IsNullOrEmpty(oldValue))
            {
                int index = -1;
                int lastIndex = 0;

                StringBuilder buffer = new StringBuilder(original.Length);

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
            if (original.Length <= maxLength)
                return original;
            else
                return original.Substring(0, maxLength - 3) + "...";
        }

        public static string ReplaceRecursive(this string original, string oldValue, string newValue)
        {
            const int maxTries = 1000;

            string ante, res;

            res = original.Replace(oldValue, newValue);

            var i = 0;
            do
            {
                i++;
                ante = res;
                res = ante.Replace(oldValue, newValue);

            } while (ante != res || i > maxTries);

            return res;
        }

        public static string ToValidIdentifier(this string original)
        {
            if (original.Length == 0)
                return string.Empty;

           StringBuilder res = new StringBuilder(original.Length + 1);
            if (! char.IsLetter(original[0]) && original[0] != '_' )
                res.Append('_');

            for (int i = 0; i < original.Length; i++)
            {
                char c = original[i];
                if (char.IsLetterOrDigit(c) || c == '_')
                    res.Append(c);
                else
                    res.Append('_');
            }

            var identifier = res.ToString().ReplaceRecursive("__", "_").Trim('_');
            if (identifier.Length == 0)
                return "_";
            else if (char.IsDigit(identifier[0]))
                identifier = "_" + identifier;

            return identifier;

        }

        public static string ToCapitalCase(this string original)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(original.ToLower());
        }

     
        public static string AvoidNull(this string original)
        {
            if (original == null)
                return string.Empty;

            return original;
        }


 

    }
}
