using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class KazakhstanTest
    {
        [TestMethod]
        public void TestOrthodoxChristmas()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 1, 7), CountryCode.KZ));
        }
    }
}
