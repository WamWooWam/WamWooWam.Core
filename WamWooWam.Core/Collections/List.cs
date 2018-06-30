﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WamWooWam.Core.Collections
{
    public static class ListExtensions
    {
        public static T RandomItem<T>(this IEnumerable<T> sourceList)
        {
            Random random = new Random();
            return sourceList.ElementAt(random.Next(sourceList.Count()));
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> sourceList)
        {
            Random random = new Random();
            List<T> source = sourceList.ToList(); // Not the fastest but it'll work
            int initialCount = source.Count;

            for(int i = 0; i < initialCount; i++)
            {
                T result = source.ElementAt(random.Next(source.Count));
                source.Remove(result);
                yield return result;
            }
        }
    }
}