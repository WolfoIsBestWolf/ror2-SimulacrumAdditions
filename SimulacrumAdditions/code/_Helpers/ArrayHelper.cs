using System;
using System.Linq;
using UnityEngine;

namespace SimulacrumAdditions
{
    public static class ArrayHelper
    {
        public static T[] Add<T>(this T[] array, T item)
        {
            HG.ArrayUtils.ArrayAppend(ref array, item);
            return array;
        }
        public static T[] Add<T>(this T[] array, params T[] items)
        {
            int originalLength = array.Length;
            Array.Resize<T>(ref array, array.Length + items.Length);
            for (int i = 0; i < items.Length; i++)
            {
                array[originalLength+i] = items[i];
            }
            return array;
        }
        /*public static T[] Add<T>(this T[] array, params T[] items)
        {
            return (array ?? Enumerable.Empty<T>()).Concat(items).ToArray();
        }*/

        public static T[] Remove<T>(this T[] array, params T[] items)
        {
            return (array ?? Enumerable.Empty<T>()).Except(items).ToArray();
        }
    }
}
