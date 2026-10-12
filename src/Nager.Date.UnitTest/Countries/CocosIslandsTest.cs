using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class CocosIslandsTest
    {
        [TestMethod]
        [DataRow(2026, 6, 8)]
        [DataRow(2027, 6, 7)]
        public void TestKingsBirthday(int year, int month, int day)
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(year, month, day), CountryCode.CC));
        }
    }
}
