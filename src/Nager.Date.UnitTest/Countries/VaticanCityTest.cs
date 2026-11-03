using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class VaticanCityTest
    {
        [TestMethod]
        public void TestAnniversaryOfTheElectionOfPopeLeoXIV()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 5, 8), CountryCode.VA));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2026, 3, 13), CountryCode.VA));
        }

        [TestMethod]
        public void TestAnniversaryOfTheElectionOfPopeFrancis()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2024, 3, 13), CountryCode.VA));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2024, 5, 8), CountryCode.VA));
        }

        [TestMethod]
        public void TestNameDayOfThePope()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2024, 4, 23), CountryCode.VA));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2025, 9, 17), CountryCode.VA));
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 9, 17), CountryCode.VA));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2026, 4, 23), CountryCode.VA));
        }
    }
}
