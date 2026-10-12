using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class MoldovaTest
    {
        [TestMethod]
        [DataRow(2026, 4, 12)]
        [DataRow(2026, 4, 13)]
        [DataRow(2026, 4, 20)]
        [DataRow(2026, 6, 1)]
        [DataRow(2026, 12, 25)]
        public void TestPublicHolidays(int year, int month, int day)
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.MD));
        }

        [TestMethod]
        [DataRow(2026, 2, 23)]
        [DataRow(2026, 5, 22)]
        [DataRow(2026, 8, 24)]
        [DataRow(2026, 9, 3)]
        [DataRow(2026, 10, 14)]
        [DataRow(2026, 11, 21)]
        public void TestNoPublicHolidays(int year, int month, int day)
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.MD));
        }
    }
}
