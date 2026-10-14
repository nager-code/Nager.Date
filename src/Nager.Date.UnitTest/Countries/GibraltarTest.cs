using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class GibraltarTest
    {
        [TestMethod]
        [DataRow(2024, 6, 17)]
        [DataRow(2025, 6, 16)]
        [DataRow(2026, 6, 15)]
        public void TestKingsBirthday(int year, int month, int day)
        {
            var holiday = HolidaySystem.GetHolidays(year, CountryCode.GI).Single(o => o.Id == "GI-QUEENSBIRTHDAY-01");
            Assert.AreEqual(new DateTime(year, month, day), holiday.Date);
            Assert.AreEqual("King's Birthday", holiday.EnglishName);
        }
    }
}
