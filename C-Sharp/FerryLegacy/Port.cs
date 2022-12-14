using System.Collections.Generic;
using Newtonsoft.Json;

namespace FerryLegacy
{
    public class Port
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        public List<Ferry> Ferries { get; set; }
        public List<TimeTableEntry> TimeTable { get; set; }
    }
}