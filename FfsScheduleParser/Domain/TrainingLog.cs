using System;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace FfsScheduleParser.Domain
{
    public class TrainingLog
    {
        public string SessionNumber { get; set; }
        public string Company { get; set; }
        public int Day => TrainingDate.Day;
        public int Month => TrainingDate.Month;
        public int Year => TrainingDate.Year;
        public DateTime TrainingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan TrainingTime { get; set; }
        public bool IsEndsInNextShift { get; set; }

        [Ignore]
        public string Simulator { get; set; }

        public static IEnumerable<TrainingLog> CreateShiftLogs(
            IReadOnlyCollection<TrainingSession> sessionsForLogs,
            DateTime shiftEnd)
        {
            foreach (var session in sessionsForLogs)
            {
                yield return new TrainingLog
                {
                    SessionNumber = session.SessionNumber,
                    Company = session.Company,
                    TrainingDate = session.StartDateTime,
                    StartTime = session.StartTime,
                    TrainingTime = session.EndDateTime.Subtract(session.StartDateTime),
                    Simulator = session.Simulator,
                    IsEndsInNextShift = session.EndDateTime > shiftEnd
                };
            }
        }
    }
}
