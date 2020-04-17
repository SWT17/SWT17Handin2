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
        private IDisplay _display;
        private ILogfile _logfile;
        private IUsbCharger _usbCharger;
        private IChargeControl _chargeControl;
        private StationControl uut;

        [SetUp]
        public void Setup()
        {
          

            _rfidReader = Substitute.For<IRFIDReader>();
            _door = Substitute.For<IDoor>();
            _logfile = Substitute.For<ILogfile>();
            _display = Substitute.For<IDisplay>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _chargeControl = Substitute.For<IChargeControl>();

            uut = new StationControl(_rfidReader, _door, _display, _logfile, _chargeControl);
        }
       
       [Test]
        public void RfidDetected_chargingNotConnectedÉlse_DoorNotLocked()
        {
            //Arrange
            _usbCharger = Substitute.For<IUsbCharger>();
            _door = Substitute.For<IDoor>();
            _chargeControl = new ChargeControl(_usbCharger, _display);
         

            _usbCharger.Connected.Returns(false);

            
           
            
            
            uut.RfidDetected(100);

            _door.DidNotReceive().LockDoor();


                         
        }

        [Test]
        public void RfidDetected_LadeskabeStateLockedAndIdEqualOldId_LadeskabeIsAvailable()
        {
            // Arrange
            _usbCharger = Substitute.For<IUsbCharger>();
            _logfile = Substitute.For<Logfile>();
            _chargeControl = new ChargeControl(_usbCharger, _display);
            int id = 100;
            
            StationControl uut = new StationControl(_rfidReader, _door, _display, _logfile, _chargeControl);

            //act
            uut._state = Ladeskab.StationControl.LadeskabState.Available;

            _usbCharger.Connected.Returns(true);

            uut.RfidDetected(id); //Sætter oldId = id

            uut.RfidDetected(id); 

            //Assert
            Assert.That(uut._state, Is.EqualTo(Ladeskab.StationControl.LadeskabState.Available));

        }

        [Test]

        public void RfidDetected_LadeskabeStateisDoorOpen_LadeskabeIsDoorOpen()
        {
            // Arrange
            _usbCharger = Substitute.For<IUsbCharger>();
            _logfile = Substitute.For<Logfile>();
            _chargeControl = new ChargeControl(_usbCharger, _display);
            int id = 100;

            StationControl uut = new StationControl(_rfidReader, _door, _display, _logfile, _chargeControl);

            //act
            uut._state = Ladeskab.StationControl.LadeskabState.DoorOpen;

            _usbCharger.Connected.Returns(true);


            uut.RfidDetected(id);

            //Assert
            Assert.That(uut._state, Is.EqualTo(Ladeskab.StationControl.LadeskabState.DoorOpen));
        }

       
        [Test]

        public void DoorOpened_DoorIsOpened_DisplayRecievesMessage()
        {
            uut.DoorOpened();

            _display.Received().Display1Message("Tilslut telefon");

        }

        [Test]

        public void DoorClosed_DoorIsClosed_DisplayRecievesMessage()
        {
            uut.DoorClosed();

            _display.Received().Display1Message("Indlæs RFID");
        }

        [Test]

        public void RfidDetected_PhoneChargingReopenDoor_wrongID()
        {
            _chargeControl.IsConnected().Returns(true);
            uut.RfidDetected(111);
            
            uut.RfidDetected(122);

            _display.Received().Display1Message("Forkert RFID tag");


            
        }




    }
}
