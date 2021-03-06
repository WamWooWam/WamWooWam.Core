﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WamWooWam.Core
{
    public static class Strings
    {
        const string RANDOM_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
        internal static char[] _quotes = new[] { '"', '”', '“' };

        public static string RandomString(int length)
        {
            var random = new Random();
            return new string(Enumerable.Repeat(RANDOM_CHARS, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Normalises a string. (e.g. "This is 'a string'" == "this-is-a-string")
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Normalise(string str, char replaceChar = '-', bool changeCase = true)
        {
            var lower = (changeCase ? str.ToLowerInvariant() : str).Trim()
                .Where(x => char.IsLetterOrDigit(x) || char.IsWhiteSpace(x) || x == replaceChar)
                .Select(x => char.IsWhiteSpace(x) ? replaceChar : x)
                .ToArray();
            return new string(lower);
        }

        public static string NaturalJoin(IEnumerable<string> strings, string separator = ",", string and = "&")
        {
            string result;
            var count = strings.Count();
            if (count <= 1)
            {
                result = string.Join("", strings.ToArray());
            }
            else
            {
                result = $"{string.Join(separator + " ", strings.Take(count - 1))} {and} {strings.Last()}";
            }

            return result;
        }

        public static IEnumerable<string> SplitCommandLine(this string commandLine)
        {
            var inQuotes = false;

            return commandLine.Split((b, c, a) =>
            {
                if (_quotes.Contains(c) && b != '\\')
                {
                    inQuotes = !inQuotes;
                }

                return !inQuotes && c == ' ';
            }).Select(arg => arg.Trim().TrimMatchingQuotes());
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string Truncate(this string value, int maxLength, string elipsis)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Length <= maxLength ? value : value.Substring(0, maxLength - elipsis.Length) + elipsis;
        }

        public static string Replace(this string orig, string[] find, string[] replace)
        {
            for (var i = 0; i <= find.Length - 1; i++)
            {
                orig = orig.Replace(find[i], replace[i]);
            }
            return orig;
        }

        public static string Replace(this string orig, string[] find, string replace)
        {
            for (var i = 0; i <= find.Length - 1; i++)
            {
                orig = orig.Replace(find[i], replace);
            }
            return orig;
        }

        public static IEnumerable<string> Split(this string str, Func<char, bool> controller)
        {
            var nextPiece = 0;

            for (var c = 0; c < str.Length; c++)
            {
                if (controller(str[c]))
                {
                    yield return str.Substring(nextPiece, c - nextPiece);
                    nextPiece = c + 1;
                }
            }

            yield return str.Substring(nextPiece);
        }

        public static IEnumerable<string> Split(this string str, Func<char, char, char, bool> controller)
        {
            var nextPiece = 0;

            for (var c = 0; c < str.Length; c++)
            {
                if (controller(c > 0 ? str[c - 1] : default, str[c], c < str.Length - 1 ? str[c + 1] : default))
                {
                    yield return str.Substring(nextPiece, c - nextPiece);
                    nextPiece = c + 1;
                }
            }

            yield return str.Substring(nextPiece);
        }

        public static string TrimMatchingQuotes(this string input)
        {
            if ((input.Length >= 2) && (_quotes.Contains(input[0])) && (_quotes.Contains(input[input.Length - 1])))
            {
                return input.Substring(1, input.Length - 2);
            }

            return input;
        }

    }
}
