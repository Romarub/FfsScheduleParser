using System;
using System.Collections.Generic;
using System.Linq;
using FfsScheduleParser.Domain;
using FfsScheduleParser.Infrastructure;
using Newtonsoft.Json;

namespace FfsScheduleParser.Services
{
    public class TrainingSessionsService : ITrainingSessionsService
    {
        private readonly IWebClientWrapper _webClient;

        public TrainingSessionsService(IWebClientWrapper webClient)
        {
            _webClient = webClient;
        }

        public IEnumerable<TrainingSession> GetSessionsForIntervalByEndTime(string requestUrl, DateTime start, DateTime end)
        {
            var json = _webClient.DownloadString(requestUrl ?? throw new ArgumentNullException());

            return JsonConvert.DeserializeObject<List<TrainingSession>>(json)
                .Where(session => session.Status == string.Empty)
                .Where(session => session.EndDateTime >= start &&
                                  session.EndDateTime < end);
        }
    }
}
