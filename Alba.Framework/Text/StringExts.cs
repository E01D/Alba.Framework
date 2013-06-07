﻿using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Alba.Framework.Attributes;

namespace Alba.Framework.Text
{
    public static class StringExts
    {
        private static readonly Regex ReNewlines = new Regex("@\r?\n", RegexOptions.Compiled);

        [Pure]
        public static string AppendSentence (this string @this, string sentence)
        {
            var sb = new StringBuilder(@this, @this.Length + sentence.Length + 2);
            sb.AppendSentence(sentence);
            return sb.ToString();
        }

        [Pure]
        public static bool IsNullOrEmpty (this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }

        [Pure]
        public static bool IsNullOrWhitespace (this string @this)
        {
            return string.IsNullOrWhiteSpace(@this);
        }

        [Pure]
        public static string RemovePostfix (this string @this, string postfix)
        {
            if (!@this.EndsWith(postfix))
                throw new ArgumentException("String '{0}' does not contain postfix '{1}'.".Fmt(@this, postfix), "postfix");
            return @this.Remove(@this.Length - postfix.Length);
        }

        [Pure]
        public static string RemovePostfixSafe (this string @this, string postfix)
        {
            return @this.EndsWith(postfix) ? @this.Remove(@this.Length - postfix.Length) : @this;
        }

        [Pure]
        public static string RemovePrefix (this string @this, string prefix)
        {
            if (!@this.StartsWith(prefix))
                throw new ArgumentException("String '{0}' does not contain prefix '{1}'.".Fmt(@this, prefix), "prefix");
            return @this.Substring(prefix.Length);
        }

        [Pure]
        public static string RemovePrefixSafe (this string @this, string prefix)
        {
            return @this.StartsWith(prefix) ? @this.Substring(prefix.Length) : @this;
        }

        [Pure]
        public static string SingleLine (this string @this)
        {
            return ReNewlines.Replace(@this, " ");
        }

        [Pure]
        public static string SubstringEnd (this string @this, int length)
        {
            return @this.Length < length ? @this : @this.Substring(@this.Length - length, length);
        }

        public static void AppendSentence (this StringBuilder @this, string sentence)
        {
            @this.Append(@this[@this.Length - 1] == '.' ? " " : ". ");
            @this.Append(sentence);
        }

        [Pure, StringFormatMethod ("format")]
        public static string Fmt (this string format, IFormatProvider provider, params object[] args)
        {
            return string.Format(provider, format, args);
        }

        [Pure, StringFormatMethod ("format")]
        public static string Fmt (this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        [Pure, StringFormatMethod ("format")]
        public static string FmtInv (this string format, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }

        [Pure]
        public static bool Contains (this string @this, char value)
        {
            return @this.IndexOf(value) >= 0;
        }

        [Pure]
        public static bool Contains (this string @this, char value, StringComparison comparisonType)
        {
            return @this.IndexOf(new string(value, 1), comparisonType) >= 0;
        }

        [Pure]
        public static bool Contains (this string @this, string value, StringComparison comparisonType)
        {
            return @this.IndexOf(value, comparisonType) >= 0;
        }

        [Pure]
        public static string ReEscape (this string @this)
        {
            return Regex.Escape(@this);
        }

        [Pure]
        public static string ReEscapeReplacement (this string @this)
        {
            return @this.Replace("$", "$$");
        }

        [Pure]
        public static bool IsReMatch (this string @this, string pattern, RegexOptions options = RegexOptions.None)
        {
            return Regex.IsMatch(@this, pattern, options);
        }

        [Pure]
        public static Match ReMatch (this string @this, string pattern, RegexOptions options = RegexOptions.None)
        {
            return Regex.Match(@this, pattern, options);
        }

        [Pure]
        public static MatchCollection ReMatches (this string @this, string pattern, RegexOptions options = RegexOptions.None)
        {
            return Regex.Matches(@this, pattern, options);
        }

        [Pure]
        public static string ReReplace (this string @this, string pattern, string replacement, RegexOptions options = RegexOptions.None)
        {
            return Regex.Replace(@this, pattern, replacement, options);
        }

        [Pure]
        public static string ReReplace (this string @this, string pattern, MatchEvaluator evaluator, RegexOptions options = RegexOptions.None)
        {
            return Regex.Replace(@this, pattern, evaluator, options);
        }

        [Pure]
        public static string[] ReSplit (this string @this, string pattern, string replacement, RegexOptions options = RegexOptions.None)
        {
            return Regex.Split(@this, pattern, options);
        }

        [Pure]
        public static string ReUnescape (this string @this)
        {
            return Regex.Unescape(@this);
        }

        [Pure]
        public static string Sub (this string @this, int startIndex, int length)
        {
            return startIndex >= 0
                ? (length >= 0
                    ? @this.Substring(startIndex, length)
                    : @this.Substring(startIndex, @this.Length - startIndex + length))
                : (length >= 0
                    ? @this.Substring(@this.Length + startIndex, length)
                    : @this.Substring(@this.Length + startIndex, @this.Length + startIndex + length));
        }

        [Pure]
        public static string Sub (this string @this, int startIndex)
        {
            return startIndex >= 0
                ? @this.Substring(startIndex)
                : @this.Substring(@this.Length + startIndex);
        }

        [Pure]
        public static string Indent (this string @this, string indentStr)
        {
            return @this.ReReplace("(?m)^(.*)$", indentStr.ReEscapeReplacement() + "$1");
        }

        [Pure]
        public static string Unindent (this string @this)
        {
            return UnindentInternal(@this, @"\s*");
        }

        [Pure]
        public static string Unindent (this string @this, int indentLength)
        {
            return UnindentInternal(@this, @"\s{0}".FmtInv(indentLength));
        }

        [Pure]
        public static string Unindent (this string @this, string indentStr)
        {
            return UnindentInternal(@this, indentStr.ReEscape());
        }

        private static string UnindentInternal (this string @this, string indentPattern)
        {
            return @this.ReReplace("(?m)^{0}(.*)$".Fmt(indentPattern), "$1");
        }
    }
}