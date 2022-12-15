using System;
using System.Collections.Generic;
using System.Linq;

namespace FerryLegacy
{
    class Commands
    {
        // Edits:
        // - Separated Program.cs into Commands class
        // - Resolved bug with Displaying TimeTable
        // - added a few additional commands and cleaned code

        // system start variable
        private static bool _systemWelcome = false;
        
        // user command
        private static string _command = "";

        public static void Command(string command)
        {
            _command = command;
            if (_command != null)
                DoCommand();
        }

        // Calls corresponding method for each command
        private static void DoCommand()
        {
            if (_systemWelcome == true)
            {
                Console.WriteLine();
            }

            if (_command.StartsWith("start", StringComparison.OrdinalIgnoreCase) && _systemWelcome == false)
                StartCommand();
            else if (_command.StartsWith("search", StringComparison.OrdinalIgnoreCase))
                SearchCommand();
            else if (_command.StartsWith("book", StringComparison.OrdinalIgnoreCase))
                Book();
            else if (_command.StartsWith("random", StringComparison.OrdinalIgnoreCase))
                BookRandomCommand();
            else if (_command.StartsWith("list ports", StringComparison.OrdinalIgnoreCase))
                ListPortsCommand();
            else if (_command.StartsWith("list bookings", StringComparison.OrdinalIgnoreCase))
                ListBookingsCommand();
            else if (_command.StartsWith("display timetable", StringComparison.OrdinalIgnoreCase))
                DisplayTimeTableCommand();
            else if (_command.StartsWith("help", StringComparison.OrdinalIgnoreCase))
                PrintAllCommands();
            else if (_command.StartsWith("clear", StringComparison.OrdinalIgnoreCase))
                ClearCommand();
            else if (_command.StartsWith("exit", StringComparison.OrdinalIgnoreCase))
                ExitCommand();
            else
                GeneralError();
            Console.WriteLine();
        }

        // Start Command
        private static void StartCommand()
        {
            try
            {
                Console.WriteLine("\nWelcome to the Ferry Finding System, Your Journey Awaits!");
                Console.WriteLine("---------------------------------------------------------\n");
                DisplayTimeTableCommand();
                Console.WriteLine("\nPlease enter a command");
                PrintAllCommands();

                _systemWelcome = true;
            }
            catch (Exception)
            {
                StartCommandError();
            }
        }

        // Error Handling for failed start
        private static void StartCommandError()
        {
            Console.WriteLine("Start failed.");
        }


        // Error Handling for incorrect user input
        private static void GeneralError()
        {
            Console.WriteLine("Unrecognized Command.");
            Console.WriteLine("Type a command again or type [help] to see available commands.");
        }


        // List Ports Command
        private static void ListPortsCommand()
        {
            try
            {
                Console.WriteLine("Ports:");
                Console.WriteLine("------");
                foreach (var port in SystemManager.GetAllPorts())
                {
                    Console.WriteLine("Port {0} - {1}", port.Id, port.Name);
                }
            }
            catch (Exception)
            {
                ListPortError();
            }
        }

        // Error Handling for listing ports command
        private static void ListPortError()
        {
            Console.WriteLine("Listing ports unsuccessful.");
            PrintListCommands();

        }

        // List Bookings Command
        private static void ListBookingsCommand()
        {
            try
            {
                Console.WriteLine("Bookings:");
                Console.WriteLine("---------");
                foreach (var b in SystemManager.GetAllBookings())
                {
                    Console.WriteLine("Journey {0} from {1} to {2} has {3} passengers and {4} vehicles weighing a total of {5} tons.",
                        b.Journey.Id,
                        b.Journey.Origin.Name,
                        b.Journey.Destination.Name,
                        b.Passengers,
                        b.Journey.Vehicles,
                        b.Journey.Weight
                        );
                }
            }
            catch (Exception)
            {
                ListBookingsError();
            }
        }

        // Error Handling for listing bookings
        private static void ListBookingsError()
        {
            Console.WriteLine("Listing bookings unsuccessful.");
            PrintListCommands();
        }

