

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FerryLegacy.Tests
{
    public class GoldenMaster
    {
        private static void GenerateFile(string filename)
        {
            var fs = new FileStream(filename, FileMode.Create);
            var sw = new StreamWriter(fs);
            Console.SetOut(sw);

            Program.MainWithTestData();

            sw.Close();
        }

        [Test]
        public void save_test_output()
        {
            GenerateFile(@"C:\code\legacy-ferry-booking-system\TestResult.txt");

            var goldenmaster = File.ReadAllLines(@"C:\code\legacy-ferry-booking-system\GoldenMaster.txt");
            var testresult = File.ReadAllLines(@"C:\code\legacy-ferry-booking-system\TestResult.txt");

            NUnit.Framework.Assert.That(goldenmaster, Is.EqualTo(testresult));
        }
    }
}




//namespace FerryLegacy.Tests
//{
//    public class UnitTest1
//    {
//        [Fact]
//        public void testCommands()
//        {
//            DoCommand("help");
//            DoCommand("list ports");
//            DoCommand("search 2 3 00:00");
//            DoCommand("search 2 3 00:00");
//            DoCommand("book 10 2");
//            DoCommand("search 2 3 00:00");
//            DoCommand("book 10 10");
//            DoCommand("book 10 1");
//            DoCommand("search 1 2 01:00");
//            DoCommand("book 4 2");
//            DoCommand("book 6 8");
//            DoCommand("search 1 2 01:00");
//            DoCommand("search 1 3 01:00");
//            DoCommand("search 1 3 01:30");
//            DoCommand("book 5 16");
//            DoCommand("book 16 16");
//            DoCommand("search 1 3 00:00");
//            DoCommand("list bookings");

//        }

//        private static void DoCommand(string command)
//        {
//            if (command.StartsWith("search"))
//                Search(command);
//            else if (command.StartsWith("book"))
//                Book(command);
//            else if (command.StartsWith("list ports"))
//            {
//                Console.WriteLine("Ports:");
//                Console.WriteLine("------");
//                foreach (var port in _ports.All())
//                {
//                    Console.WriteLine("{0} - {1}", port.Id, port.Name);
//                }
//                Console.WriteLine();
//            }
//            else if (command.StartsWith("list bookings"))
//            {
//                var bookings = _bookingService.GetAllBookings();
//                Console.WriteLine("Bookings:");
//                Console.WriteLine("---------");
//                foreach (var b in bookings)
//                {
//                    Console.WriteLine("journey {0} - passengers {1}", b.JourneyId, b.Passengers);
//                }
//                Console.WriteLine();
//            }
//            else
//            {
//                Console.WriteLine("Commands are: [search x y hh:mm] book, or list bookings");
//                Console.WriteLine("  search x y hh:mm");
//                Console.WriteLine("  book x y");
//                Console.WriteLine("  list bookings");
//                Console.WriteLine("  list ports");
//                Console.WriteLine();
//                Console.WriteLine("Book is [book x y]");
//                Console.WriteLine("where x - journey id");
//                Console.WriteLine("where y - number of passenger");
//                Console.WriteLine();
//                Console.WriteLine("Search is [search x y hh:mm]");
//                Console.WriteLine("where: x - origin port id");
//                Console.WriteLine("where: y - destinationg port id");
//                Console.WriteLine("where: hh:mm - time to search after");
//            }
//        }




//    }
//}