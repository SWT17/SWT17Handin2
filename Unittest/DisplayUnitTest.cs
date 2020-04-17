using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Ladeskab;

namespace Unittest
{
    [TestFixture]
    public class DisplayUnitTest
    {
        IDisplay uut;
       

        [SetUp]
        public void Setup()
        {          
            uut = new Display();

        }

        [Test]
        public void Display1Message_CorrectStringArgumentRecieved_StringRecieved()
        {

            
        }
    }
}
