using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PenguinPlunge.Utility
{
    public static class GeneralExtensions
    {
        public static float RandomInRange(this Vector2 vector) => Random.Range(vector.x, vector.y);

        public static int RandomInRange(this Vector2Int vector) => Random.Range(vector.x, vector.y);

        public static int GetRandomDifferentIndex<T>(this IEnumerable<T> collection, int currentIndex)
        {
            int count = collection.Count();
            int index = Random.Range(0, count);
            if (count > 1 && currentIndex == index)
            {
                return collection.GetRandomDifferentIndex(currentIndex);
            }
            return index;
        }

        public static T GetRandomEnumValueExcluding<T>(this T exclude) where T : Enum
        {
            List<T> values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            values.Remove(exclude);
            return values[Random.Range(0, values.Count)];
        }

        public static T GetRandomEnumValue<T>(this T e) where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            return values[Random.Range(0, values.Count)];
        }

        public static T GetRandomElement<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            int count = collection.Count();

            if (count == 0)
            {
                throw new InvalidOperationException("Collection is empty.");
            }

            int index = Random.Range(0, count);
            return collection.ElementAt(index);
        }

        public static T GetRandomElementExcluding<T>(this IEnumerable<T> collection, T excluding)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            int count = collection.Count();

            if (count < 2) //At least two values are required, as exclusion is applied
            {
                throw new InvalidOperationException("Collection is empty.");
            }
            List<T> values = collection.ToList();
            values.Remove(excluding);
            int index = Random.Range(0, values.Count);
            return values.ElementAt(index);
        }
    }
}
