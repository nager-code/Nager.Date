using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class SintMaartenTest
    {
        [TestMethod]
        public void TestCarnivalDay()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 4, 30), CountryCode.SX));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2026, 2, 16), CountryCode.SX));
        }

        [TestMethod]
        [DataRow(2023, 10, 9)]
        [DataRow(2026, 10, 12)]
        public void TestConstitutionDay(int year, int month, int day)
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.SX));
        }
    }
}
