using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Ladeskab;
using UsbSimulator;

namespace App
{
    public class Program
    {
        static void Main(string[] args)
        {
            IDoor _door = new Door();
            IRFIDReader _rfidReader = new RFIDReader();
            Display _display = new Display();
            Logfile _logfile = new Logfile();
            IUsbCharger _usbCharger = new UsbChargerSimulator();
            ChargeControl _chargeControl = new ChargeControl(_usbCharger, _display);

            StationControl _stationControl = new StationControl(_rfidReader, _door, _display, _logfile,_chargeControl);


            //        _door.OnUserOpensDoor();

            _door.OnUserOpensDoor();
            Thread.Sleep(2000);
            _door.OnUserClosesDoor();
            Thread.Sleep(2000);
            _rfidReader.OnRFIDTagPresented(123);
            Thread.Sleep(1000);
            Console.WriteLine("...");
            Thread.Sleep(1000);
            Console.WriteLine("...");
            Thread.Sleep(1000);
            Console.WriteLine("...");
            Thread.Sleep(2000);
            _rfidReader.OnRFIDTagPresented(123);
            Console.ReadKey();
        }
    }
}
