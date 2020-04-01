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
        public StationControl(IRFIDReader RFIDReader, IDoor Door, Display display, Logfile logfile, ChargeControl chargeControl)
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
        //private int _id;

        // Vi har lavet en Logfile klasse, derfor er næste linje ikke med mere
        //private string logFile = "logfile.txt"; // Navnet på systemets log-fil


        // Her mangler constructor

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        public void RfidDetected(int id) //ændret til public!!! :D    Skal den være det?? bliver den ikke kun kaldt i denne klasse??
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse

                    if (_chargeControl.IsConnected())
                    {
                        _door.LockDoor();
                        _oldId = id;
                        //using (var writer = File.AppendText(logFile))
                        //{
                        //    writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        //}
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
