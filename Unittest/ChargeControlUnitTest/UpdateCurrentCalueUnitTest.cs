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

      
        [TestCase("",0)]
        [TestCase("Telefonen er fuldt opladet",1.0)]
        [TestCase("Telefonen er fuldt opladet", 5.0)]
        [TestCase("Ladning er igang", 6.0)]
        [TestCase("Ladning er igang", 500.0)]
        [TestCase("Fejlmeddelse - frakoble straks telefonen", 510.0)]

        public void UpdateCurrentCalue_Values_chargeMessage(string message, double CurrentValue)
        {

            IUsbCharger _chargerUsb = Substitute.For<IUsbCharger>();
            ChargeControl uut = new ChargeControl(_chargerUsb, new Display());

            uut.UpdateCurrentCalue(CurrentValue);

            StringAssert.Contains(message, uut._chargeMessage);
            

        }

    }
}
