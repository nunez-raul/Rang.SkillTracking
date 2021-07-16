using System;

namespace Rang.SkillTracking.Application.Common
{
    public static class DateTimeAndTimeZoneExtensions
    {
        public static DateTime FromTimeZoneTimeToUtc(this DateTime time, TimeZoneInfo sourceTimeZone)
        {
            return TimeZoneInfo.ConvertTimeToUtc(time, sourceTimeZone);
        }

        public static DateTime FromUtcToTimeZoneTime(this DateTime utcTime, TimeZoneInfo destinationTimeZone)
        {
            if (utcTime.Kind != DateTimeKind.Utc)
                throw new ArgumentException("utcTime must have a Utc kind.");

            return TimeZoneInfo.ConvertTime(utcTime, destinationTimeZone);
        }

        public static DateTime GetStartingOfWeek(this DateTime time)
        {
            var diff = time.DayOfWeek - DayOfWeek.Sunday;
            if (diff < 0)
            {
                diff += 7;
            }

            return time.AddDays(-1 * diff).Date;
        }
    }
}
