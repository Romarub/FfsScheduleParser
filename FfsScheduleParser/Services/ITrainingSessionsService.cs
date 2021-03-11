using System;
using System.Collections.Generic;
using FfsScheduleParser.Domain;

namespace FfsScheduleParser.Services
{
    public interface ITrainingSessionsService
    {
        IEnumerable<TrainingSession> GetTrainingSessionsForIntervalByEndTime(string requestUrl, DateTime start, DateTime end);
    }
}
