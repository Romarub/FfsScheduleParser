using System;
using FfsScheduleParser.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FfsScheduleParserTests
{
    [TestClass]
    public class StatisticTableRowTests
    {
        private readonly string SimulatorName = "TestSimulator";

        [TestMethod]
        public void CreateStatisticTableRow_GoodCase_ReturnsCorrectTableRow()
        {
            var trainingDate = new DateTime(2021, 3, 1);
            var session1 = new TrainingSession
            {
                Simulator = SimulatorName,
                StartDate = trainingDate,
                StartTime = new TimeSpan(0, 0, 0),
                EndDate = trainingDate,
                EndTime = new TimeSpan(4, 0, 0)
            };
            var session2 = new TrainingSession
            {
                Simulator = SimulatorName,
                StartDate = trainingDate,
                StartTime = new TimeSpan(4, 00, 0),
                EndDate = trainingDate,
                EndTime = new TimeSpan(24, 00, 0)
            };
            var sessions = new[] {session1, session2};
            const string expectedPlannedTime = "24:00";
            var expectedTableRow = new StatisticTableRow
            {
                Simulator = SimulatorName,
                NumberOfSessions = sessions.Length,
                PlannedTime = expectedPlannedTime,
                AchievedTime = expectedPlannedTime
            };

            var result = StatisticTableRow.CreateStatisticTableRow(SimulatorName, sessions);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTableRow.Simulator, result.Simulator);
            Assert.AreEqual(expectedTableRow.NumberOfSessions, result.NumberOfSessions);
            Assert.AreEqual(expectedTableRow.PlannedTime, result.PlannedTime);
            Assert.AreEqual(expectedTableRow.AchievedTime, result.AchievedTime);
        }
    }
}
