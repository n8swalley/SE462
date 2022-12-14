using System;
using System.Collections.Generic;
using System.Linq;

namespace FerryLegacy
{
    class SystemManager
    {
        private static BookingManager _bookingManager;
        private static JourneyManager _journeyManager;
        private static FerryManager _ferryManager;
        private static PortManager _portManager;
        private static TimeTableManager _timeTableManager;

        // Default Constructor - initializes all submanagers
        public SystemManager()
        {
            _ferryManager = new FerryManager();
            _timeTableManager = new TimeTableManager();
            _portManager = new PortManager();
            _bookingManager = new BookingManager();
            _journeyManager = new JourneyManager();
        }

        // Search - returns list of available journeys
        public static List<Journey> GetAvailableJourneys(int originPortId, int destinationPortId, TimeSpan time)
        {
            return _journeyManager.GetAvailableJourneys(originPortId, destinationPortId, time);
        }

        // Book a journey
        public static bool Book(int journeyId, int passengers, int vehicles, int weight)
        {
            return _bookingManager.Book(journeyId, passengers, vehicles, weight);
        }

        // Get all journeys
        public static List<Journey> GetAllJourneys() => _journeyManager.GetAllJourneys();

        // Get all ports
        public static List<Port> GetAllPorts() => _portManager.GetAllPorts();

        // Get all bookings
        public static List<Booking> GetAllBookings() => _bookingManager.GetAllBookings();

        // Get all ferries
        public static List<Ferry> GetAllFerries() => _ferryManager.GetAllFerries();

        // Get time table entries
        public static List<TimeTableEntry> GetFullTimeTable() => _timeTableManager.GetFullTimeTable();

        // Send a ferry to a new port
        public static void MoveFerry(int originId, int destinationId, Ferry ferry)
        {
            _portManager.MoveFerry(ferry, originId, destinationId);
        }

        // Find the next available ferry at a port to go to another port after a certain time
        public static Ferry GetNextAvailableFerry(Port origin, Port destination, TimeSpan departureTime)
        {
            return _portManager.GetNextAvailableFerry(origin, destination, departureTime);
        }

        // Get a single journey
        public static Journey GetJourney(int journeyId)
        {
            return _journeyManager.GetJourney(journeyId);
        }

        // Set a ferry on a journey
        public static void SetFerryJourney(Ferry ferry, Journey journey)
        {
            _ferryManager.SetFerryJourney(ferry, journey);
        }

        // Get a list of all the bookings for a journey
        public static List<Booking> GetBookings(int id)
        {
            return _bookingManager.GetJourneyBookings(id);
        }
    }
}
