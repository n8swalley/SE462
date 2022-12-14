using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FerryLegacy
{
    public class PortManager
    {
        private static List<Port> _ports;

        private void ReadPorts()
        {
            StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\data\\ports.txt");
            string json = reader.ReadToEnd();
            _ports = JsonConvert.DeserializeObject<List<Port>>(json);

            foreach (var port in _ports)
            {
                port.Ferries = new List<Ferry>();
                port.TimeTable = new List<TimeTableEntry>();
            }
        }

        // Assign ferries to their ports
        private void PortFerries()
        {
            foreach (var port in _ports)
            {
                foreach (var ferry in SystemManager.GetAllFerries())
                {
                    if (ferry.HomePortId == port.Id)
                    {
                        port.Ferries.Add(ferry);
                    }
                }
            }
        }

        // Assign a time table slot to a port
        private void PortTimeTable()
        {
            foreach (var port in _ports)
            {
                foreach (var entry in SystemManager.GetFullTimeTable())
                {
                    if (entry.TimeTableId == port.Id)
                    {
                        entry.OriginId = entry.TimeTableId;
                        port.TimeTable.Add(entry);
                    }
                }
            }
        }

        // Default Constructor
        public PortManager()
        {
            _ports = new List<Port>();
            ReadPorts();
            PortFerries();
            PortTimeTable();
        }

        // Return list of all the ports
        public List<Port> GetAllPorts()
        {
            return _ports;
        }

        // Return next available ferry at a port to leave to another port at a specific time
        public Ferry GetNextAvailableFerry(Port origin, Port destination, TimeSpan departureTime)
        {
            Port port = _ports.Find(x => x.Id == origin.Id);

            //Check to see if a ferry arrived from the next destination's port and is able to go
            foreach (var ferry in port.Ferries)
            {
                if (destination.Id != ferry.HomePortId)
                    continue;
                if (ferry.Journey != null)
                {
                    if (ferry.Journey.Arrival < departureTime)
                    {
                        return ferry;
                    }
                }
            }

            //Otherwise send the next availble ferry
            foreach (var ferry in port.Ferries)
            {
                if (destination.Id != ferry.HomePortId && origin.Id != ferry.HomePortId)
                    continue;
                if (ferry.Journey != null)
                {
                    if (ferry.Journey.Arrival < departureTime)
                    {
                        return ferry;
                    }
                }
                else
                    return ferry;
            }

            return null;
        }

        // Move ferry to a new port
        public void MoveFerry(Ferry ferry, int originId, int destinationId)
        {
            Port oldPort = _ports.Find(x => x.Id == originId);
            oldPort.Ferries.Remove(ferry);
            Port newPort = _ports.Find(x => x.Id == destinationId);
            newPort.Ferries.Add(ferry);
        }
    }
}