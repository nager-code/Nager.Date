using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class EcuadorTest
    {
        [TestMethod]
        [DataRow(2025, 5, 2)]
        [DataRow(2025, 8, 11)]
        [DataRow(2025, 10, 10)]
        [DataRow(2026, 5, 25)]
        [DataRow(2026, 8, 10)]
        [DataRow(2026, 10, 9)]
        public void TestMovedHolidays(int year, int month, int day)
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.EC));
        }

        [TestMethod]
        [DataRow(2025, 5, 1)]
        [DataRow(2025, 10, 9)]
        public void TestOriginalDateIsNotDayOff(int year, int month, int day)
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.EC));
        }
    }
}
