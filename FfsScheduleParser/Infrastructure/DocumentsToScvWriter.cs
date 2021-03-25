using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using FfsScheduleParser.Domain;

namespace FfsScheduleParser.Infrastructure
{
    public class DocumentsToScvWriter : IDocumentsWriter
    {
        public void WriteStatistic(IEnumerable<StatisticTableRow> statisticTableRows, string path)
        {
            var tableRows = statisticTableRows.ToList();
            RenameSimulatorsForCsvFormat(tableRows);
            using var writer = new StreamWriter(path ?? throw new ArgumentNullException(nameof(path)));
            using var csv = new CsvWriter(writer, CsvSettings());
            csv.Context.TypeConverterOptionsCache.AddOptions<TimeSpan>(CsvTimeSpanFormatConverterOptions());
            csv.WriteRecords(tableRows);
        }

        private static void RenameSimulatorsForCsvFormat(IEnumerable<StatisticTableRow> statisticTableRows)
        {
            foreach (var tableRow in statisticTableRows)
            {
                tableRow.Simulator = ConfigurationManager.AppSettings.Get(tableRow.Simulator);
            }
        }

        public void WriteLogs(IEnumerable<TrainingLog> logs, string path)
        {
            using var writer = new StreamWriter(path ?? throw new ArgumentNullException(nameof(path)));
            using var csv = new CsvWriter(writer, CsvSettings());
            csv.Context.TypeConverterOptionsCache.AddOptions<TimeSpan>(CsvTimeSpanFormatConverterOptions());
            csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(new TypeConverterOptions
                {Formats = new[] {ConfigurationManager.AppSettings.Get("CsvLogDateFormat")}});
            csv.WriteRecords(logs);
        }

        private static CsvConfiguration CsvSettings()
        {
            return new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ", "
            };
        }

        private static TypeConverterOptions CsvTimeSpanFormatConverterOptions()
        {
            return new()
                {Formats = new[] {ConfigurationManager.AppSettings.Get("CsvTimeSpanFormat")}};
        }
    }
}
