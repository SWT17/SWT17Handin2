using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using NSubstitute;
using NUnit.Framework;
using Ladeskab;

namespace Unittest
{
    public class DoorUnitTest
    {
        private IDoor uut;
        protected IRFIDReader _rfidReader;
        private StationControl _stationControl;

        [SetUp]
        public void SetUp()
        {
            _rfidReader = Substitute.For<IRFIDReader>(); // fake lavet for at StationControl's constructor ikke er tom
            uut = new Door();
            _stationControl = Substitute.For<StationControl>(_rfidReader, uut);

        }

        /// <summary>
        /// Der testes overordnet for at sikre sig at når døren åbnes eller lukkes så bliver de relevante metoder kaldt i StationControl.
        /// De "modtagende" metoder i StationControl kan vi ikke teste på da de er private. Da der heller ikke sættes nogen variable fra disse,
        /// skal der testes imod om metoderne bliver kaldt. Man kan teste ved at kalde metoderne UserOpensDoor og UserClosesDoor eller ved
        /// at raise eventet med den variabel som vi ønsker (Open, boolean). Ved at teste med metoderne ved vi at de rigtige events bliver kaldt.
        /// Dette bliver også separat testet så koden er gennemtestet.
        /// </summary>
        ///

        [Test]

        public void EventCalled_DoorIsOpen()
        {
            var fisk = 0;
            uut.DoorOpenEvent += delegate(object sender, DoorOpenEventArgs e) { fisk = 1; };
            uut.UserOpensDoor();
            Assert.That(fisk==1);
        }


        [Test]
        public void DoorIsOpened_Ctrl_Method_DoorOpened() // her testes at der kaldes metoden DoorOpened som resultat af UserOpensDoor
        {
            uut.UserClosesDoor();
           // uut.DoorOpenEvent += Raise.EventWith(new DoorOpenEventArgs() { Open = false });
            // man kan teste ved at raise eventet på den her måde, men på den anden måde tester den specifikt om metoden resulterer i det den skal
            _stationControl.Received(1).DoorOpened();
        }


        [Test]
        public void DoorIsOpened_Ctrl_Method_DoorClosed_Not_Called() // her testes at der IKKE kaldes metoden DoorClosed som resultat af UserOpensDoor
        {
            uut.UserOpensDoor();
            _stationControl.DidNotReceive().DoorClosed();
        }


        [Test]
        public void EventCalled_DoorIsClosed()
        {
            uut.DoorOpenEvent += delegate (object sender, DoorOpenEventArgs e)
            {
                Assert.That(e.Open = false);
            };
            uut.UserClosesDoor();
        }


        [Test]
        public void DoorIsClosed_Ctrl_Method_DoorClosed() // her testes at der kaldes metoden DoorClosed som resultat af UserClosesDoor
        {
            uut.UserClosesDoor();
            _stationControl.Received(1).DoorClosed();
        }

        [Test]
        public void DoorIsClosed_Ctrl_Method_DoorOpened_Not_Called() // her testes at der IKKE kaldes metoden DoorOpened som resultat af UserClosesDoor
        {
            uut.UserClosesDoor();
            _stationControl.DidNotReceive().DoorOpened();
        }

        [Test]

        public void Liste()
        {
            List<EventArgs> receivedEvents = new List<EventArgs>();
            uut.DoorOpenEvent += delegate (object sender, DoorOpenEventArgs e) {
                receivedEvents.Add(e);
            };

            uut.UserOpensDoor();
            uut.UserClosesDoor();
            uut.UserOpensDoor();
            uut.UserOpensDoor();

            Assert.That(receivedEvents.Count(), Is.EqualTo(4));
        }
    }
}
