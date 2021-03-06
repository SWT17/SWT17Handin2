﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public interface IDoor
    {
        event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
        event EventHandler<DoorClosedEventArgs> DoorClosedEvent;

        void OnUserOpensDoor();
        void OnUserClosesDoor();

        void LockDoor();
        void UnlockDoor();
    }
}
