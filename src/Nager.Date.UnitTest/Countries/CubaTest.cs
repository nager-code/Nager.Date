using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class CubaTest
    {
        [TestMethod]
        public void TestNewYearsEve()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 12, 31), CountryCode.CU));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2006, 12, 31), CountryCode.CU));
        }

        [TestMethod]
        public void TestGoodFriday()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 4, 3), CountryCode.CU));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2011, 4, 22), CountryCode.CU));
        }
    }
}
