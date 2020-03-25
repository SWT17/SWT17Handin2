using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace Ladeskab
{
   public class ChargeControl
   {
        private Display _display;
        private IUsbCharger _usbCharger;
       

        public ChargeControl(IUsbCharger USBCharger, Display display)
        {
            USBCharger.CurrentValueEvent += HandleNewCurrentValue;
            _display = display;
            _usbCharger = USBCharger;
        }

        private void HandleNewCurrentValue(object sender, CurrentEventArgs e)
        {
            

                if (e.Current == 0)
                {
                    _display.Display2Message("");
                }
                else if (e.Current > 0 && e.Current <= 5)
                {
                    _display.Display2Message("Telefonen er fuldt opladet");

                }
                else if (e.Current > 5 && e.Current <= 500)
                {
                    _display.Display2Message("Ladning er igang");

                }
                else if (e.Current > 500)
                {
                    _display.Display2Message("Fejlmeddelse - frakoble straks telefonen");

                }
            
        }

        public bool IsConnected()
        {
            bool connection = _usbCharger.Connected;
            return connection;
        }
        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }
        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }

        public void StimulateOverload()
        {
        }


    }
}
