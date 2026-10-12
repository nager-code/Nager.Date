using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class GuernseyTest
    {
        [TestMethod]
        [DataRow(2025, 5, 5)]
        [DataRow(2026, 5, 4)]
        [DataRow(2027, 5, 3)]
        public void TestEarlyMayBankHoliday(int year, int month, int day)
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.GG));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(year, 5, 1), CountryCode.GG));
        }
    }
}
