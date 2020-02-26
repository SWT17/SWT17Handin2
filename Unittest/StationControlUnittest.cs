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

        [TestCase(true)]
        [TestCase(false)]
        public void testmethod_test_true(bool x)
        {
            StationControl uut = new StationControl();

            bool result = uut.testMethod(x);

            Assert.That(result, Is.EqualTo(x));
        }

    }
}