        // Search command
        private static void SearchCommand()
        {
            try
            {
                string[] parts = _command.Split(' ');
                int originPortId = int.Parse(parts[1]);
                int destinationPortId = int.Parse(parts[2]);
                TimeSpan time = TimeSpan.Parse(parts[3]);

                List<Journey> availableJourneys = SystemManager.GetAvailableJourneys(originPortId, destinationPortId, time);

                foreach (var result in availableJourneys)
                {
                    Console.WriteLine();
                    Console.WriteLine("Journey Id: " + result.Id);
                    Console.WriteLine("{1} to {2} departing at {0}",
                        result.Departure.ToString("hh':'mm"),
                        result.Origin.Name,
                        result.Destination.Name
                        );
                    Console.WriteLine("(Ferry: {0}, Passenger Tickets Left: {1}, Vehicle Tickets Left: {2}, Capacity Remaining: {3} tons)",
                        result.Ferry.Name,
                        result.Seats,
                        result.Vehicles,
                        result.Weight
                        );
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                SearchError();
            }
        }

        // Error Handling for failed search
        private static void SearchError()
        {
            Console.WriteLine("Search unsuccessful.");
            PrintSearchCommand();
        }

        // Book Command
        private static void Book()
        {
            try
            {
                string[] parts = _command.Split(' ');
                int journeyId = Convert.ToInt32(parts[1]);
                int passengers = Convert.ToInt32(parts[2]);
                int vehicles = Convert.ToInt32(parts[3]);
                int weight = Convert.ToInt32(parts[4]);

                bool booked = SystemManager.Book(journeyId, passengers, vehicles, weight);

                // books an available journey

                if (booked)
                    Console.WriteLine("Booked");
                else
                    Console.WriteLine("Cannot book that journey");
            }
            catch (Exception)
            {
                BookingError();
            }
        }

        // Error Handling for failed booking
        private static void BookingError()
        {
            Console.WriteLine("Book unsuccessful.");
            PrintBookingCommand();
        }

        // Display Time Table Command
        private static void DisplayTimeTableCommand()
        {
            try
            {
                Console.WriteLine("Ferry Time Table:");
                Console.WriteLine("-----------------");

                // loop thru ports
                foreach (var port in SystemManager.GetAllPorts()) 
                {
                    // print ports headings
                    PrintPortHeader(port.Name);

                    foreach (var journey in SystemManager.GetAllJourneys().OrderBy(x => x.Origin.Name).ThenBy(x => x.Departure))
                    {
                        if (journey.Origin.Id == port.Id)
                        {
                            Console.WriteLine("| {0} | {1} | {2} | {3} | {4} |",
                                journey.Departure.ToString().PadRight(8),
                                journey.Destination.Name.PadRight(13),
                                journey.Travel.ToString().PadRight(13),
                                journey.Ferry.Name.PadRight(18),
                                journey.Arrival.ToString().PadRight(8));
                        }
                    }
                }

            }
            catch (Exception)
            {
                DisplayTimeTableError();
            }
        }

        // Error Handling for displating time table
        private static void DisplayTimeTableError()
        {
            Console.WriteLine("Display unsuccessful");
        }

        // Clear command
        private static void ClearCommand()
        {
            // clears the current console
            Console.Clear();
            Console.WriteLine("Type a command or type [help] to see available commands.");
        }

        // Terminates program
        private static void ExitCommand()
        {
            Environment.Exit(0);
        }


        // Print Display Command
        private static void PrintDisplayCommand()
        {
            Console.WriteLine("display timetable");
        }

        // Print Clear Command
        private static void PrintClearCommand()
        {
            Console.WriteLine("clear");
        }

        // Print Exit Command
        private static void PrintExitCommand()
        {
            Console.WriteLine("exit");
        }

        // Print Port Header
        private static void PrintPortHeader(string portName)
        {
            Console.WriteLine();
            Console.WriteLine("Departures from " + portName);
            Console.WriteLine();
            Console.WriteLine(" --------------------------------------------------------------------------");
            Console.WriteLine("| {0} | {1} | {2} | {3} | {4} |",
                "Time".PadRight(8),
                "Destination".PadRight(13),
                "Journey Time".PadRight(13),
                "Ferry".PadRight(18),
                "Arrives".PadRight(8));
            Console.WriteLine(" --------------------------------------------------------------------------");
        }

        // Print Search Command
        private static void PrintSearchCommand()
        {
            Console.WriteLine("Search Available Journeys:");
            Console.WriteLine("  search x y hh:mm");
            Console.WriteLine("       where x - origin port id");
            Console.WriteLine("       where y - destination port id");
            Console.WriteLine("       where hh::mm - time to search after");
        }

        // Print Booking Command
        private static void PrintBookingCommand()
        {
            Console.WriteLine("Book:");
            Console.WriteLine("  book a b c d");
            Console.WriteLine("       where a - journey id");
            Console.WriteLine("       where b - number of passengers");
            Console.WriteLine("       where c - number of vehicles");
            Console.WriteLine("       where d - total weight of vehicle(s) in tons");
        }

        // Print List Commands
        private static void PrintListCommands()
        {
            Console.WriteLine("List:");
            Console.WriteLine("  list bookings");
            Console.WriteLine("  list ports");
        }

        private static void PrintRandomTripCommand()
        {
            Console.WriteLine("Traveling alone and can't decide where to go? Let us book a random trip for you!");
            Console.WriteLine("  random trip");
        }

        // Random Trip Command - Books a random trip for the user
        private static void BookRandomCommand()
        {
            try 
            {
                // Picks random int less than 20
                Random rnd = new Random();
                int randID = rnd.Next(20);
             
                // books an available journey for one passenger and no vehicles
                bool booked = SystemManager.Book(randID, 1, 0, 0);

                if (booked)
                    Console.WriteLine("Booked");
                else
                    Console.WriteLine("Cannot book that journey");
            }   
            catch (Exception)
            {
                BookingError();
            }

        }

        // Print All Commands
        private static void PrintAllCommands()
        {
            Console.WriteLine("Commands are:");
            Console.WriteLine("--------------");
            PrintBookingCommand();
            Console.WriteLine();
            PrintRandomTripCommand();
            Console.WriteLine();
            PrintSearchCommand();
            Console.WriteLine();
            PrintListCommands();
            Console.WriteLine();
            PrintDisplayCommand();
            Console.WriteLine();
            PrintClearCommand();
            Console.WriteLine();
            PrintExitCommand();
        }
    }
}