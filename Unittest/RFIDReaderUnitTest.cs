using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;

namespace Unittest
{
    public class RFIDReaderUnitTest
    {
        private IDoor _door;
        private IRFIDReader uut;
        private StationControl _stationControl;
        private ChargeControl _chargeControl;
        private IUsbCharger _usbCharger;

        public event EventHandler _eventInvoker;

        [SetUp]

        public void SetUp()
        {
            _usbCharger = new UsbChargerSimulator();
            _chargeControl = new ChargeControl(_usbCharger, new Display());
            uut = new RFIDReader();
            _door = Substitute.For<Door>();
            _stationControl = Substitute.For<StationControl>(uut, _door, new Display(), new Logfile(), _chargeControl );
        }


        #region Test af om event raises som forventet

        [Test]

        public void EventCalled_RFIDPresented()
        {
            var rfidEventRaised = 0;
           
           
           uut.RFIDDetectedEvent += delegate (object sender, RFIDDetectedEventArgs a) { rfidEventRaised = 1; };

            //uut.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs(){Id = 0000});
            uut.OnRFIDTagPresented(0000);

            //_stationControl.Received(1).RfidDetected(0000);

            Assert.That(rfidEventRaised, Is.EqualTo(1));
        }


        [Test]

        public void EventCalled_RFIDPresented_3Times() // testen består også hvor der testes med gentagne kald, uanset om DoorEvents bliver raised
        {
            var rfidEventRaised = 0;

            uut.RFIDDetectedEvent += delegate (object sender, RFIDDetectedEventArgs e) { rfidEventRaised += 1; };

            uut.OnRFIDTagPresented(0000);
            _door.OnUserOpensDoor();
            uut.OnRFIDTagPresented(0001);
            _door.OnUserClosesDoor();
            uut.OnRFIDTagPresented(0002);

            Assert.That(rfidEventRaised, Is.EqualTo(3));
        }


        #endregion

    }
}
