using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Door : IDoor
    {
        private StationControl _stationControl;
        public event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
        public Door()
        {
            _stationControl = new StationControl();
            DoorOpenEvent.Invoke(this, new DoorOpenEventArgs(){Open = false});
        }
        
        
        public void UserOpensDoor()
        {
            DoorOpenEvent.Invoke(this, new DoorOpenEventArgs() { Open = true });
            _stationControl.DoorOpened();
        }

        public void UserClosesDoor()
        {
            DoorOpenEvent.Invoke(this, new DoorOpenEventArgs() { Open = false });
            _stationControl.DoorClosed();
        }
    }
}
