using System;
using System.Collections.Generic;
using System.Linq;
using FerryLegacy.domain;
using FerryLegacy.DAO;

namespace FerryLegacy.Service
{
    public class PortManager
    {
        private readonly IPorts _ports;
        private readonly IFerries _ferries;

        public PortManager(IPorts ports, IFerries ferries)
        {
            _ports = ports;
            _ferries = ferries;
        }

        public List<PortModel> PortModels()
        {
            var ports = _ports.GetAll().Select(x => new PortModel(x)).ToList();
            foreach (var ferry in _ferries.GetAll())
            {
                var port = ports.Single(x => x.Id == ferry.HomePortId);
                port.AddBoat(new TimeSpan(0, 0, 10), ferry);
            }
            return ports;
        }
    }
}