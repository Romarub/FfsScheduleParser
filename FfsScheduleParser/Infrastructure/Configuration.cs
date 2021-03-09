﻿using System.Configuration;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace FfsScheduleParser.Infrastructure
{
    public static class Configuration
    {
        public static CsvConfiguration Csv { get; set; } = new(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            Delimiter = ", "
        };

        public static TypeConverterOptions CsvConverterOptions { get; set; } = new()
            {Formats = new[] {ConfigurationManager.AppSettings.Get("CsvTimeFormat")}};
    }
}
