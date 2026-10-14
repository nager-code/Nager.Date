using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class CaboVerdeTest
    {
        [TestMethod]
        public void TestAshWednesday()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 2, 18), CountryCode.CV));
        }

        [TestMethod]
        public void TestGoodFriday()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 4, 3), CountryCode.CV));
        }
    }
}
