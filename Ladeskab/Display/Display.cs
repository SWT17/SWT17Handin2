using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ladeskab
{
   public class Display:IDisplay
    {
        public void Display1Message(string message)
        {
            Console.WriteLine("Display1: " + message);
        }

        public void Display2Message(string message)
        {
            Console.WriteLine("Display2:" + message);
        }

    }
}
