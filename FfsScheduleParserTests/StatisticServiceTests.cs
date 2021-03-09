using System;
using System.Collections.Generic;
using System.Linq;
using FfsScheduleParser.Domain;
using FfsScheduleParser.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FfsScheduleParserTests
{
    [TestClass]
    public class StatisticServiceTests
    {
        private readonly string SimulatorName = "TestSimulator";

        [TestMethod]
        public void GetSessionsStatistic_GoodCase_ReturnsCorrectStatistic()
        {
            var trainingDate = new DateTime(2021, 3, 1);
            var session = new TrainingSession
            {
                Simulator = SimulatorName,
                StartDate = trainingDate,
                StartTime = new TimeSpan(8, 0, 0),
                EndDate = trainingDate,
                EndTime = new TimeSpan(12, 0, 0)
            };
            var simulators = new List<string> {SimulatorName};
            var expectedStatistic = new StatisticTableRow
            {
                Simulator = SimulatorName,
                NumberOfSessions = 1,
                PlannedTime = new TimeSpan(4, 0, 0),
                AchievedTime = new TimeSpan(4, 0, 0)

            };
            var statisticService = new StatisticService();

            var statistic = statisticService.GetSessionsStatistic(new[] {session}, simulators).ToArray();

            Assert.AreEqual(1, statistic.Length);
            Assert.AreEqual(expectedStatistic.Simulator, statistic[0].Simulator);
            Assert.AreEqual(expectedStatistic.NumberOfSessions, statistic[0].NumberOfSessions);
            Assert.AreEqual(expectedStatistic.PlannedTime, statistic[0].PlannedTime);
            Assert.AreEqual(expectedStatistic.AchievedTime, statistic[0].AchievedTime);
        }
    }
}
