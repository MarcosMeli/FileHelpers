using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Linq;

namespace System
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class DevooCollectionsExtensionMethods
    {

        public static bool IsNullOrZeroItems(this IList list)
        {
            return list == null || list.Count == 0;
        }

        //public static T First<T>(this IList<T> list)
        //    where T:class 
        //{
        //    if (list.Count == 0)
        //        return null;
        //    return list[0];
        //}


        public static T FirstBasedOn<T>(this IList<T> list, Func<T, IComparable> order)
            where T : class
        {
            if (list.Count == 0)
                return null;

            var itemFirst = default(T);
            IComparable valueFirst = null;

            foreach (var item in list)
            {
                var actual = order(item);

                if (valueFirst == null || actual.CompareTo(valueFirst) < 0)
                {
                    valueFirst = actual;
                    itemFirst = item;
                }
            }

            return itemFirst;
        }

        //public static T Last<T>(this IList<T> list)
        //    where T : class
        //{
        //    if (list.Count == 0)
        //        return null;
        //    return list[list.Count - 1];
        //}


        public static T LastBasedOn<T>(this IList<T> list, Func<T, IComparable> order)
            where T : class
        {
            if (list.Count == 0)
                return null;

            var itemLast = default(T);
            IComparable valueLast = null;
                
            foreach (var item in list)
            {
                var actual = order(item);

                if (valueLast == null || actual.CompareTo(valueLast) >= 0)
                {
                    valueLast = actual;
                    itemLast = item;
                }
            }

            return itemLast;
        }
        
        public static int CountDistinct<TObj, TResult>(this IList<TObj> list, Func<TObj, TResult> valCalculator)
        {
            var check = new HashSet<TResult>();

            foreach (var item in list)
            {
                var result = valCalculator(item);
                if (!check.Contains(result))
                    check.Add(result);
            }

            return check.Count;
        }
        

        public static List<TResult> SelectDistinctUnSorted<TObj, TResult>(this IList<TObj> list, Func<TObj, TResult> valCalculator)
        {
            var res = new List<TResult>();
            var containsCheck = new HashSet<TResult>();

            foreach (var item in list)
            {
                var result = valCalculator(item);
                if (!containsCheck.Contains(result))
                {
                    containsCheck.Add(result);
                    res.Add(result);
                }
            }

            return res;
        }

      

        public static Dictionary<TKey, TItem> ToDictionary<TItem, TKey>(this IList<TItem> list, Func<TItem, TKey> keyFunc)
        {
            return ToDictionary(list, keyFunc, x => x);
        }

        public static Dictionary<TKey, TValue> ToDictionary<TItem, TKey, TValue>(this IList<TItem> list, Func<TItem, TKey> keyFunc, Func<TItem, TValue> valueFunc)
        {
            var res = new Dictionary<TKey, TValue>(list.Count);

            foreach (var item in list)
            {
                res.Add(keyFunc(item), valueFunc(item));
            }

            return res;
        }

        public static HashSet<TItem> ToHashSetIgnoringDuplicates<TItem>(this IList<TItem> list) where TItem : IComparable<TItem>
        {
            var res = new HashSet<TItem>();

            foreach (var item in list)
            {
                if (!res.Contains(item))
                    res.Add(item);
            }

            return res;
        }


        public static HashSet<TItem> ToHashSet<TItem>(this IEnumerable<TItem> list) where TItem: IComparable<TItem>
        {
            var res = new HashSet<TItem>();

            foreach (var item in list)
            {
                res.Add(item);
            }

            return res;
        }

        public static HashSet<TItem> ToHashSet<TItem>(this IEnumerable<TItem> list, bool ignoreDup) where TItem : IComparable<TItem>
        {
            var res = new HashSet<TItem>();

            foreach (var item in list)
            {
                if (ignoreDup && res.Contains(item))
                    continue;

                res.Add(item);
            }

            return res;
        }

        public static HashSet<TKey> ToHashSet<TItem, TKey>(this IEnumerable<TItem> list, Func<TItem, TKey> keyFunc) where TKey : IComparable<TKey>
        {
            var res = new HashSet<TKey>();

            foreach (var item in list)
            {
                res.Add(keyFunc(item));
            }

            return res;
        }


        /// <summary>
        /// Agrupa los elementos en Listas de con todos los que cumplen determinado criterio
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="keyFunc"></param>
        /// <returns></returns>
        public static Dictionary<TKey, List<TItem>> GroupByAsDictionary<TItem, TKey>(this IEnumerable<TItem> list, Func<TItem, TKey> keyFunc)
        {
            return GroupByAsDictionary(list, keyFunc, x => x);
        }

        /// <summary>
        /// Agrupa los elementos en Listas de con todos los que cumplen determinado criterio
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="keyFunc"></param>
        /// <param name="valueFunc"></param>
        /// <returns></returns>
        public static Dictionary<TKey, List<TValue>> GroupByAsDictionary<TItem, TKey, TValue>(this IEnumerable<TItem> list, Func<TItem, TKey> keyFunc, Func<TItem, TValue> valueFunc)
        {
            var res = new Dictionary<TKey, List<TValue>>();

            foreach (var item in list)
            {
                var key = keyFunc(item);
                var value = valueFunc(item);

                List<TValue> valuesList;
                if (!res.TryGetValue(key, out valuesList))
                {
                    valuesList = new List<TValue>();
                    res.Add(key, valuesList);
                }

                valuesList.Add(value);
            }

            return res;
        }


        public static Dictionary<TKey1, Dictionary<TKey2, List<TItem>>> GroupByAsDictionaryOfDictionaries<TItem, TKey1, TKey2>(this IEnumerable<TItem> list, Func<TItem, TKey1> keyFunc1, Func<TItem, TKey2> keyFunc2)
        {
            var res = new Dictionary<TKey1, Dictionary<TKey2, List<TItem>>>();

            foreach (var item in list)
            {
                var key1 = keyFunc1(item);
                var key2 = keyFunc2(item);

                Dictionary<TKey2, List<TItem>> dictionary1;
                if (!res.TryGetValue(key1, out dictionary1))
                {
                    dictionary1 = new Dictionary<TKey2, List<TItem>>();
                    res.Add(key1, dictionary1);
                }

                List<TItem> valuesList;
                if (!dictionary1.TryGetValue(key2, out valuesList))
                {
                    valuesList = new List<TItem>();
                    dictionary1.Add(key2, valuesList);
                }

                valuesList.Add(item);
            }

            return res;
        }
        public static Dictionary<TKey, HashSet<TValue>> GroupByAsDictionaryHash<TItem, TKey, TValue>(this IEnumerable<TItem> list, Func<TItem, TKey> keyFunc, Func<TItem, TValue> valueFunc)
        {
            var res = new Dictionary<TKey, HashSet<TValue>>();

            foreach (var item in list)
            {
                var key = keyFunc(item);
                var value = valueFunc(item);

                HashSet<TValue> valuesList;
                if (!res.TryGetValue(key, out valuesList))
                {
                    valuesList = new HashSet<TValue>();
                    res.Add(key, valuesList);
                }

                valuesList.Add(value);
            }

            return res;
        }

        public static Dictionary<TKey, TValue> ToDictionaryIgnoringDuplicateKeys<TItem, TKey, TValue>(this IList<TItem> list, Func<TItem, TKey> keyFunc, Func<TItem, TValue> valueFunc)
        {
            var res = new Dictionary<TKey, TValue>(list.Count);

            foreach (var item in list)
            {
                var key = keyFunc(item);
                if (!res.ContainsKey(key))
                    res.Add(key, valueFunc(item));
            }

            return res;
        }


        

        //public static Hashtable ToDictionary<TItem>(this IList<TItem> list, Func<TItem, object> keyFunc, Func<TItem, object> valueFunc)
        //{
        //    var res = new Hashtable(list.Count);

        //    foreach (var item in list)
        //    {
        //        res.Add(keyFunc(item), valueFunc(item));
        //    }

        //    return res;
        //}

        public static List<KeyValuePair<TKey, int>> ToSortedArrayByValue<TKey>(this Dictionary<TKey, int> list)
        {
            List<KeyValuePair<TKey, int>> res = new List<KeyValuePair<TKey, int>>();

            foreach (var valor in list)
            {
                res.Add(valor);
            }

            res.Sort((x, y) => -x.Value.CompareTo(y.Value));


            return res;
        }

        public static List<KeyValuePair<TKey, TValue>> ToSortedArrayByKey<TKey, TValue>(this Dictionary<TKey, TValue> list)
            where TKey : IComparable<TKey>
        {
            var res = new List<KeyValuePair<TKey, TValue>>();

            foreach (var valor in list)
            {
                res.Add(valor);
            }

            res.Sort((x, y) => x.Key.CompareTo(y.Key));


            return res;
        }

        //     DICTIONARY   

        public static void AddOrOverride<TKey, TValue>(this Dictionary<TKey, TValue> list, TKey key, TValue value)
        {
            list[key] = value;
        }

        public static TValue GetOrAddValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> newValueCreator)
        {
            TValue res;
            if (dictionary.TryGetValue(key, out res))
                return res;

            res = newValueCreator(key);
            dictionary.Add(key, res);
            return res;

        }

        public static TValue GetOrCreateNew<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
            where TValue: new()
        {
            TValue res;
            if (dictionary.TryGetValue(key, out res))
                return res;

            res = new TValue();
            dictionary.Add(key, res);
            return res;

        }

        /// <summary>
        /// Gets the or calculate.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="valueCalculator">The value calculator.</param>
        /// <returns>if dictionary is null or don't contains the key so returns the valueCalculator result</returns>
        public static TValue GetOrCalculate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueCalculator)
        {
            TValue res;
            if (dictionary != null && dictionary.TryGetValue(key, out res))
                return res;

            return valueCalculator(key);
        }

        public static TValue GetOrNull<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
            where TValue: class
        {
            TValue res;
            if (dictionary != null && dictionary.TryGetValue(key, out res))
                return res;

            return null;
        }


        /// <summary>
        /// Inserta o Modifica el valor de una clave
        /// </summary>
        /// <typeparam name="TKey">Tipo del Key del diccionario</typeparam>
        /// <typeparam name="TValue">Tipo del Valor del diccionario</typeparam>
        /// <param name="dictionary">Diccionario</param>
        /// <param name="key">valor del Key</param>
        /// <param name="value">valor a ser Insertado o  modificado</param>
        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {

            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }

        public static string JoinToStringFormat<T>(this IEnumerable<T> list)
            where T : IFormattable
        {
            return list.JoinToStringFormat(",");
        }

        public static string JoinToStringFormat<T>(this IEnumerable<T> list, string delimiter)
            where T : IFormattable
        {
            return list.JoinToStringFormat(delimiter, null);
        }

        public static string JoinToStringFormat<T>(this IEnumerable<T> list, string delimiter, IFormatProvider info)
                where T: IFormattable
            {
    
                var sb = new StringBuilder();

                bool primero = true;

                foreach (T x in list)
                {
                    if (primero)
                    {
                        primero = false;
                    }
                    else
                    {
                        sb.Append(delimiter);
                    }
                    sb.Append(x.ToString(null, info));
                }

                return sb.ToString();
            }

        /// <summary>
        /// String con los valores de la coleccion separados por ","
        /// </summary>
        /// <typeparam name="T">Tipo de los elementos de la Coleccion</typeparam>
        /// <param name="list">Coleccion</param>
        /// <returns>string con los valores de la coleccion separados por ","</returns>
        public static string JoinToString<T>(this IEnumerable<T> list)
        {
            return list.JoinToString(",");
        }
        
        /// <summary>
        /// String con los valores de cada item de la colección uno por linea
        /// </summary>
        /// <typeparam name="T">Tipo de los elementos de la Coleccion</typeparam>
        /// <param name="list">Coleccion</param>
        /// <returns></returns>
        public static string JoinOnePerLine<T>(this IEnumerable<T> list)
        {
            return list.JoinToString(Environment.NewLine) + Environment.NewLine;
        }

        /// <summary>
        /// String con los valores de la coleccion separados por el delimitador indicado
        /// </summary>
        /// <param name="list">Coleccion</param>
        /// <param name="delimiter">delimitador de los elementos de la colecion</param>
        /// <returns>String con los valores de la coleccion separados por el delimitador indicado</returns>
        public static string JoinToString(this IEnumerable<string> list, string delimiter)
        {
            if (list == null)
                return string.Empty;

            StringBuilder sb;
            var listTyped = list as IList<string>;
            if (listTyped == null)
                sb = new StringBuilder();
            else
            {
                var largo = listTyped.Sum(x => x.Length);
                sb = new StringBuilder(largo + delimiter.Length * listTyped.Count);
            }

            bool primero = true;

            foreach (var x in list)
            {
                if (primero)
                {
                    primero = false;
                }
                else
                {
                    sb.Append(delimiter);
                }
                sb.Append(x);
            }

            return sb.ToString();
        }

        /// <summary>
        /// String con los valores de la coleccion separados por el delimitador indicado
        /// </summary>
        /// <typeparam name="T">Tipo de los elementos de la Coleccion</typeparam>
        /// <param name="list">Coleccion</param>
        /// <param name="delimiter">delimitador de los elementos de la colecion</param>
        /// <returns>String con los valores de la coleccion separados por el delimitador indicado</returns>
        public static string JoinToString<T>(this IEnumerable<T> list, string delimiter)
        {
            if (list == null)
                return string.Empty;

            StringBuilder sb;

            if (list is IList<T>)
                sb = new StringBuilder(((IList<T>)list).Count * 5);
            else
                sb = new StringBuilder();

            bool primero = true;

            foreach (T x in list)
            {
                if (primero)
                {
                    primero = false;
                }
                else
                {
                    sb.Append(delimiter);
                }
                sb.Append(x.ToString());
            }

            return sb.ToString();
        }

        public static bool IsOn(this byte source, params byte[] list)
        {
            return IsOn<byte>(source, list);
        }

        public static bool IsOn(this short source, params short[] list)
        {
            return IsOn<short>(source, list);
        }

        /// <summary>
        /// Search the <paramref name="source"/> in the <paramref name="list"/> and returns trus if found it
        /// </summary>
        /// <typeparam name="T">The Generic Class</typeparam>
        /// <param name="source">Value to search for</param>
        /// <param name="list">List to search in</param>
        /// <returns></returns>
        public static bool IsOn<T>(this T source, params T[] list) where T: IComparable
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].CompareTo(source) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsOn<T>(this T source, IEnumerable<T> list) where T : IComparable
        {
            foreach (var item in list)
            {
                if (item.CompareTo(source) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsOn<T>(this T source, HashSet<T> list) where T : IComparable
        {
            return list.Contains(source);
        }
     
        public static bool IsOnIgnoreCase(this string source, params string[] list) 
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (source.EqualsIgnoreCase(list[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static void ShuffleInPlace<T>(this IList<T> items)
        {
            ShuffleInPlace(items, 4);
        }

        public static void ShuffleInPlace<T>(this IList<T> items, int times)
        {
            for (int j = 0; j < times; j++)
            {
                var rnd = new Random((int)(DateTime.Now.Ticks % int.MaxValue) - j);

                for (int i = 0; i < items.Count; i++)
                {
                    var index = rnd.Next(items.Count - 1);
                    var temp = items[index];
                    items[index] = items[i];
                    items[i] = temp;
                }
            }
        }

        public static List<T> ShuffleToNewList<T>(this IList<T> items)
        {
            return ShuffleToNewList(items, 4);
        }

        public static List<T> ShuffleToNewList<T>(this IList<T> items, int times)
        {
            var res = new List<T>(items);
            res.ShuffleInPlace(times);
            return res;
        }


        //public static void SetDataSource<T>(this BindingSource bs, IList<T>, Control ctrlContext)
        //{
        //    bs.DataSource = 
        //    return new SortableBindingList<T>(list, ctrlContext);
        //}


        public static TSource[] ToSortedArray<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, Comparison<TSource> comparer)
        {
            var res = source.ToArray();
            Array.Sort(res, comparer);
            return res;
        }

        public static TSource[] ToSortedArray<TSource>(this System.Collections.Generic.IEnumerable<TSource> source)
            where TSource: IComparable<TSource>
        {
            var res = source.ToArray();
            Array.Sort(res);
            return res;
        }

        public static List<TSource> RemoveDuplicates<TSource>(this IList<TSource> values)
        {
            var res = new List<TSource>(values.Count);
            var duplicateCheck = new HashSet<TSource>();
            
            foreach (var value in values)
            {
                if (duplicateCheck.Contains(value))
                    continue;

                duplicateCheck.Add(value);
                res.Add(value);
            }

            return res;
        }

        public static List<TSource> RemoveDuplicates<TSource, TCheck>(this IList<TSource> values, Func<TSource, TCheck> dupliCheck)
        {
            var duplicateCheck = new HashSet<TCheck>();
            var res = new List<TSource>(values.Count);

            foreach (var value in values)
            {
                var val = dupliCheck(value);

                if (duplicateCheck.Contains(val))
                    continue;

                duplicateCheck.Add(val);
                res.Add(value);
            }

            return res;
        }


        public static List<string> RemoveDuplicatesIgnoreCase(this IList<string> values)
        {
            var duplicateCheck = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var res = new List<string>(values.Count);
            foreach (var value in values)
            {
                if (duplicateCheck.Contains(value))
                {
                    continue;
                }

                duplicateCheck.Add(value);
                res.Add(value);
            }

            return res;
        }

        public static List<TSource> MoveToFirst<TSource>(this List<TSource> source, TSource element)
        {
            if (!source.Contains(element))
                return source;

            source.Remove(element);
            source.Insert(0, element);
            return source;
        }

        //public static List<List<TSource>> SplitInGroups<TSource>(this IEnumerable<TSource> values, int groupSize)
        //{
        //    var res = new List<List<TSource>>();
        //    var currentList = new List<TSource>(groupSize);

        //    foreach (var value in values)
        //    {
        //        if (currentList.Count >= groupSize)
        //        {
        //            res.Add(currentList);
        //            currentList = new List<TSource>(groupSize);
        //        }
        //        currentList.Add(value);
        //    }

        //    if (currentList.Count > 0)
        //        res.Add(currentList);
            
        //    return res;
        //}

        public static IEnumerable<List<TSource>> SplitInGroups<TSource>(this IEnumerable<TSource> values, int groupSize)
        {
            var asList = values as List<TSource>;
            if (asList != null && asList.Count <= groupSize)
            {
                yield return asList;
                yield break;
            }

            var currentList = new List<TSource>(groupSize);

            foreach (var value in values)
            {
                if (currentList.Count >= groupSize)
                {
                    yield return currentList;
                    currentList = new List<TSource>(groupSize);
                }
                currentList.Add(value);
            }

            if (currentList.Count > 0)
                yield return currentList;
        }

        public static List<List<TSource>> SplitInGroupsRemovingDuplicates<TSource>(this IEnumerable<TSource> values, int groupSize)
        {
            var res = new List<List<TSource>>();

            var duplicateCheck = new HashSet<TSource>();
            var currentList = new List<TSource>(groupSize);

            foreach (var value in values)
            {
                if (duplicateCheck.Contains(value))
                    continue;

                duplicateCheck.Add(value);

                if (currentList.Count >= groupSize)
                {
                    res.Add(currentList);
                    currentList = new List<TSource>(groupSize);
                }
                currentList.Add(value);
            }

            if (currentList.Count > 0)
                res.Add(currentList);

            return res;
        }

        public static void AddDictionary<TKey, TVal>(this Dictionary<TKey, TVal> me, Dictionary<TKey, TVal> other)
        {
            foreach (var src in other)
            {
                me.Add(src.Key, src.Value);
            }
        }

        public static T[] FindAll<T>(this T[] me, Predicate<T> condition)
        {
            return Array.FindAll(me, condition);
        }

        public static bool Contains<T>(this IEnumerable<T> me, Predicate<T> condition)
        {
            foreach (var val in me)
            {
                if (condition(val)) 
                    return true;
            }
            return false;
        }

        public static List<string> ToStringList<T>(this IEnumerable<T> me, Func<T, string> stringConverter)
        {
            var res = new List<string>();

            foreach (var val in me)
            {
                res.Add(stringConverter(val));
            }

            return res;
        }


        public static List<Tuple<TKey, TValue>> ToTuple<TKey, TValue>(this Dictionary<TKey, TValue> me)
        {
            var res = new List<Tuple<TKey, TValue>>();

            foreach (var val in me)
            {
                res.Add(Tuple.Create(val.Key, val.Value));
            }

            return res;
        }

    }
}
