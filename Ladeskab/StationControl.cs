using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class StationControl
    {
        private IDoor _door;
        private IRFIDReader _rfidReader;
        private Display _display;
        private Logfile _logfile;
        private IUsbCharger _usbCharger;
        public StationControl(IRFIDReader RFIDReader, IDoor Door, Display display, Logfile logfile, IUsbCharger usbCharger)
        {
            RFIDReader.RFIDDetectedEvent += HandleNewRFID;
            Door.DoorOpenEvent += HandleNewDoorOpen;
            Door.DoorClosedEvent += HandleNewDoorClosed;
            _door = Door;
            _rfidReader = RFIDReader;
            _display = display;
            _logfile = logfile;
            _usbCharger = usbCharger;
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
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private int _oldId;
        private int _id;

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
                    if (_usbCharger.Connected)
                    {
                        _door.LockDoor();
                        _usbCharger.StartCharge();
                        _oldId = id;
                        //using (var writer = File.AppendText(logFile))
                        //{
                        //    writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        //}
                        _logfile.LogDoorLocked(id);

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _usbCharger.StopCharge();
                        _door.UnlockDoor();
                        //using (var writer = File.AppendText(logFile))
                        //{
                        //    writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        //}

                        _logfile.LogDoorUnlocked(id);

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        private int ReadIFID()
        {
            return 123;
        }

        public void DoorOpened()
        {
            _display.DisplayMessage("Tilslut telefon");
        }

        public void DoorClosed()
        {
            _display.DisplayMessage("Indlæs RFID");
        }

        //private bool CheckId(OldId id)
        //{

        //}

        // Her mangler de andre trigger handlere
    }
}
