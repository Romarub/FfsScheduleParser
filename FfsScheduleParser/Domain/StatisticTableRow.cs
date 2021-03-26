using System;
using System.Collections.Generic;
using System.Linq;

namespace FfsScheduleParser.Domain
{
    public record StatisticTableRow
    {
        public string Simulator { get; set; }
        public int NumberOfSessions { get; set; }
        public string PlannedTime { get; set; }
        public string AchievedTime { get; set; }

        public static StatisticTableRow CreateStatisticTableRow(
            string simulator,
            IReadOnlyCollection<TrainingSession> simulatorSessions)
        {
            var trainingSessions = simulatorSessions.ToList();
            var plannedTime = new TimeSpan();
            plannedTime = trainingSessions
                .Aggregate(plannedTime,
                    (current, session) => current + session.EndDateTime.Subtract(session.StartDateTime));
            var timeSpanInHoursMinutes = $"{plannedTime.TotalHours:00}:" + $"{plannedTime.Minutes:00}";

            return new StatisticTableRow
            {
                Simulator = simulator,
                NumberOfSessions = trainingSessions.Count,
                PlannedTime = timeSpanInHoursMinutes,
                AchievedTime = timeSpanInHoursMinutes
            };
        }
    }
}
