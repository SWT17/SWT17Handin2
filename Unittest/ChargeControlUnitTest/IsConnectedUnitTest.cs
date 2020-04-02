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
    public class IsConnectedUnitTest
    {
        IUsbCharger _chargerUsb;
        IChargeControl uut;
        IDisplay _display;
        [SetUp]
        public void SetUp()
        {
            _chargerUsb = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            uut = new ChargeControl(_chargerUsb,_display);
        }

        [TestCase(false,false)]
        [TestCase(true,true)]
        public void IsConnected_methodCalled_ReturnsResult(bool connection, bool result)
        {

            _chargerUsb.Connected.Returns(connection);

            Assert.AreEqual(uut.IsConnected(), result);
        }

       




    }
}
