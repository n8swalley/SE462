using System.Collections.Generic;
using System.Linq;

namespace FerryLegacy
{
    // Edits:
    // - consolidated bookings and booking class
    // - removed available crossing class and consolidated into Journey Manager system
    // - combined JounreyBookingService class with BookingManager


    public class BookingManager
    {

        private List<Booking> _bookings;

        // Default Constructor
        public BookingManager()
        {
            // initialize booking list
            _bookings = new List<Booking>();
        }

        // Returns list of all bookings
        public List<Booking> GetAllBookings()
        {
            return _bookings;
        }

        // Returns list of bookings for a particular journey
        public List<Booking> GetJourneyBookings(int id)
        {
            return _bookings.Where(x => x.Journey.Id == id).ToList();
        }


        // Determines if a user can book a particular journey
        private bool CanBook(int journeyId, int passengers, int vehicles, int weight)
        {
            foreach (var journey in SystemManager.GetAllJourneys())
            {
                if (journey.Id == journeyId)
                {
                    // can book based off of passengers, tickets, and weight
                    var bookings = _bookings.Where(x => x.Journey.Id == journeyId);
                    var seatsLeft = journey.Ferry.Passengers - bookings.Sum(x => x.Passengers);
                    var vehiclesLeft = journey.Ferry.Vehicles - bookings.Sum(x => x.Vehicles);
                    var weightLeft = journey.Ferry.Weight - bookings.Sum(x => x.Weight);
                    return seatsLeft >= passengers && vehiclesLeft >= vehicles && weightLeft >= weight;
                }
            }
            return false;
        }


        // Books a journey if possible
        public bool Book(int journeyId, int passengers, int vehicles, int weight)
        {
            if (CanBook(journeyId, passengers, vehicles, weight))
            {
                _bookings.Add(new Booking
                {
                    Journey = SystemManager.GetJourney(journeyId),
                    Passengers = passengers,
                    Vehicles = vehicles,
                    Weight = weight
                });
                return true;
            }
            return false;
        }
    }





}