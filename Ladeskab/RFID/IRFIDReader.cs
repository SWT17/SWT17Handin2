﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
   public interface IRFIDReader
    {
        event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;

        void OnRFIDTagPresented(int id);
    }
}
