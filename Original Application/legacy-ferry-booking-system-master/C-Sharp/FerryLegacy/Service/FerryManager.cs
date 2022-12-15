using System.Collections.Generic;
using System.Linq;
using FerryLegacy.domain;

namespace FerryLegacy.Service
{
    public static class FerryManager
    {
        public static FerryJourney CreateFerryJourney(List<PortModel> ports, TimeTableEntry timetable)
        {
            if (ports == null)
                return null;

            if (timetable == null)
                return null;

            var fj = new FerryJourney
            {
                Origin = ports.Single(x => x.Id == timetable.OriginId),
                Destination = ports.Single(x => x.Id == timetable.DestinationId)
            };
            return fj;
        }

        public static void AddFerry(TimeTableEntry timetable, FerryJourney journey)
        {
            journey.Ferry = journey.Origin.GetNextAvailable(timetable.Time);
        }

        public static int GetFerryTurnaroundTime(PortModel destination)
        {
            if (destination.Id == 3)
                return 25;
            if (destination.Id == 2)
                return 20;
            return 15;
        }
    }
}