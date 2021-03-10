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
        private readonly IStatisticService _statisticService;
        private const string ParsingTimeExceptionMessage = "Wrong time format";
        private const string ShiftStartTimeConfigKey = "ShiftStartTime";
        private const string ShiftLengthConfigKey = "ShiftLength";

        public WorkingShiftService(ITrainingSessionsService sessionsService, IStatisticService statisticService)
        {
            _sessionsService = sessionsService;
            _statisticService = statisticService;
        }

        public WorkingShift CreateShiftWithConfigParams(DateTime startDate)
        {
            var shiftStartTime = ShiftStartTimeConfigKey.ParseTimeSpan(ParsingTimeExceptionMessage);
            var shiftLength = ShiftLengthConfigKey.ParseTimeSpan(ParsingTimeExceptionMessage);
            var shiftSessions = _sessionsService.GetSessionsForIntervalByEndTime(
                ConfigurationManager.AppSettings.Get("UrlForSessionRequest"),
                startDate.Add(shiftStartTime),
                startDate.Add(shiftStartTime + shiftLength))
                .ToArray();
            var simulators = ConfigurationManager.AppSettings["Simulators"]?.Split(',');
            return new WorkingShift
            {
                StartDate = DateTime.Today,
                ShiftLength = shiftLength,
                Sessions = shiftSessions,
                Statistic = _statisticService.GetSessionsStatistic(shiftSessions, simulators)
            };
        }
    }
}
