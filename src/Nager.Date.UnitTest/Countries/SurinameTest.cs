using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class SurinameTest
    {
        [TestMethod]
        public void TestEasterMonday()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 4, 6), CountryCode.SR));
        }
    }
}
