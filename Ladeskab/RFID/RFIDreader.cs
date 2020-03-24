using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
   public class RFIDReader : IRFIDReader
    {

        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;

        //private StationControl _stationControl;
        //public RFIDReader()
        //{
        //    _stationControl = new StationControl();
        //}

        //public RFIDReader(StationControl _stationControl)
        //{

        //}

        public void OnRFIDTagPresented(int id)
        {
            RFIDDetectedEvent?.Invoke(this, new RFIDDetectedEventArgs(){Id = id});
        }
    }
}
