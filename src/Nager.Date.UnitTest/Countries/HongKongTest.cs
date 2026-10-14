using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class HongKongTest
    {
        [TestMethod]
        [DataRow(2019, 4, 5)]
        [DataRow(2020, 4, 4)]
        [DataRow(2021, 4, 4)]
        [DataRow(2022, 4, 5)]
        [DataRow(2023, 4, 5)]
        [DataRow(2024, 4, 4)]
        [DataRow(2025, 4, 4)]
        [DataRow(2026, 4, 5)]
        [DataRow(2027, 4, 5)]
        public void TestChingMingFestival(int year, int month, int day)
        {
            var holiday = HolidaySystem.GetHolidays(year, CountryCode.HK).Single(o => o.Id == "HK-CHINGMINGFESTIVAL-01");
            Assert.AreEqual(new DateTime(year, month, day), holiday.Date);
        }

        [TestMethod]
        public void TestHolidays2026()
        {
            //General holidays for 2026 published by the Hong Kong government
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 4, 3), CountryCode.HK));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 4, 4), CountryCode.HK));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 4, 6), CountryCode.HK));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 4, 7), CountryCode.HK));
        }
    }
}
