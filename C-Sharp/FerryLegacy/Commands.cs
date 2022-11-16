using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerryLegacy
{
    class Commands
    {
        // stores user command
        private static string userCmd = " ";

        // do commands here

        public static void Command(string command) 
        {
            userCmd = command;

            if (userCmd != null)
            {
                runCmd();
            }
            
        }

        private static void runCmd()
        {
            // call corresponding private method for each command
        }

        private static void startCmd()
        {
            // start command
        }

        private static void printAllCmds()
        {
           // print all commands
        }

        private static void exitCmd()
        {

        }
        





    }
}
