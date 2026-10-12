using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class BoliviaTest
    {
        [TestMethod]
        public void TestPlurinationalStateFoundationDay()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2025, 1, 22), CountryCode.BO));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2009, 1, 22), CountryCode.BO));
        }

        [TestMethod]
        public void TestNoNationalHoliday()
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2025, 2, 2), CountryCode.BO));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2025, 8, 2), CountryCode.BO));
        }
    }
}
