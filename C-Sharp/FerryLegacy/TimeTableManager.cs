using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FerryLegacy
{
    // Edits:

    // Fixed bug with time table 
    public class TimeTableManager
    {
        private List<TimeTableEntry> _timeTableEntries;

        // Default Constructor
        public TimeTableManager()
        {
            var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\data\\timetable.txt");
            string json = reader.ReadToEnd();
            _timeTableEntries = JsonConvert.DeserializeObject<List<TimeTableEntry>>(json);
        }

        // Return full list of time table entries
        public List<TimeTableEntry> GetFullTimeTable()
        {
            return _timeTableEntries;
        }
    }
}