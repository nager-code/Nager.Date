using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class BelizeTest
    {
        [TestMethod]
        public void TestIndigenousPeoplesResistanceDay()
        {
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2026, 10, 12), CountryCode.BZ));
        }
    }
}
