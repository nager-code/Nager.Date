using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class RussiaTest
    {
        [TestMethod]
        public void TestNewYearHolidays()
        {
            for (var day = 1; day <= 8; day++)
            {
                Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 1, day), CountryCode.RU));
            }

            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2026, 1, 9), CountryCode.RU));
        }

        [TestMethod]
        public void TestNewYearHoliday8JanuaryBefore2013()
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2012, 1, 8), CountryCode.RU));
        }
    }
}
