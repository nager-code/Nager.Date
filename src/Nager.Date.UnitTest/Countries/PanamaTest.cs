using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class PanamaTest
    {
        [TestMethod]
        public void TestNationalDayOfMourning()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2025, 12, 20), CountryCode.PA));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 12, 21), CountryCode.PA));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2021, 12, 20), CountryCode.PA));
        }

        [TestMethod]
        public void TestNoFlagDayHoliday()
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2026, 11, 4), CountryCode.PA));
        }

        [TestMethod]
        public void TestTwelveNationalHolidays()
        {
            var publicHolidays = HolidaySystem.GetHolidays(2026, CountryCode.PA).Where(o => o.HolidayTypes.HasFlag(HolidayTypes.Public));
            Assert.AreEqual(12, publicHolidays.Count());
        }
    }
}
