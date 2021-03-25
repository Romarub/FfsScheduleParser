using System;
using System.Collections.Generic;
using FfsScheduleParser.Domain;

namespace FfsScheduleParser.Services
{
    public interface ITrainingSessionsService
    {
        IEnumerable<TrainingSession> GetSessionsForArincMetrics(string requestUrl, DateTime start, DateTime end);
        IEnumerable<TrainingSession> GetSessionsForLogs(string requestUrl, DateTime start, DateTime end);
    }
}
