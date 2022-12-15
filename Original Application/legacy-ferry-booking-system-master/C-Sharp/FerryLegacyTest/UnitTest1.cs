using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FerryLegacy;

namespace FerryLegacyTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            FileStream testOutStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\data\\currentOut.txt", FileMode.OpenOrCreate, FileAccess.Write);
            TextWriter consoleOutputWriter = new StreamWriter(testOutStream);
           
            Console.SetOut(consoleOutputWriter);
            Program.MainWithTestData();

            consoleOutputWriter.Close();
            testOutStream.Close();

            StreamReader expectedOutputStream = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\data\\currentSystemOutput.txt");
            StreamReader currentOutputStream = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\data\\currentOut.txt");

            Assert.AreEqual(expectedOutputStream.ReadToEnd(), currentOutputStream.ReadToEnd());

            expectedOutputStream.Close();
            currentOutputStream.Close();
        }
    }
}
