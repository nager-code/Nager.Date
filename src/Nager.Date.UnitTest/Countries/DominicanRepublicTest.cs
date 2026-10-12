using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class DominicanRepublicTest
    {
        [TestMethod]
        [DataRow(2026, 1, 5)]
        [DataRow(2026, 1, 26)]
        [DataRow(2026, 5, 4)]
        [DataRow(2026, 11, 9)]
        [DataRow(2025, 5, 5)]
        [DataRow(2022, 1, 24)]
        public void TestMovedHolidays(int year, int month, int day)
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.DO));
        }

        [TestMethod]
        [DataRow(2026, 1, 6)]
        [DataRow(2026, 5, 1)]
        [DataRow(2026, 11, 6)]
        [DataRow(2026, 5, 28)]
        public void TestNoPublicHoliday(int year, int month, int day)
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.DO));
        }

        [TestMethod]
        public void TestFixedHolidaysAreNotMoved()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 1, 21), CountryCode.DO));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 2, 27), CountryCode.DO));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 9, 24), CountryCode.DO));
        }
    }
}
