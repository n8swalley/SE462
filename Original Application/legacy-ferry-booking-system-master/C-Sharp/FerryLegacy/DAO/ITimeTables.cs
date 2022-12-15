using System.Collections.Generic;
using FerryLegacy.domain;

namespace FerryLegacy.DAO
{
    public interface ITimeTables
    {
        List<TimeTable> GetAll();
    }
}