using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class BahamasTest
    {
        [TestMethod]
        [DataRow("BS-LABOURDAY-01", 2026, 6, 5)]
        [DataRow("BS-EMANCIPATIONDAY-01", 2026, 8, 3)]
        [DataRow("BS-NATIONALHEROESDAY-01", 2026, 10, 12)]
        [DataRow("BS-NATIONALHEROESDAY-01", 2027, 10, 11)]
        public void TestMovableHolidays(string id, int year, int month, int day)
        {
            var holiday = HolidaySystem.GetHolidays(year, CountryCode.BS).Single(o => o.Id == id);
            Assert.AreEqual(new DateTime(year, month, day), holiday.Date);
        }

        [TestMethod]
        [DataRow("BS-NEWYEARSDAY-01", 2026, 1, 1)]
        [DataRow("BS-NEWYEARSDAY-01", 2022, 1, 3)]
        [DataRow("BS-MAJORITYRULEDAY-01", 2026, 1, 12)]
        [DataRow("BS-MAJORITYRULEDAY-01", 2024, 1, 10)]
        [DataRow("BS-STSTEPHENSDAY-01", 2026, 12, 28)]
        public void TestObservedDates(string id, int year, int month, int day)
        {
            var holiday = HolidaySystem.GetHolidays(year, CountryCode.BS).Single(o => o.Id == id);
            Assert.AreEqual(new DateTime(year, month, day), holiday.ObservedDate);
        }

        [TestMethod]
        public void TestNoPerryChristieDay()
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2025, 4, 1), CountryCode.BS));
        }
    }
}
