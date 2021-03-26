using System;
using System.Collections.Generic;

namespace FfsScheduleParser.Domain
{
    public record WorkingShift
    {
        public DateTime StartDate { get; set; }
        public TimeSpan ShiftLength { get; set; }
        public IEnumerable<TrainingSession> Sessions { get; set; }
        public IEnumerable<StatisticTableRow> Statistic { get; set; }
        public IEnumerable<TrainingLog> ShiftLogs { get; set; }
    }
}
