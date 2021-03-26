using System;
using System.Linq;
using FfsScheduleParser.Domain;
using FfsScheduleParser.Infrastructure;
using FfsScheduleParser.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace FfsScheduleParserTests
{
    [TestClass]
    public class TrainingSessionsServiceTests
    {
        private const string SimulatorName = "TestSimulator";
        private const string RequestAddress = "TestAddress";

        [TestMethod]
        public void GetCurrentShiftTrainingSessions_GoodCase_ReturnsTrainingSessions()
        {
            var trainingDate = new DateTime(2021, 3, 1);
            var startOfInterval = new TimeSpan(11, 0, 0);
            var intervalLength = new TimeSpan(1, 30, 0);

            var sessionInInterval = new TrainingSession
            {
                Simulator = SimulatorName,
                StartDate = trainingDate,
                StartTime = new TimeSpan(8, 0, 0),
                EndDate = trainingDate,
                EndTime = new TimeSpan(12, 0, 0),
                Status = string.Empty
            };
            var sessionOutOfInterval = new TrainingSession
            {
                Simulator = SimulatorName,
                StartDate = trainingDate,
                StartTime = new TimeSpan(8, 0, 0),
                EndDate = trainingDate,
                EndTime = new TimeSpan(14, 0, 0),
                Status = string.Empty
            };

            var serializedSessions = JsonConvert.SerializeObject(new[] {sessionInInterval, sessionOutOfInterval});

            var expectedSession = new TrainingSession
            {
                Simulator = SimulatorName,
                EndTime = new TimeSpan(12, 0, 0)
            };

            var webClientMock = new Mock<IWebClientWrapper>();
            webClientMock.Setup(c => c.DownloadString(RequestAddress)).Returns(serializedSessions);
            var sessionsService = new TrainingSessionsService(webClientMock.Object);


            var sessions = sessionsService.GetSessionsForArincMetrics(
                RequestAddress,
                trainingDate.Add(startOfInterval),
                trainingDate.Add(startOfInterval + intervalLength))
                .ToArray();


            Assert.AreEqual(1, sessions.Length);
            Assert.AreEqual(expectedSession.Simulator, sessions[0].Simulator);
            Assert.AreEqual(expectedSession.EndTime, sessions[0].EndTime);
        }

        [TestMethod]
        public void GetSessionsForLogs_GoodCase_ReturnsTrainingSessions()
        {
            var trainingDate = new DateTime(2021, 3, 1);
            var startOfInterval = new TimeSpan(11, 0, 0);
            var intervalLength = new TimeSpan(1, 30, 0);

            var sessionInInterval = new TrainingSession
            {
                Simulator = SimulatorName,
                StartDate = trainingDate,
                StartTime = startOfInterval.Add(intervalLength),
                EndDate = trainingDate,
                EndTime = new TimeSpan(12, 0, 0),
                Status = string.Empty
            };
            var sessionOutOfInterval = new TrainingSession
            {
                Simulator = SimulatorName,
                StartDate = trainingDate,
                StartTime = startOfInterval,
                EndDate = trainingDate,
                EndTime = new TimeSpan(14, 0, 0),
                Status = string.Empty
            };

            var serializedSessions = JsonConvert.SerializeObject(new[] {sessionInInterval, sessionOutOfInterval});

            var expectedSession = new TrainingSession
            {
                Simulator = SimulatorName,
                EndTime = new TimeSpan(12, 0, 0)
            };

            var webClientMock = new Mock<IWebClientWrapper>();
            webClientMock.Setup(c => c.DownloadString(RequestAddress)).Returns(serializedSessions);
            var sessionsService = new TrainingSessionsService(webClientMock.Object);


            var sessions = sessionsService.GetSessionsForLogs(
                    RequestAddress,
                    trainingDate.Add(startOfInterval),
                    trainingDate.Add(startOfInterval + intervalLength))
                .ToArray();


            Assert.AreEqual(1, sessions.Length);
            Assert.AreEqual(expectedSession.Simulator, sessions[0].Simulator);
            Assert.AreEqual(expectedSession.EndTime, sessions[0].EndTime);
        }
    }
}
