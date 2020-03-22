using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Door : IDoor
    {
        // her anvendes to events da de hver især repræsenterer forskellige states
        // events indeholder ingen yderligere information

        public event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
        public event EventHandler<DoorClosedEventArgs> DoorClosedEvent;
        private DoorState _doorState;


        private enum DoorState
        {
            Locked,
            Unlocked
        }


        public void OnUserOpensDoor()
        {
            if (_doorState == DoorState.Unlocked)
            {
                DoorOpenEvent?.Invoke(this, new DoorOpenEventArgs());
            }
        }

        public void OnUserClosesDoor()
        {
            DoorClosedEvent?.Invoke(this, new DoorClosedEventArgs());
        }

        public void LockDoor()
        {
            _doorState = DoorState.Locked;
        }

        public void UnlockDoor()
        {
            _doorState = DoorState.Unlocked;
        }

    }
}
