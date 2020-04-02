using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Ladeskab;

namespace Unittest
{
    [TestFixture]
    public class UpdateCurrentCalueUnitTest
    {
        private IUsbCharger _chargerUsb;
        private IDisplay _display;
      
        [TestCase("",0)]
        [TestCase("Telefonen er fuldt opladet",1.0)]
        [TestCase("Telefonen er fuldt opladet", 5.0)]
        [TestCase("Ladning er igang", 6.0)]
        [TestCase("Ladning er igang", 500.0)]
        [TestCase("Fejlmeddelse - frakoble straks telefonen", 510.0)]

        public void UpdateCurrentCalue_Values_chargeMessage(string message, double CurrentValue)
        {
            _chargerUsb = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();

            ChargeControl uut = new ChargeControl(_chargerUsb, _display );

            _chargerUsb.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = CurrentValue });

            StringAssert.Contains(message, uut._chargeMessage);
            

        }

        [Test]
        public void UpdateCurrentValue_Overload_StopChargeCalled()
        {
            int overloadCurrent = 750;

            _chargerUsb = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();

            ChargeControl uut = new ChargeControl(_chargerUsb, _display);

            uut.UpdateCurrentCalue(overloadCurrent);

            _chargerUsb.Received().StopCharge();

  




        }
    }
}
