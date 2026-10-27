using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nager.Date.Models;
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
            //Day off for public sector workers only, not a public holiday
            Assert.IsFalse(HolidaySystem.GetHolidays(2021, CountryCode.NI).Any(o => o.Date == new DateTime(2021, 5, 30)));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2022, 5, 30), CountryCode.NI));

            var mothersDay = HolidaySystem.GetHolidays(2022, CountryCode.NI).Single(o => o.Date == new DateTime(2022, 5, 30));
            Assert.AreEqual(HolidayTypes.Authorities, mothersDay.HolidayTypes);
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

            //14 holidays according to Ley 1272, Mother's Day counts only for the public sector
            Assert.AreEqual(13, HolidaySystem.GetHolidays(2026, CountryCode.NI).Count(o => o.HolidayTypes.HasFlag(HolidayTypes.Public)));
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
