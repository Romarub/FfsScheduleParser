using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using CsvHelper;
using FfsScheduleParser.Domain;
using Configuration = FfsScheduleParser.Infrastructure.Configuration;

namespace FfsScheduleParser.Services
{
    public class StatisticService : IStatisticService
    {
        public IEnumerable<StatisticTableRow> GetSessionsStatistic(
            IReadOnlyCollection<TrainingSession> sessions,
            IReadOnlyCollection<string> simulators)
        {
            var statisticTableRows = new List<StatisticTableRow>();
            foreach (var simulator in simulators)
            {
                var simulatorSessions = sessions
                    .Where(ses => ses.Simulator == simulator)
                    .ToArray();
                statisticTableRows.Add(StatisticTableRow.CreateStatisticTableRow(simulator, simulatorSessions));
            }

            return statisticTableRows;
        }

        public void WriteStatisticToCsv(IEnumerable<StatisticTableRow> statisticTableRows, string filePath)
        {
            var tableRows = statisticTableRows.ToList();
            RenameSimulatorsForCsvFormat(tableRows);
            using var writer = new StreamWriter(filePath ?? throw new ArgumentNullException(nameof(filePath)));
            using var csv = new CsvWriter(writer, Configuration.Csv);
            csv.Context.TypeConverterOptionsCache.AddOptions<TimeSpan>(Configuration.CsvTimeFormatConverterOptions);
            csv.WriteRecords(tableRows);
        }

        private static void RenameSimulatorsForCsvFormat(IEnumerable<StatisticTableRow> statisticTableRows)
        {
            foreach (var tableRow in statisticTableRows)
            {
                tableRow.Simulator = ConfigurationManager.AppSettings.Get(tableRow.Simulator);
            }
        }
    }
}
