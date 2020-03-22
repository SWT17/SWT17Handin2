using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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

            StationControl _stationControl = new StationControl(_rfidReader, _door, _display, _logfile, _usbCharger);

            ChargeControl _chargeControl = new ChargeControl(_usbCharger, _display);

            //        _door.OnUserOpensDoor();
            _door.OnUserOpensDoor();
            System.Threading.Thread.Sleep(2000);
            _door.OnUserClosesDoor();
            System.Threading.Thread.Sleep(2000);
            _rfidReader.OnRFIDTagPresented(123);
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("...");
            System.Threading.Thread.Sleep(2000);
            _rfidReader.OnRFIDTagPresented(123);
            Console.ReadKey();
        }
    }
}
