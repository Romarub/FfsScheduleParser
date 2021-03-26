using System;
using System.Configuration;
using System.Linq;
using FfsScheduleParser.Domain;
using FfsScheduleParser.Infrastructure;

namespace FfsScheduleParser.Services
{
    public class WorkingShiftService : IWorkingShiftService
    {
        private readonly ITrainingSessionsService _sessionsService;
        private readonly IDocumentsService _documentsService;
        private const string ParsingTimeExceptionMessage = "Wrong time format";
        private const string ShiftStartTimeConfigKey = "ShiftStartTime";
        private const string ShiftLengthConfigKey = "ShiftLength";

        public WorkingShiftService(ITrainingSessionsService sessionsService, IDocumentsService documentsService)
        {
            _sessionsService = sessionsService;
            _documentsService = documentsService;
        }

        public WorkingShift CreateShiftWithConfigParams(DateTime startDate)
        {
            var shiftStartTime = ShiftStartTimeConfigKey.ParseTimeSpan(ParsingTimeExceptionMessage);
            var shiftLength = ShiftLengthConfigKey.ParseTimeSpan(ParsingTimeExceptionMessage);
            var shiftStart = startDate.Add(shiftStartTime);
            var shiftEnd = startDate.Add(shiftStartTime + shiftLength);
            var shiftSessions = _sessionsService.GetSessionsForArincMetrics(
                ConfigurationManager.AppSettings.Get("UriForSessionRequest"),
                shiftStart,
                shiftEnd)
                .ToArray();
            var sessionsForLogs = _sessionsService.GetSessionsForLogs(
                ConfigurationManager.AppSettings.Get("UriForSessionRequest"),
                shiftStart,
                shiftEnd)
                .ToArray();
            var simulators = ConfigurationManager.AppSettings["Simulators"]?.Split(',');
            return new WorkingShift
            {
                StartDate = DateTime.Today,
                ShiftLength = shiftLength,
                Sessions = shiftSessions,
                Statistic = _documentsService.GetSessionsStatistic(shiftSessions, simulators),
                ShiftLogs = _documentsService.GetTrainingLogs(sessionsForLogs, shiftEnd)
            };
        }
    }
}
