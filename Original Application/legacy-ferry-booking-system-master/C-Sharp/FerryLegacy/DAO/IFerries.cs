using System.Collections.Generic;
using FerryLegacy.domain;

namespace FerryLegacy.DAO
{
    public interface IFerries
    {
        List<Ferry> GetAll();
    }
}