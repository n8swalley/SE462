//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FerryLegacy
//{
//    class Commands
//    {
//        // stores user command
//        private static string userCmd = " ";

//        // do commands here

//        public static void Command(string command) 
//        {
//            userCmd = command;

//            if (userCmd != null)
//            {
//                runCmd();
//            }

//        }

//        private static void runCmd()
//        {
//            // call corresponding private method for each command
//        }

//        private static void startCmd()
//        {
//            // start command
//        }

//        private static void printAllCmds()
//        {
//           // print all commands
//        }

//        private static void exitCmd()
//        {

//        }






//    }
//}




using System;
using System.Collections.Generic;
using System.Linq;

namespace FerryLegacy
{
    class Commands
    {
        // Management System Connection
        // Variable to determine if system has started
        private static bool _systemWelcome = false;
        // Varaible to store user command
        private static string _command = "";

        // Command - attempts to do the command
        public static void Command(string command)
        {
            _command = command;
            if (_command != null)
                DoCommand();
        }

        // Do Command in Command String - Calls corresponding private method for each command
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

        // General Error - Tells user command is not recognized
        private static void GeneralError()
        {
            Console.WriteLine("Unrecognized Command.");
            Console.WriteLine("Type a command again or type [help] to see available commands.");
        }

        // Start Command - Welcome to system and gives basic instructions
        private static void StartCommand()
        {
            try
            {
                Console.WriteLine("Welcome to the Ferry Finding System");
                Console.WriteLine("=====================================");
                Console.WriteLine("Type a command or type [help] to see available commands.");
                _systemWelcome = true;
            }
            catch (Exception)
            {
                StartCommandError();
            }
        }

        // Start Command Error - Displays startup error
        private static void StartCommandError()
        {
            Console.WriteLine("Start failed.");
        }

        // List Ports Command  - Displays all ports
        private static void ListPortsCommand()
        {
            try
            {
                Console.WriteLine("Ports:");
                Console.WriteLine("------");
                foreach (var port in ManagementSystem.GetAllPorts())
                {
                    Console.WriteLine("Port {0} - {1}", port.Id, port.Name);
                }
            }
            catch (Exception)
            {
                ListPortError();
            }
        }

        // List Port Error - Displays listing error
        private static void ListPortError()
        {
            Console.WriteLine("Listing ports unsuccessful.");
            PrintListCommands();

        }

        // List Bookings Command - Displays all bookings
        private static void ListBookingsCommand()
        {
            try
            {
                Console.WriteLine("Bookings:");
                Console.WriteLine("---------");
                foreach (var b in ManagementSystem.GetAllBookings())
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

        // List Bookings Error - Displays listing error
        private static void ListBookingsError()
        {
            Console.WriteLine("Listing bookings unsuccessful.");
            PrintListCommands();
        }

        // Search Command - Displays available journeys found in search
        private static void SearchCommand()
        {
            try
            {
                string[] parts = _command.Split(' ');
                int originPortId = int.Parse(parts[1]);
                int destinationPortId = int.Parse(parts[2]);
                TimeSpan time = TimeSpan.Parse(parts[3]);

                List<Journey> availableJourneys = ManagementSystem.GetAvailableJourneys(originPortId, destinationPortId, time);

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

        // Search Error - Displays search error
        private static void SearchError()
        {
            Console.WriteLine("Search unsuccessful.");
            PrintSearchCommand();
        }

        // Book Command - Books an available journey
        private static void Book()
        {
            try
            {
                string[] parts = _command.Split(' ');
                int journeyId = Convert.ToInt32(parts[1]);
                int passengers = Convert.ToInt32(parts[2]);
                int vehicles = Convert.ToInt32(parts[3]);
                int weight = Convert.ToInt32(parts[4]);

                bool booked = ManagementSystem.Book(journeyId, passengers, vehicles, weight);

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

        // Booking Error - Displays booking error
        private static void BookingError()
        {
            Console.WriteLine("Book unsuccessful.");
            PrintBookingCommand();
        }

        // Display Time Table Command - Displays the time table
        private static void DisplayTimeTableCommand()
        {
            try
            {
                Console.WriteLine("Ferry Time Table:");
                Console.WriteLine("-----------------");

                foreach (var port in ManagementSystem.GetAllPorts())
                {
                    PrintPortHeader(port.Name);

                    // Every Journey is based on an entry in the timetable, use them to display timetable
                    foreach (var journey in ManagementSystem.GetAllJourneys().OrderBy(x => x.Origin.Name).ThenBy(x => x.Departure))
                    {
                        if (journey.Origin.Id == port.Id)
                        {
                            Console.WriteLine("| {0} | {1} | {2} | {3} | {4} |",
                                journey.Departure.ToString().PadRight(8),
                                journey.Destination.Name.PadRight(13),
                                journey.Travel.ToString().PadRight(13),
                                journey.Ferry.Name.PadRight(18),
                                journey.Arrival.ToString().PadRight(8)
                                );
                        }
                    }
                }

            }
            catch (Exception)
            {
                DisplayTimeTableError();
            }
        }

        // Display Time Table Error - Displays display error
        private static void DisplayTimeTableError()
        {
            Console.WriteLine("Display unsuccessful");
        }

        // Clear Command - Clear the console
        private static void ClearCommand()
        {
            Console.Clear();
            Console.WriteLine("Type a command or type [help] to see available commands.");
        }

        // Clear Command - Clear the console
        private static void ExitCommand()
        {
            Environment.Exit(0);
        }

        // Print Port Header - Prints header for display time table
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

        // Print Search Command - Prints the search command
        private static void PrintSearchCommand()
        {
            Console.WriteLine("Search Available Journeys:");
            Console.WriteLine("  search x y hh:mm");
            Console.WriteLine("       where x - origin port id");
            Console.WriteLine("       where y - destination port id");
            Console.WriteLine("       where hh::mm - time to search after");
        }

        // Print Booking Command - Prints the booking command
        private static void PrintBookingCommand()
        {
            Console.WriteLine("Book:");
            Console.WriteLine("  book a b c d");
            Console.WriteLine("       where a - journey id");
            Console.WriteLine("       where b - number of passengers");
            Console.WriteLine("       where c - number of vehicles");
            Console.WriteLine("       where d - total vehicle(s) weight in tons");
        }

        // Print List Commands - Prints the list command options
        private static void PrintListCommands()
        {
            Console.WriteLine("List:");
            Console.WriteLine("  list bookings");
            Console.WriteLine("  list ports");
        }

        // Print Display Command - Prints command to display time table
        private static void PrintDisplayCommand()
        {
            Console.WriteLine("display timetable");
        }

        // Print Clear Command - Prints command to clear the console
        private static void PrintClearCommand()
        {
            Console.WriteLine("clear");
        }

        // Print Exit Command - Prints command to close the console
        private static void PrintExitCommand()
        {
            Console.WriteLine("exit");
        }

        // Print All Commands - Prints all commands
        private static void PrintAllCommands()
        {
            Console.WriteLine("Commands are:");
            Console.WriteLine("--------------");
            PrintBookingCommand();
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