using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Ladeskab;

namespace Unittest.ChargeControlUnitTest
{
    [TestFixture]
   public class StopAndStartChargeUnitTest
    {
       [Test]
       public void stopCharge_methodIsCalled_UsbSimulatorStopChargeIsCalled()
        {
            IUsbCharger _usbCharger = Substitute.For<IUsbCharger>();

            var uut = new ChargeControl(_usbCharger, new Display());

            uut.StopCharge();

            _usbCharger.Received().StopCharge();



        }

        [Test]
        public void startCharge_methodIsCalled_UsbSimulatorStartChargeIsCalled()
        {
            IUsbCharger _usbCharger = Substitute.For<IUsbCharger>();

            var uut = new ChargeControl(_usbCharger, new Display());

            uut.StartCharge();

            _usbCharger.Received().StartCharge();



        }
    }
}
