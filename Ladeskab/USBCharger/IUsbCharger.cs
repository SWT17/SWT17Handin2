using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    /// <summary>
    /// Givet kode
    /// </summary>
    
    public interface IUsbCharger
        {
            // Event triggered on new current value
            event EventHandler<CurrentEventArgs> CurrentValueEvent;

            // Direct access to the current current value
            double CurrentValue { get; }

            // Require connection status of the phone
            bool Connected { get; }

            // Start charging
            void StartCharge();
            // Stop charging
            void StopCharge();
            void SimulateOverload(bool overload);

        }
    }