using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
   public class RFIDreader : IRFIDReader
    {

        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;
        private 
        public int RFIDTagId { get; set; }

        //private StationControl _stationControl;
        //public RFIDreader()
        //{
        //    _stationControl = new StationControl();
        //}

        public void RFIDTagPresented()
        {
            RFIDDetectedEvent?.Invoke(this, new RFIDDetectedEventArgs(){Id = RFIDTagId});
        }
    }
}
