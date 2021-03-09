using System;
using System.Collections.Generic;
using FfsScheduleParser.Domain;

namespace FfsScheduleParser.Services
{
    public interface ITrainingSessionsService
    {
        IEnumerable<TrainingSession> GetSessionsForIntervalByEndTime(string requestUrl, DateTime start, DateTime end);
    }
}
