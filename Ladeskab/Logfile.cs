using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Logfile
    {


        public void LogDoorLocked(int Id)
        {
            //using (var writer = File.AppendText(Log))
            //using (var writer = File.AppendText(logFile))
            //{
            //    writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
            //}
            File.AppendAllText("..\\Log.txt", "Locked:    ID; " + Id + "; Date;" + DateTime.Now + "\n");
        }

        public void LogDoorUnlocked(int Id)
        {
     //       File.AppendAllText("..\\Ladeskab\\Log.txt", "Unlocked:    ID; " + slope + "; Date;" + DateTime.Now + "\n");
        }
    }
}
