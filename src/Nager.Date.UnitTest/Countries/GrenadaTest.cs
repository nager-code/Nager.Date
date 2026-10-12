using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class GrenadaTest
    {
        [TestMethod]
        [DataRow(2025, 8, 11)]
        [DataRow(2026, 8, 10)]
        [DataRow(2027, 8, 9)]
        public void TestCarnival(int year, int month, int day)
        {
            var carnivalMonday = new DateTime(year, month, day);
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(carnivalMonday, CountryCode.GD));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(carnivalMonday.AddDays(1), CountryCode.GD));
        }
    }
}
