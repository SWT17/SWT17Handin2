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
        public string _chargeMessage;
       

        public ChargeControl(IUsbCharger USBCharger, Display display)
        {
            USBCharger.CurrentValueEvent += HandleNewCurrentValue;
            _display = display;
            _usbCharger = USBCharger;
        }

        private void HandleNewCurrentValue(object sender, CurrentEventArgs e)
        {
            UpdateCurrentCalue(e.Current);
           
        }

        public void UpdateCurrentCalue(double Current)
        {

            if (Current == 0)
            {
                _chargeMessage = "";
                _display.Display2Message(_chargeMessage);
            }
            else if (Current > 0 && Current <= 5)
            {
                _chargeMessage = "Telefonen er fuldt opladet";
                _display.Display2Message(_chargeMessage);

            }
            else if (Current > 5 && Current <= 500)
            {
                _chargeMessage = "Ladning er igang";
                _display.Display2Message(_chargeMessage);

            }
            else if (Current > 500)
            {
                _chargeMessage = "Fejlmeddelse - frakoble straks telefonen";
                _display.Display2Message(_chargeMessage);

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

        //public void StimulateOverload()
        //{
        //}


    }
}
