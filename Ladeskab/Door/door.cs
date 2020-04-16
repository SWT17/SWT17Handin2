using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Door : IDoor
    {
       

        public event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
        public event EventHandler<DoorClosedEventArgs> DoorClosedEvent;


        public void OnUserOpensDoor()
        {
            Console.WriteLine("[Door opens]");
            DoorOpenEvent?.Invoke(this, new DoorOpenEventArgs());
        }

        public void OnUserClosesDoor()
        {
            Console.WriteLine("[Door closes]");
            DoorClosedEvent?.Invoke(this, new DoorClosedEventArgs());
        }

        public void LockDoor()
        {
            Console.WriteLine("[Door is locked]");
        }

        public void UnlockDoor()
        {
            Console.WriteLine("[Door is unlocked]");
        }
    }
}
