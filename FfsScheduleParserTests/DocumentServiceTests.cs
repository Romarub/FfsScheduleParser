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
             var statisticService = new DocumentsService(docWriterMock.Object);

             var statistic = statisticService.GetSessionsStatistic(new[] {session}, simulators).ToArray();

             Assert.AreEqual(1, statistic.Length);
             Assert.AreEqual(expectedStatistic.Simulator, statistic[0].Simulator);
             Assert.AreEqual(expectedStatistic.NumberOfSessions, statistic[0].NumberOfSessions);
             Assert.AreEqual(expectedStatistic.PlannedTime, statistic[0].PlannedTime);
             Assert.AreEqual(expectedStatistic.AchievedTime, statistic[0].AchievedTime);
         }
     }
 }
