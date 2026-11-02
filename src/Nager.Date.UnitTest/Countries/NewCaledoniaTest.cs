using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class NewCaledoniaTest
    {
        [TestMethod]
        public void TestEasterMonday()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 4, 6), CountryCode.NC));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2026, 4, 3), CountryCode.NC));
        }

        [TestMethod]
        public void TestNoNewYearsEve()
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2026, 12, 31), CountryCode.NC));
        }

        [TestMethod]
        public void TestCitizenshipDay()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 9, 24), CountryCode.NC));
        }
    }
}
