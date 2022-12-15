using System.Collections.Generic;
using FerryLegacy.domain;

namespace FerryLegacy.DAO
{
    public interface IPorts
    {
        List<Port> GetAll();
    }
}