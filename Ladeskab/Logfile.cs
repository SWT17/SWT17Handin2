using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Logfile
    {
        public void LogDoorLocked(int Id)
        {
            File.AppendAllText("..\\Log.txt", "Locked:    ID; " + slope + "; Date;" + DateTime.Now + "\n");
        }

        public void LogDoorUnlocked(int Id)
        {
     //       File.AppendAllText("..\\Ladeskab\\Log.txt", "Unlocked:    ID; " + slope + "; Date;" + DateTime.Now + "\n");
        }
    }
}
