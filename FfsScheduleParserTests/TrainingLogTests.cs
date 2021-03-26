using System;
using System.Linq;
using FfsScheduleParser.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FfsScheduleParserTests
{
    [TestClass]
    public class TrainingLogTests
    {
        private const string SimulatorName = "TestSimulator";

        [TestMethod]
        public void CreateTrainingLogs_OneSessionEndsInCurrentShiftAndAnotherSessionInNextShift_ReturnsCorrectLogs()
        {
            var trainingDate = new DateTime(2021, 3, 1);
            var shiftEndTime = new DateTime(2021, 3, 1, 16, 15, 0);
            var sessionInShift = new TrainingSession
            {
                Simulator = SimulatorName,
                StartDate = trainingDate,
                StartTime = new TimeSpan(8, 0, 0),
                EndDate = trainingDate,
                EndTime = new TimeSpan(16, 15, 0),
                Status = string.Empty
            };
            var sessionInNextShift = new TrainingSession
            {
                Simulator = SimulatorName,
                StartDate = trainingDate,
                StartTime = new TimeSpan(12, 15, 0),
                EndDate = trainingDate,
                EndTime = new TimeSpan(16, 16, 0),
                Status = string.Empty
            };
            var expectedLogs = new[]
            {
                new TrainingLog
                {
                    TrainingDate = sessionInShift.StartDateTime,
                    StartTime = sessionInShift.StartTime,
                    TrainingTime = sessionInShift.EndDateTime.Subtract(sessionInShift.StartDateTime),
                    Simulator = sessionInShift.Simulator,
                    IsEndsInNextShift = sessionInShift.EndDateTime > shiftEndTime
                },
                new TrainingLog
                {
                    TrainingDate = sessionInNextShift.StartDateTime,
                    StartTime = sessionInNextShift.StartTime,
                    TrainingTime = sessionInNextShift.EndDateTime.Subtract(sessionInNextShift.StartDateTime),
                    Simulator = sessionInNextShift.Simulator,
                    IsEndsInNextShift = sessionInNextShift.EndDateTime > shiftEndTime
                }
            };

            var logs = TrainingLog.CreateTrainingLogs(new []{sessionInShift, sessionInNextShift}, shiftEndTime).ToArray();

            Assert.AreEqual(2, logs.Length);
            Assert.AreEqual(expectedLogs[0], logs[0]);
            Assert.AreEqual(expectedLogs[1], logs[1]);
        }
    }
}
