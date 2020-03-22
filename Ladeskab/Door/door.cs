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
        private DoorState _doorState = DoorState.Unlocked; // Jeg satte den til at være Unlocked som default...

        private enum DoorState
        {
            Locked,
            Unlocked
        }

        public void OnUserOpensDoor()
        {
            if (_doorState == DoorState.Unlocked)
            {
                Console.WriteLine("[Door opens]");
                DoorOpenEvent?.Invoke(this, new DoorOpenEventArgs());
            }
        }

        public void OnUserClosesDoor()
        {
            Console.WriteLine("[Door closes]");
            DoorClosedEvent?.Invoke(this, new DoorClosedEventArgs());
        }

        public void LockDoor()
        {
            Console.WriteLine("[Door is locked]");
            _doorState = DoorState.Locked;
        }

        public void UnlockDoor()
        {
            Console.WriteLine("[Door is unlocked]");
            _doorState = DoorState.Unlocked;
        }
    }
}
