using System;
using Newtonsoft.Json;

namespace FfsScheduleParser.Domain
{
    public class TrainingSession
    {
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("startTime")]
        public TimeSpan StartTime { get; set; }

        public DateTime StartDateTime => StartDate + StartTime;

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("endTime")]
        public TimeSpan EndTime { get; set; }

        public DateTime EndDateTime => EndDate + EndTime;

        [JsonProperty("ak")]
        public string Company { get; set; }

        [JsonProperty("tren")]
        public string Simulator { get; set; }

        [JsonProperty("instr")]
        public string Instructor { get; set; }

        [JsonProperty("shn1")]
        public string FirstPilot { get; set; }

        [JsonProperty("shn2")]
        public string SecondPilot { get; set; }

        [JsonProperty("sess")]
        public string SessionNumber { get; set; }

        [JsonProperty("class")]
        public string Status { get; set; }
    }
}
