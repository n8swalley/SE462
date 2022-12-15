using System;
using Newtonsoft.Json;

namespace FerryLegacy
{
    public class TimeTableEntry
    {
        [JsonProperty("Id")]

        public int Id { get; set; }

        [JsonProperty("TimeTableId")]

        public int TimeTableId { get; set; }

        [JsonProperty("OriginId")]

        public int OriginId { get; set; }

        [JsonProperty("DestinationId")]

        public int DestinationId { get; set; }

        [JsonConverter(typeof(TimeSpanJsonConverter))]

        [JsonProperty("Time")]

        public TimeSpan Time { get; set; }

        [JsonConverter(typeof(TimeSpanJsonConverter))]

        [JsonProperty("JourneyTime")]

        public TimeSpan JourneyTime { get; set; }

    }
}