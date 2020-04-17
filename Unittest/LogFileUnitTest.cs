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
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", String.Empty);

            var uut = new Logfile();
            var id = "222";

            uut.LogDoorUnlocked(222);

            var fileText = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt");

            StringAssert.Contains(id, fileText);




        }

    }
}
