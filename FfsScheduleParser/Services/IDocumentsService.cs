using System;
using System.Collections.Generic;
using FfsScheduleParser.Domain;

namespace FfsScheduleParser.Services
{
    public interface IDocumentsService
    {
        IEnumerable<StatisticTableRow> GetSessionsStatistic(
            IReadOnlyCollection<TrainingSession> sessions,
            IReadOnlyCollection<string> simulators);
        void WriteStatistic(IEnumerable<StatisticTableRow> statisticTableRows, string path);
        public void WriteLogs(IEnumerable<TrainingLog> logs, string path);
        IEnumerable<TrainingLog> GetTrainingLogs(IReadOnlyCollection<TrainingSession> sessionsForLogs, DateTime shiftEnd);
    }
}
