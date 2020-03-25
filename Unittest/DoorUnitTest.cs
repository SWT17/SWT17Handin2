using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using NSubstitute;
using NUnit.Framework;
using Ladeskab;
using NSubstitute.ReceivedExtensions;

namespace Unittest
{
    public class DoorUnitTest
    {
        private IDoor uut;
        private IRFIDReader _rfidReader;
        private StationControl _stationControl;
        private IUsbCharger _usbCharger;
        private ChargeControl _chargeControl;
        private Display _display;

        [SetUp]
        public void SetUp()
        {
            _rfidReader = Substitute.For<IRFIDReader>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<Display>();
            _chargeControl = Substitute.For<ChargeControl>(_usbCharger,_display);


            uut = new Door();
            _stationControl = Substitute.For<StationControl>(_rfidReader, uut, new Display(), new Logfile(), _chargeControl);
        }

        /// <summary>
        /// Der testes overordnet for at sikre sig at når døren åbnes eller lukkes så bliver de relevante events raised.
        /// De "modtagende" metoder i StationControl kan vi ikke teste på da de er private.
        /// Man kan teste ved at kalde metoderne OnUserOpensDoor og OnUserClosesDoor eller ved
        /// at raise eventet.Her testes ved at kalde metoderne og så delegate en aktivitet til eventet, for eksempel at sætte
        /// en value eller tilføje til en liste. Der assertes så på at dette sker som forventet.
        /// </summary>
        ///

        #region Test af om events raises som forventet

        [Test]

        public void EventCalled_DoorIsOpen()
        {
            var doorEventRaised = 0;

            uut.DoorOpenEvent += delegate (object sender, DoorOpenEventArgs e) { doorEventRaised = 1; };

            uut.OnUserOpensDoor();

            Assert.That(doorEventRaised, Is.EqualTo(1));
        }

        [Test]

        public void EventCalled_DoorIsOpen_3Times()
        {
            List<EventArgs> receivedEvents = new List<EventArgs>(); // Liste af EventArgs så den kan tage DoorClosedEventArgs i tilfælde af at de kastes

            uut.DoorOpenEvent += delegate (object sender, DoorOpenEventArgs e)
            {
                receivedEvents.Add(e);
            };

            uut.OnUserOpensDoor();
            uut.OnUserClosesDoor();
            uut.OnUserOpensDoor();
            uut.OnUserOpensDoor();

            Assert.That(receivedEvents.Count(), Is.EqualTo(3));
        }


        [Test]
        public void EventCalled_DoorIsClosed()
        {
            var doorEventRaised = 0;

            uut.DoorClosedEvent += delegate (object sender, DoorClosedEventArgs e) { doorEventRaised = 1; };

            uut.OnUserClosesDoor();

            Assert.That(doorEventRaised, Is.EqualTo(1));
        }


        [Test]

        public void EventCalled_DoorIsClosed_Twice()
        {
            List<EventArgs> receivedEvents = new List<EventArgs>();

            uut.DoorOpenEvent += delegate (object sender, DoorOpenEventArgs e)
            {
                receivedEvents.Add(e);
            };

            uut.OnUserOpensDoor();
            uut.OnUserClosesDoor();
            uut.OnUserOpensDoor();
            uut.OnUserClosesDoor();

            Assert.That(receivedEvents.Count(), Is.EqualTo(2));
        }

        #endregion
    }
}
