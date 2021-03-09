using System.Collections.Generic;
using FfsScheduleParser.Domain;

namespace FfsScheduleParser.Services
{
    public interface IStatisticService
    {
        IEnumerable<StatisticTableRow> GetSessionsStatistic(
            IReadOnlyCollection<TrainingSession> sessions,
            IReadOnlyCollection<string> simulators);
        void WriteStatisticToCsv(IEnumerable<StatisticTableRow> statisticTableRows, string filePath);
    }
}
