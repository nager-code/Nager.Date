using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class TurksAndCaicosIslandsTest
    {
        [TestMethod]
        [DataRow(2024, 6, 17)]
        [DataRow(2026, 6, 15)]
        public void TestKingsBirthday(int year, int month, int day)
        {
            var holiday = HolidaySystem.GetHolidays(year, CountryCode.TC).Single(o => o.Id == "TC-KINGSBIRTHDAY-01");
            Assert.AreEqual(new DateTime(year, month, day), holiday.Date);
        }

        [TestMethod]
        public void TestObservedHolidays2026()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 8, 3), CountryCode.TC));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 12, 28), CountryCode.TC));
        }
    }
}
