using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class CaribbeanNetherlandsTest
    {
        [TestMethod]
        [DataRow(2025, 12, 5)]
        [DataRow(2026, 12, 4)]
        public void TestSabaDay(int year, int month, int day)
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.BQ, "BQ-SA"));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.BQ, "BQ-BO"));
        }
    }
}
