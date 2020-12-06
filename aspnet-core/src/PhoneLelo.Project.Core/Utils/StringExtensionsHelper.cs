using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneLelo.Project.Utils
{
    public static class StringExtensionsHelper
    {
        public static string RemoveSingleQuoteWithSpace(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : text.Replace("'", " ");
        }

        public static string RemoveSingleQuoteWithDoubleSingleQuote(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : text.Replace("'", "''");
        }


        public static DateTime? ShrinkToMinute(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
                dateTime = ShrinkToMinute(dateTime.Value);

            return dateTime;
        }

        public static DateTime ShrinkToMinute(this DateTime dateTime)
        {
            return new DateTime(
                year: dateTime.Year,
                month: dateTime.Month,
                day: dateTime.Day,
                hour: dateTime.Hour,
                minute: dateTime.Minute,
                second: 0,
                kind: dateTime.Kind
            );
        }


        public static DateTime? EndOfDay(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
                dateTime = EndOfDay(dateTime.Value);

            return dateTime;
        }

        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return new DateTime(
                year: dateTime.Year,
                month: dateTime.Month,
                day: dateTime.Day,
                hour: 23,
                minute: 59,
                second: 59,
                kind: dateTime.Kind
            );
        }
         
        public static string RemoveTrailingSlash(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            if (url != null && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }
    }
}
