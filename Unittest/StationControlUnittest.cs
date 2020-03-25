using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ladeskab;
using NSubstitute;

namespace Unittest
{   
    [TestFixture]
    public class StationControlUnitTest
    {
        private IDoor _door;
        private IRFIDReader _rfidReader;
        private Display _display;
        private Logfile _logfile;
        private StationControl _stationControl;
        private IUsbCharger _usbCharger;
        private ChargeControl _chargeControl;

        [SetUp]
        public void Setup()
        {
            _rfidReader = new RFIDReader();
            _door = new Door();
            _logfile = new Logfile();
            _display = new Display();
            _usbCharger = new UsbSimulator.UsbChargerSimulator();
        }
       
       [Test]
       public void RfidDetected_chargingNotConnected_()
        {

            _usbCharger = Substitute.For<IUsbCharger>();
            _door = Substitute.For<IDoor>();
            _chargeControl = new ChargeControl(_usbCharger, _display);
         

            _usbCharger.Connected.Returns(false);

            var uut = new StationControl(_rfidReader,_door,_display,_logfile,_chargeControl);
            
            uut.RfidDetected(100);

            _door.DidNotReceive().LockDoor();
                   
        }


    }
}
