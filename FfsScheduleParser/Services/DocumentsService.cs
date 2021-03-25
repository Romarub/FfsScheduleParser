using System;
using System.Collections.Generic;
using System.Linq;
using FfsScheduleParser.Domain;
using FfsScheduleParser.Infrastructure;

namespace FfsScheduleParser.Services
{
    public class DocumentsService : IDocumentsService
    {
        private readonly IDocumentsWriter _documentsWriter;

        public DocumentsService(IDocumentsWriter documentsWriter)
        {
            _documentsWriter = documentsWriter;
        }

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

        public IEnumerable<TrainingLog> GetShiftLogs(
            IReadOnlyCollection<TrainingSession> sessionsForLogs,
            DateTime shiftEnd)
        {
            return TrainingLog.CreateShiftLogs(sessionsForLogs, shiftEnd);
        }

        public void WriteStatistic(IEnumerable<StatisticTableRow> statisticTableRows, string path)
        {
            _documentsWriter.WriteStatistic(statisticTableRows, path);
        }

        public void WriteLogs(IEnumerable<TrainingLog> logs, string path)
        {
            _documentsWriter.WriteLogs(logs, path);
        }
    }
}
