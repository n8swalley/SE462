using System;
using System.Collections.Generic;
using System.Linq;

namespace FerryLegacy
{
    class JourneyManager
    {
        private static List<Journey> _journeys;

        // Creates list of possible journeys 
        private static void CreateJourneys()
        {
            var timetable = SystemManager.GetFullTimeTable().OrderBy(x => x.Time).ThenBy(x => x.OriginId);
            var ports = SystemManager.GetAllPorts();

            foreach (var entry in timetable)
            {
                Port fromPort = ports.Where(x => x.Id == entry.OriginId).Single();
                Port toPort = ports.Where(x => x.Id == entry.DestinationId).Single();
                Ferry ferry = SystemManager.GetNextAvailableFerry(fromPort, toPort, entry.Time);
                Journey journey = new Journey
                {
                    Id = entry.Id,
                    Origin = fromPort,
                    Destination = toPort,
                    Departure = entry.Time,
                    Travel = entry.JourneyTime,
                    Arrival = entry.Time + entry.JourneyTime,
                    Ferry = ferry,
                    Seats = ferry.Passengers,
                    Vehicles = ferry.Vehicles,
                    Weight = ferry.Weight
                };

                _journeys.Add(journey);
                SystemManager.SetFerryJourney(ferry, journey);
                SystemManager.MoveFerry(entry.OriginId, entry.DestinationId, ferry);
            }
        }

        // Default Constructor
        public JourneyManager()
        {
            _journeys = new List<Journey>();
            CreateJourneys();
        }

        // Returns list of all the journeys
        public List<Journey> GetAllJourneys()
        {
            return _journeys;
        }

        // Returns specific journey
        public Journey GetJourney(int journeyId)
        {
            return _journeys.Single(x => x.Id == journeyId);
        }

 
        // Returns list of available journeys
        public List<Journey> GetAvailableJourneys(int fromPort, int toPort, TimeSpan time)
        {
            List<Journey> available = new List<Journey>();
            foreach (var journey in _journeys)
            {
                if (toPort == journey.Destination.Id && fromPort == journey.Origin.Id)
                {
                    if (journey.Departure >= time)
                    {
                        List<Booking> bookings = SystemManager.GetBookings(journey.Id);
                        var seatsLeft = journey.Ferry.Passengers - bookings.Sum(x => x.Passengers);
                        var vehiclesLeft = journey.Ferry.Vehicles - bookings.Sum(x => x.Vehicles);
                        var weightLeft = journey.Ferry.Weight - bookings.Sum(x => x.Weight);
                        if (seatsLeft > 0)
                        {
                            journey.Seats = seatsLeft;
                            journey.Vehicles = vehiclesLeft;
                            journey.Weight = weightLeft;
                            available.Add(journey);
                        }
                    }
                }
            }
            return available;
        }
    }
}
