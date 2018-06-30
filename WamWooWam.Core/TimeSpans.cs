using System;
using System.Collections.Generic;
using System.Text;

namespace WamWooWam.Core
{
    public static class TimeSpans
    {
        public static string ToNaturalString(this TimeSpan time)
        {
            if (time.Days > 0)
            {
                return $"{time.Days} {(time.Days == 1 ? "day" : "days")} {time.Hours} {(time.Hours == 1 ? "hour" : "hours")} {time.Minutes} {(time.Minutes == 1 ? "minute" : "minutes")} and {time.Seconds} {(time.Seconds == 1 ? "second" : "seconds")}";
            }

            if (time.Hours > 0)
            {
                return $"{time.Hours} {(time.Hours == 1 ? "hour" : "hours")} {time.Minutes} {(time.Minutes == 1 ? "minute" : "minutes")} and {time.Seconds} {(time.Seconds == 1 ? "second" : "seconds")}";
            }

            if (time.Minutes > 0)
            {
                return $"{time.Minutes} {(time.Minutes == 1 ? "minute" : "minutes")} and {time.Seconds} {(time.Seconds == 1 ? "second" : "seconds")}";
            }

            if (time.Seconds > 0)
            {
                return $"{time.Seconds} {(time.Seconds == 1 ? "second" : "seconds")}";
            }
            else
            {
                return $"{time.Milliseconds} {(time.Milliseconds == 1 ? "millisecond" : "milliseconds")}";
            }
        }
    }
}
