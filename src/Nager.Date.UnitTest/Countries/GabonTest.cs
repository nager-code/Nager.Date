using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class GabonTest
    {
        [TestMethod]
        public void TestIndependenceDays()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 8, 16), CountryCode.GA));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 8, 17), CountryCode.GA));
        }

        [TestMethod]
        public void TestLiberationDay()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2024, 8, 30), CountryCode.GA));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2023, 8, 30), CountryCode.GA));
        }
    }
}
