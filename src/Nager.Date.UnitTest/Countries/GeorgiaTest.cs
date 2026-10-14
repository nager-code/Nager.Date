using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class GeorgiaTest
    {
        [TestMethod]
        public void TestDayOfFamilySanctity()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2024, 5, 17), CountryCode.GE));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 5, 17), CountryCode.GE));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2023, 5, 17), CountryCode.GE));
        }
    }
}
