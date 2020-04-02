using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;

namespace Ladeskab
{
    public interface IStationControl
    {
        
                 
        void RfidDetected(int id); //ændret til public!!! :D    Skal den være det?? bliver den ikke kun kaldt i denne klasse??

        void DoorOpened();

        void DoorClosed();
        
    }
}
