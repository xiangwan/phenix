using System;

namespace Phenix.Infrastructure.Extensions
{
    public static class DateExtensions
    {
        public static string ToPrettyDate(this DateTime date)
        {
            var timeSince = DateTime.Now.Subtract(date);
            if (timeSince.TotalMilliseconds < 1) return "not yet";
            if (timeSince.TotalMinutes < 1) return "just now";
            if (timeSince.TotalMinutes < 2) return "1 minute ago";
            if (timeSince.TotalMinutes < 60) return string.Format("{0} minutes ago", timeSince.Minutes);
            if (timeSince.TotalMinutes < 120) return "1 hour ago";
            if (timeSince.TotalHours < 24) return string.Format("{0} hours ago", timeSince.Hours);
            if (timeSince.TotalDays == 1) return "yesterday";
            if (timeSince.TotalDays < 7) return string.Format("{0} day(s) ago", timeSince.Days);
            if (timeSince.TotalDays < 14) return "last week";
            if (timeSince.TotalDays < 21) return "2 weeks ago";
            if (timeSince.TotalDays < 28) return "3 weeks ago";
            if (timeSince.TotalDays < 60) return "last month";
            if (timeSince.TotalDays < 365) return string.Format("{0} months ago", Math.Round(timeSince.TotalDays / 30));
            if (timeSince.TotalDays < 730) return "last year";
            return string.Format("{0} years ago", Math.Round(timeSince.TotalDays / 365));
        }
  
        public static int DaysAgo(this DateTime dt1,DateTime dt2) {
            var diff=dt1.Date.Subtract(dt2.Date);
            return Math.Abs(diff.Days); 
        }
        public static int DaysAgo(this DateTime dt) {
            return DaysAgo(DateTime.Now, dt);
        } 
        public static DateTime ToRealTime(this string unixTime)
        { 
            var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(unixTime + "0000000");
            var toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow); 
        } 

        public static DateTime ToRealTime(this long unixTime) {
            return unixTime.ToString().ToRealTime();
        }
        public static long ToUnixTime(this DateTime dt)
        {
            var dtstart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Math.Floor((dt - dtstart).TotalSeconds).ToString().ToLong();
        }
    }
}