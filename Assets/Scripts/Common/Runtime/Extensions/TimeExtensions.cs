using System;
using System.Text;

namespace Modules.CommonModule.Extensions
{
    [Flags]
    public enum TimeConvert
    {
        All = ~0,
        None = 0,
        Milliseconds = 1 << 0,
        Seconds = 1 << 1,
        Minutes = 1 << 2,
        Hours = 1 << 3,
        Days = 1 << 4,
    }

    public static class TimeExtensions
    {
        private const string DAYS_LETTER = "d";
        private const string HOURS_LETTER = "h";
        private const string MINUTES_LETTER = "m";
        private const string SECONDS_LETTER = "s";
        private const string MILLISECONDS_LETTER = "ms";
        private const string TIME_FORMAT = "{0:D2}{1}{2}";
        private const string EMPTY = "";


        public static string ToTimeFormat(this TimeSpan timeSpan, TimeConvert convertType = TimeConvert.All,
            bool useLetters = false,
            char separator = ':',
            TimeConvert allowZero = TimeConvert.All,
            int depth = int.MaxValue,
            TimeConvert totalValues = TimeConvert.None)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int depthCounter = 0;

            int days = totalValues.HasFlag(TimeConvert.Days) ? (int)timeSpan.TotalDays : timeSpan.Days;
            if (depthCounter < depth &&
                (allowZero.HasFlag(TimeConvert.Days) || days > 0f))
            {
                stringBuilder.Append(convertType.HasFlag(TimeConvert.Days)
                    ? $"{days:D2}{(useLetters ? DAYS_LETTER : EMPTY)}{separator}"
                    : EMPTY);
                depthCounter++;
            }

            int hours = totalValues.HasFlag(TimeConvert.Hours) ? (int)timeSpan.TotalHours : timeSpan.Hours;
            if (depthCounter < depth &&
                (allowZero.HasFlag(TimeConvert.Hours) || hours > 0f || days > 0f))
            {
                stringBuilder.Append(convertType.HasFlag(TimeConvert.Hours)
                    ? $"{hours:D2}{(useLetters ? HOURS_LETTER : EMPTY)}{separator}"
                    : EMPTY);
                depthCounter++;
            }


            int minutes = totalValues.HasFlag(TimeConvert.Minutes) ? (int)timeSpan.TotalMinutes : timeSpan.Minutes;
            if (depthCounter < depth &&
                (allowZero.HasFlag(TimeConvert.Minutes) || minutes > 0f || hours > 0f || days > 0f))
            {
                stringBuilder.Append(convertType.HasFlag(TimeConvert.Minutes)
                    ? $"{minutes:D2}{(useLetters ? MINUTES_LETTER : EMPTY)}{separator}"
                    : EMPTY);
                depthCounter++;
            }

            int seconds = totalValues.HasFlag(TimeConvert.Seconds) ? (int)timeSpan.TotalSeconds : timeSpan.Seconds;
            if (depthCounter < depth &&
                (allowZero.HasFlag(TimeConvert.Seconds) || seconds > 0f || minutes > 0f || hours > 0f || days > 0f))
            {
                stringBuilder.Append(convertType.HasFlag(TimeConvert.Seconds)
                    ? $"{seconds:D2}{(useLetters ? SECONDS_LETTER : EMPTY)}{separator}"
                    : EMPTY);
                depthCounter++;
            }

            int milliseconds = totalValues.HasFlag(TimeConvert.Milliseconds)
                ? (int)timeSpan.TotalMilliseconds
                : timeSpan.Milliseconds;
            if (depthCounter < depth &&
                (allowZero.HasFlag(TimeConvert.Milliseconds) || milliseconds > 0f || seconds > 0f || minutes > 0f ||
                 hours > 0f || days > 0f))
            {
                stringBuilder.Append(convertType.HasFlag(TimeConvert.Milliseconds)
                    ? $"{milliseconds:D2}{(useLetters ? MILLISECONDS_LETTER : EMPTY)}{separator}"
                    : EMPTY);
            }

            return stringBuilder.ToString().TrimEnd(separator);
        }
    }
}
