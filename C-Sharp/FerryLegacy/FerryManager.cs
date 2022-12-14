using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ServiceStack;

namespace FerryLegacy
{
    public class FerryManager
    {

        private List<Ferry> _ferries;

        private void ReadFerries()
        {
            StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\data\\ferries.txt");
            string json = reader.ReadToEnd();
            _ferries = JsonConvert.DeserializeObject<List<Ferry>>(json);

            foreach (var ferry in _ferries)
            {
                ferry.Journey = null;
            }
        }

        // Sets a ferry on a journey
        public void SetFerryJourney(Ferry ferry, Journey journey)
        {
            ferry.Journey = journey;
        }

        // Default Constructor 
        public FerryManager()
        {
            _ferries = new List<Ferry>();
            ReadFerries();
        }

        // Return list of all the ferries
        public List<Ferry> GetAllFerries()
        {
            return _ferries;
        }
 
    }
}