using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class NicaraguaTest
    {
        [TestMethod]
        public void TestHolyWeek()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2025, 4, 17), CountryCode.NI));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2025, 4, 18), CountryCode.NI));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2025, 4, 3), CountryCode.NI));
        }

        [TestMethod]
        public void TestMothersDay()
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2021, 5, 30), CountryCode.NI));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2022, 5, 30), CountryCode.NI));
        }

        [TestMethod]
        public void TestHolidaysSince2026()
        {
            var dates = new[] { new DateTime(2026, 1, 18), new DateTime(2026, 2, 2), new DateTime(2026, 2, 21), new DateTime(2026, 11, 8) };
            foreach (var date in dates)
            {
                Assert.IsTrue(HolidaySystem.IsPublicHoliday(date, CountryCode.NI));
                Assert.IsFalse(HolidaySystem.IsPublicHoliday(date.AddYears(-1), CountryCode.NI));
            }

            Assert.AreEqual(14, HolidaySystem.GetHolidays(2026, CountryCode.NI).Count());
        }

        [TestMethod]
        public void TestNoPublicHoliday()
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2025, 2, 1), CountryCode.NI));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2025, 5, 27), CountryCode.NI));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2025, 10, 12), CountryCode.NI));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2025, 12, 31), CountryCode.NI));
        }
    }
}
