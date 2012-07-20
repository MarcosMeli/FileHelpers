// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace ColorCode.Common
{
    internal static class ExtensionMethods
    {
        public static void SortStable<T>(this IList<T> list,
                                         Comparison<T> comparison)
        {
            Guard.ArgNotNull(list, "list");

            int count = list.Count;

            for (int j = 1; j < count; j++)
            {
                T key = list[j];

                int i = j - 1;
                for (; i >= 0 && comparison(list[i], key) > 0; i--)
                {
                    list[i + 1] = list[i];
                }

                list[i + 1] = key;
            }
        }

        public static string ToHtmlColor(this Color color)
        {
            if (color == Color.Empty)
                throw new ArgumentException("You may not create a hex string from an empty color.");

            return ColorTranslator.ToHtml(color);
        }
    }
}