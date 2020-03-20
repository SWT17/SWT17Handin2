using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Ladeskab;

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

        [SetUp]

        public void SetUp()
        {
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRFIDReader>();
            _display = new Display();
            _logfile = new Logfile();
            _usbCharger = Substitute.For<IUsbCharger>();
            _stationControl = new StationControl(_rfidReader,_door, _display, _logfile, _usbCharger);
        }

    }
}
