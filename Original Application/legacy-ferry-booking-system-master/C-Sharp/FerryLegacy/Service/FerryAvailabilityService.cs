using System;
using System.Linq;
using FerryLegacy.domain;
using FerryLegacy.DAO;

namespace FerryLegacy.Service
{
    public class FerryAvailabilityService
    {
        private readonly IPorts _ports;
        private readonly IFerries _ferries;
        private readonly ITimeTables _timeTables;
        private readonly PortManager _portManager;

        public FerryAvailabilityService(IPorts ports, IFerries ferries, ITimeTables timeTables, PortManager portManager)
        {
            _ports = ports;
            _ferries = ferries;
            _timeTables = timeTables;
            _portManager = portManager;
        }

        public Ferry NextFerryAvailableFrom(int portId, TimeSpan time)
        {
            var ports = _portManager.PortModels();
            var allEntries = _timeTables.GetAll().SelectMany(x => x.Entries).OrderBy(x => x.Time).ToList();

            foreach (var entry in allEntries)
            {
                var ferry = FerryManager.CreateFerryJourney(ports, entry);
                if (ferry != null)
                {
                    BoatReady(entry, ferry.Destination, ferry);
                }
                if (entry.OriginId == portId)
                {
                    if (entry.Time >= time)
                    {
                        if (ferry != null)
                        {
                            return ferry.Ferry;
                        }
                    }
                }
            }

            return null;
        }

        private static void BoatReady(TimeTableEntry timetable, PortModel destination, FerryJourney ferryJourney)
        {
            if (ferryJourney.Ferry == null)
                FerryManager.AddFerry(timetable, ferryJourney);

            var ferry = ferryJourney.Ferry;

            var time = FerryModule.TimeReady(timetable, destination);
            destination.AddBoat(time, ferry);
        }
    }
}
