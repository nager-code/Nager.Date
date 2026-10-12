using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class AlandTest
    {
        [TestMethod]
        [DataRow(2024, 11, 2)]
        [DataRow(2025, 11, 1)]
        [DataRow(2026, 10, 31)]
        [DataRow(2027, 11, 6)]
        public void TestAllSaintsDay(int year, int month, int day)
        {
            var isPublicHoliday = HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.AX);
            Assert.IsTrue(isPublicHoliday);
        }

        [TestMethod]
        [DataRow(2024, 6, 22)]
        [DataRow(2025, 6, 21)]
        [DataRow(2026, 6, 20)]
        [DataRow(2027, 6, 26)]
        public void TestMidsummerDay(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(date, CountryCode.AX));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(date.AddDays(-1), CountryCode.AX));
        }

        [TestMethod]
        public void TestNoHolidayOnWrongSaturday()
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2026, 11, 7), CountryCode.AX));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2027, 6, 19), CountryCode.AX));
        }
    }
}
