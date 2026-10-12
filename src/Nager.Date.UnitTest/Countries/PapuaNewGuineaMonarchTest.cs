using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class PapuaNewGuineaMonarchTest
    {
        [TestMethod]
        public void TestMonarchBirthdayName()
        {
            Assert.AreEqual("Queen's Birthday", HolidaySystem.GetHolidays(2022, CountryCode.PG).Single(o => o.Id == "PG-QUEENSBIRTHDAY-01").EnglishName);
            Assert.AreEqual("King's Birthday", HolidaySystem.GetHolidays(2026, CountryCode.PG).Single(o => o.Id == "PG-QUEENSBIRTHDAY-01").EnglishName);
        }
    }
}
