using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
   public class ChargeControl
    {
        public ChargeControl(IUsbCharger USBCharger)
        {
            USBCharger.CurrentValueEvent += HandleNewCurrentValue;
        }

        private void HandleNewCurrentValue(object sender, CurrentEventArgs e)
        {

        }
        //public bool IsConnected()
        //{

        //}
        public void StartCharge()
        {

        }
        public void StopCharge()
        {

        }
    }
}
