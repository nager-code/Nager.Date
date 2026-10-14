using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class BotswanaTest
    {
        [TestMethod]
        [DataRow(2026, 1, 2)]
        [DataRow(2026, 4, 4)]
        [DataRow(2026, 7, 20)]
        [DataRow(2026, 7, 21)]
        [DataRow(2026, 10, 1)]
        public void TestPublicHolidays(int year, int month, int day)
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.BW));
        }
    }
}
