using Newtonsoft.Json;

namespace FerryLegacy
{
    public class Ferry
    {
        //[JsonProperty("Id")]
        //public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Passengers")]
        public int Passengers { get; set; }
        [JsonProperty("Vehicles")]
        public int Vehicles { get; set; }
        [JsonProperty("Weight")]
        public decimal Weight { get; set; }
        [JsonProperty("HomePortId")]
        public int HomePortId { get; set; }
        public Journey Journey { get; set; }
    }
}