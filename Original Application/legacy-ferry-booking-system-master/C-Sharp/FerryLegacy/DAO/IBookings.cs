using System.Collections.Generic;
using FerryLegacy.domain;

namespace FerryLegacy.DAO
{
    public interface IBookings
    {
        void Create(Booking booking);
        IEnumerable<Booking> GetAll();
    }
}