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
            File.AppendAllText("..\\..\\..\\Ladeskab\\Log.txt", "Locked:\t\tID: " + Id + "; \tDate: " + DateTime.Now + "\n");

            //Kode der gør man kan læse fra filen
            /*
            string logData;
            string inputRecord;
            string[] inputFields;

            while ((inputRecord = fileReader.ReadLine()) != null)
            {
                inputFields = inputRecord.Split(';');
                logData = inputFields[1];
            }
            fileReader.Close();
            */
        }

        public void LogDoorUnlocked(int Id)
        {
            File.AppendAllText("..\\..\\..\\Ladeskab\\Log.txt", "Unlocked:\tID: " + Id + ";\tDate: " + DateTime.Now + "\n");
        }
    }
}
