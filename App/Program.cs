using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;

namespace App
{
    public class Program
    {
        static void Main(string[] args)
        {
            IRFIDReader _rfidReader = new RFIDreader();
            IDoor _door = new Door();
            StationControl _stationControl = new StationControl(_rfidReader,_door);

            _door.UserOpensDoor();
        }
    }
}
