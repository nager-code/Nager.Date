using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class CaymanIslandsTest
    {
        [TestMethod]
        [DataRow(2023, 6, 19)]
        [DataRow(2024, 6, 17)]
        [DataRow(2026, 6, 15)]
        public void TestKingsBirthday(int year, int month, int day)
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.KY));
        }
    }
}
