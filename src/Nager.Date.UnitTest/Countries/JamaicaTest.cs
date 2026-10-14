using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class JamaicaTest
    {
        [TestMethod]
        [DataRow(2024, 10, 21)]
        [DataRow(2025, 10, 20)]
        [DataRow(2026, 10, 19)]
        [DataRow(2027, 10, 18)]
        public void TestNationalHeroesDay(int year, int month, int day)
        {
            var holiday = HolidaySystem.GetHolidays(year, CountryCode.JM).Single(o => o.Id == "JM-NATIONALHEROESDAY-01");
            Assert.AreEqual(new DateTime(year, month, day), holiday.Date);
        }

        [TestMethod]
        [DataRow(2025, 5, 23)]
        [DataRow(2026, 5, 25)]
        [DataRow(2027, 5, 24)]
        public void TestLabourDayObserved(int year, int month, int day)
        {
            var holiday = HolidaySystem.GetHolidays(year, CountryCode.JM).Single(o => o.Id == "JM-LABOURDAY-01");
            Assert.AreEqual(new DateTime(year, month, day), holiday.ObservedDate);
        }
    }
}
