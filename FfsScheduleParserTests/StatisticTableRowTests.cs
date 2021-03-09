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
            //TODO создать билдер для этого класса
            var session1 = new TrainingSession
            {
                Simulator = SimulatorName,
                StartDate = trainingDate,
                StartTime = new TimeSpan(8, 0, 0),
                EndDate = trainingDate,
                EndTime = new TimeSpan(12, 0, 0),
                Status = string.Empty
            };
            var session2 = new TrainingSession
            {
                Simulator = SimulatorName,
                StartDate = trainingDate,
                StartTime = new TimeSpan(12, 15, 0),
                EndDate = trainingDate,
                EndTime = new TimeSpan(16, 15, 0),
                Status = string.Empty
            };
            var sessions = new[] {session1, session2};
            var expectedPlannedTime = session1.EndTime - session1.StartTime + session2.EndTime - session2.StartTime;
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
