using System;
using System.Linq;
using System.Configuration;
using FfsScheduleParser.Infrastructure;
using FfsScheduleParser.Services;

namespace FfsScheduleParser
{
    class Program
    {
        static void Main()
        {
            var webClient = new WebClientWrapper();
            ITrainingSessionsService sessionsService = new TrainingSessionsService(webClient);
            const string parsingExceptionMessage = "Wrong time format";
            const string shiftStartTimeConfigKey = "ShiftStartTime";
            const string shiftLengthConfigKey = "ShiftLength";

            var shiftStartTime = shiftStartTimeConfigKey.ParseTimeSpan(parsingExceptionMessage);
            var shiftLength = shiftLengthConfigKey.ParseTimeSpan(parsingExceptionMessage);
            var requestUrl = ConfigurationManager.AppSettings.Get("UrlForSessionRequest");
            var shiftSessions = sessionsService
                .GetSessionsForIntervalByEndTime(requestUrl, DateTime.Today.Add(shiftStartTime), DateTime.Today.Add(shiftStartTime + shiftLength))
                .ToArray();

            var simulators = ConfigurationManager.AppSettings["Simulators"]?.Split(',');
            IStatisticService statisticService = new StatisticService();
            var shiftStatistic = statisticService.GetSessionsStatistic(shiftSessions, simulators);
            statisticService.WriteStatisticToCsv(
                shiftStatistic.ToArray(),
                ConfigurationManager.AppSettings.Get("CsvFilePath"));
        }
    }
}
