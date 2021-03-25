using System.Collections.Generic;
using FfsScheduleParser.Domain;

namespace FfsScheduleParser.Infrastructure
{
    public interface IDocumentsWriter
    {
        void WriteStatistic(IEnumerable<StatisticTableRow> statisticTableRows, string path);
        void WriteLogs(IEnumerable<TrainingLog> logs, string path);
    }
}
