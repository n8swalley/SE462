using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FerryLegacy;
using NUnit.Framework;

namespace FerryLegacy.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        // automatic unit test to check system output 
            FileStream testOutStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\data\\currentOut.txt", FileMode.OpenOrCreate, FileAccess.Write);
            TextWriter consoleOutputWriter = new StreamWriter(testOutStream);

            Console.SetOut(consoleOutputWriter);

        // test program here
            Program.Main();

            consoleOutputWriter.Close();
            testOutStream.Close();

            StreamReader expectedOutputStream = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\data\\currentSystemOutput.txt");
            StreamReader currentOutputStream = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\data\\currentOut.txt");

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedOutputStream.ReadToEnd(), currentOutputStream.ReadToEnd());

            expectedOutputStream.Close();
            currentOutputStream.Close();
        }
    }
}