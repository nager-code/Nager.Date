using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class NorthernMarianaIslandsTest
    {
        [TestMethod]
        [DataRow("MARTINLUTHERKINGJRDAY-01", 2025, 1, 20)]
        [DataRow("MARTINLUTHERKINGJRDAY-01", 2026, 1, 19)]
        [DataRow("PRESIDENTSDAY-01", 2025, 2, 17)]
        [DataRow("PRESIDENTSDAY-01", 2026, 2, 16)]
        [DataRow("MEMORIALDAY-01", 2025, 5, 26)]
        [DataRow("MEMORIALDAY-01", 2026, 5, 25)]
        [DataRow("LABOURDAY-01", 2025, 9, 1)]
        [DataRow("LABOURDAY-01", 2026, 9, 7)]
        [DataRow("COMMONWEALTHCULTURALDAY-01", 2025, 10, 13)]
        [DataRow("COMMONWEALTHCULTURALDAY-01", 2026, 10, 12)]
        [DataRow("THANKSGIVING-01", 2025, 11, 27)]
        [DataRow("THANKSGIVING-01", 2026, 11, 26)]
        public void TestMovableHolidays(string id, int year, int month, int day)
        {
            var holidays = HolidaySystem.GetHolidays(year, CountryCode.MP);
            var holiday = holidays.Single(o => o.Id == $"MP-{id}");
            Assert.AreEqual(new DateTime(year, month, day), holiday.Date);
        }
    }
}
