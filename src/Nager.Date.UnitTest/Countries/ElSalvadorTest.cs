using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class ElSalvadorTest
    {
        [TestMethod]
        [DataRow(2026, 1, 1)]
        [DataRow(2026, 4, 2)]
        [DataRow(2026, 4, 3)]
        [DataRow(2026, 4, 4)]
        [DataRow(2026, 5, 1)]
        [DataRow(2026, 5, 10)]
        [DataRow(2026, 6, 17)]
        [DataRow(2026, 8, 6)]
        [DataRow(2026, 9, 15)]
        [DataRow(2026, 11, 2)]
        [DataRow(2026, 12, 25)]
        public void TestNationalHolidays(int year, int month, int day)
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.SV));
        }

        [TestMethod]
        [DataRow(2026, 5, 3)]
        [DataRow(2026, 5, 7)]
        [DataRow(2026, 10, 1)]
        [DataRow(2026, 10, 12)]
        [DataRow(2026, 11, 10)]
        [DataRow(2026, 12, 31)]
        public void TestNoPublicHoliday(int year, int month, int day)
        {
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.SV));
        }

        [TestMethod]
        public void TestAugustFestivalsSanSalvador()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 8, 3), CountryCode.SV, "SV-SS"));
            Assert.IsFalse(HolidaySystem.IsPublicHoliday(new DateTime(2026, 8, 3), CountryCode.SV, "SV-SA"));
        }
    }
}
