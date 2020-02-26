using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ladeskab;

namespace Unittest
{   
    [TestFixture]
    public class StationControlUnittest
    {

        [Test]
        public void testmethod_test_true()
        {
            StationControl uut = new StationControl();

            bool result = uut.testMethod();

            Assert.IsTrue(result);
        }

    }
}
