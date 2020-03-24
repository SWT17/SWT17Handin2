using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
   public class ChargeControl
   {
       private Display _display;
        public ChargeControl(IUsbCharger USBCharger, Display display)
        {
            USBCharger.CurrentValueEvent += HandleNewCurrentValue;
            _display = display;
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
