﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Logfile:ILogfile
    {
        public void LogDoorLocked(int Id)
        {
            File.AppendAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", "Locked:\t\tID: " + Id + "; \tDate: " + DateTime.Now + "\n");

          
        }

        public void LogDoorUnlocked(int Id)
        {

            File.AppendAllText(System.AppDomain.CurrentDomain.BaseDirectory+"\\Log.txt", "Unlocked:\tID: " + Id + ";\tDate: " + DateTime.Now + "\n");
        }
    }
}
