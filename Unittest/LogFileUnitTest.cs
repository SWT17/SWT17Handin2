using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ladeskab;

namespace Unittest
{
    [TestFixture]

    public class LogFileUnitTest
    {
        [Test]

        public void LogDoorUnlocked_SaveID111_fileHasSavedID111()
        {
            //Clear all text in the log//
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", String.Empty);

            //Opret uut og id
            var uut = new Logfile();
            var id = "111";
            //Gem i filen med 
            uut.LogDoorUnlocked(111);

            var fileText = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt");

            StringAssert.Contains(id, fileText);

            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", String.Empty);



        }

    }
}
