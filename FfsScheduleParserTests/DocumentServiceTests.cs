 using System;
 using System.Collections.Generic;
 using System.Linq;
 using FfsScheduleParser.Domain;
 using FfsScheduleParser.Infrastructure;
 using FfsScheduleParser.Services;
 using Microsoft.VisualStudio.TestTools.UnitTesting;
 using Moq;

 namespace FfsScheduleParserTests
 {
     [TestClass]
     public class DocumentServiceTests
     {
         private const string SimulatorName = "TestSimulator";

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
             const string expectedSessionTime = "04:00";
             var expectedStatistic = new StatisticTableRow
             {
                 Simulator = SimulatorName,
                 NumberOfSessions = 1,
                 PlannedTime = expectedSessionTime,
                 AchievedTime = expectedSessionTime

             };
             var docWriterMock = new Mock<IDocumentsWriter>();
             var documentsService = new DocumentsService(docWriterMock.Object);

             var statistic = documentsService.GetSessionsStatistic(new[] {session}, simulators).ToArray();

             Assert.AreEqual(1, statistic.Length);
             Assert.AreEqual(expectedStatistic, statistic[0]);
         }

         [TestMethod]
         public void GetTrainingLogs_GoodCase_ReturnsCorrectLogs()
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
             var docWriterMock = new Mock<IDocumentsWriter>();
             var documentsService = new DocumentsService(docWriterMock.Object);

             var logs = documentsService.GetTrainingLogs(new[] {sessionInShift, sessionInNextShift}, shiftEndTime).ToArray();

             Assert.AreEqual(2, logs.Length);
             Assert.AreEqual(expectedLogs[0], logs[0]);
             Assert.AreEqual(expectedLogs[1], logs[1]);
         }
     }
 }
