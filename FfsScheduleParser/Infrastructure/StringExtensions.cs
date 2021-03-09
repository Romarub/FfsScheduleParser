using System;
using System.Configuration;

namespace FfsScheduleParser.Infrastructure
{
    public static class StringExtensions
    {
        public static TimeSpan ParseTimeSpan(this string configKey, string exceptionMessage)
        {
            var time = ConfigurationManager.AppSettings.Get(configKey);
            if (!TimeSpan.TryParse(time, out var parsedTime))
                throw new ArgumentException(exceptionMessage, configKey);

            return parsedTime;
        }
    }
}
