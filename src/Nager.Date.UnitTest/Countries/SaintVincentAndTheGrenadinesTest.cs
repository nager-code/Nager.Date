using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class SaintVincentAndTheGrenadinesTest
    {
        [TestMethod]
        [DataRow(2022, 7, 4)]
        [DataRow(2024, 7, 8)]
        [DataRow(2025, 7, 7)]
        [DataRow(2026, 7, 6)]
        public void TestCarnival(int year, int month, int day)
        {
            var carnivalMonday = new DateTime(year, month, day);
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(carnivalMonday, CountryCode.VC));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(carnivalMonday.AddDays(1), CountryCode.VC));
        }

        [TestMethod]
        public void TestNoCarnivalInFebruary()
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2026, 2, 16), CountryCode.VC));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2026, 2, 17), CountryCode.VC));
        }

        [TestMethod]
        public void TestEmancipationDay()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2025, 8, 1), CountryCode.VC));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2025, 8, 5), CountryCode.VC));
        }
    }
}
