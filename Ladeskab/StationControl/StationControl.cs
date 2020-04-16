using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class StationControl:IStationControl
    {
        private IDoor _door;
        private IRFIDReader _rfidReader;
        private IDisplay _display;
        private ILogfile _logfile;
        private IChargeControl _chargeControl;
        public StationControl(IRFIDReader RFIDReader, IDoor Door, IDisplay display, ILogfile logfile, IChargeControl chargeControl)
        {
            RFIDReader.RFIDDetectedEvent += HandleNewRFID;
            Door.DoorOpenEvent += HandleNewDoorOpen;
            Door.DoorClosedEvent += HandleNewDoorClosed;
            _door = Door;
            _rfidReader = RFIDReader;
            _display = display;
            _logfile = logfile;
            _chargeControl = chargeControl;
        }

        private void HandleNewRFID(object sender, RFIDDetectedEventArgs e)
        {
            RfidDetected(e.Id);
        }

        private void HandleNewDoorOpen(object sender, DoorOpenEventArgs e)
        {
            DoorOpened();
        }

        private void HandleNewDoorClosed(object sender, DoorClosedEventArgs e)
        {
            DoorClosed();
        }

        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        public LadeskabState _state;
        private int _oldId;
        
        public void RfidDetected(int id) 
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse

                    if (_chargeControl.IsConnected())
                    {
                        _door.LockDoor();
                        _oldId = id;
                       
                        _logfile.LogDoorLocked(id);

                        _display.Display1Message("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;


                        _chargeControl.StartCharge();

                    }
                    else
                    {
                        _display.Display1Message("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _chargeControl.StopCharge();
                        _door.UnlockDoor();
                       

                        _logfile.LogDoorUnlocked(id);

                        _display.Display1Message("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.Display1Message("Forkert RFID tag");
                    }

                    break;
            }
        }

      

        public void DoorOpened()
        {
            _display.Display1Message("Tilslut telefon");
        }

        public void DoorClosed()
        {
            _display.Display1Message("Indlæs RFID");
        }

    }
}
