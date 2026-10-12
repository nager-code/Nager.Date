using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class NigeriaTest
    {
        [TestMethod]
        public void TestNoFederalPublicHoliday()
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2025, 5, 27), CountryCode.NG));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2025, 11, 1), CountryCode.NG));
        }
    }
}
