using System;
using System.Collections.Generic;
using System.Linq;

namespace FfsScheduleParser.Domain
{
    public class StatisticTableRow
    {
        public string Simulator { get; set; }
        public int NumberOfSessions { get; set; }
        public TimeSpan PlannedTime { get; set; }
        public TimeSpan AchievedTime { get; set; }

        public static StatisticTableRow CreateStatisticTableRow(
            string simulator,
            IReadOnlyCollection<TrainingSession> simulatorSessions)
        {
            var trainingSessions = simulatorSessions.ToList();
            var plannedTime = new TimeSpan();
            plannedTime = trainingSessions
                .Aggregate(plannedTime,
                    (current, session) => current + session.EndDateTime.Subtract(session.StartDateTime));

            return new StatisticTableRow
            {
                Simulator = simulator,
                NumberOfSessions = trainingSessions.Count,
                PlannedTime = plannedTime,
                AchievedTime = plannedTime
            };
        }
    }
}
