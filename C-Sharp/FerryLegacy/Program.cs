using System;
using System.Collections.Generic;
using System.Linq;

namespace FerryLegacy
{
    // Edits:
    // - moved all commands to command class
    // - add instance of commands class
    // - use commands class to call user commands
    // add instance of system manager

    public class Program
    {

        public static void Main()
        {
            SystemManager SystemManager = new SystemManager();
            runCommands();
        }

        // Reads in users commands from the console
        private static void runCommands()
        {
            string line = "start";
            while (line != "quit")
            {
                Commands.Command(line);
                line = (Console.ReadLine() ?? "").ToLower();
            }
        }
    }
}