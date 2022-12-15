using System.Collections.Generic;
using FerryLegacy.domain;

namespace FerryLegacy.DAO
{
    public class Bookings : IBookings
    {
        private readonly List<Booking> _bookings = new List<Booking>();

        public void Create(Booking booking)
        {
            _bookings.Add(booking);
        }

        public IEnumerable<Booking> GetAll()
        {
            return _bookings;
        }
    }
}