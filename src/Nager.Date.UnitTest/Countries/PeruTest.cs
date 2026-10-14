using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class PeruTest
    {
        [TestMethod]
        public void TestFlagDay()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2024, 6, 7), CountryCode.PE));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 6, 7), CountryCode.PE));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2023, 6, 7), CountryCode.PE));
        }
    }
}
