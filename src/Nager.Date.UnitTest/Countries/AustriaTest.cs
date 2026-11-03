using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nager.Date.Extensions;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class AustriaTest() : BaseTest(CountryCode.AT)
    {
        [TestMethod]
        public void TestAustria2017()
        {
            var year = 2017;
            var publicHolidays = HolidaySystem.GetHolidays(year, CountryCode.AT).Where(o => o.HolidayTypes == Models.HolidayTypes.Public).ToArray();

            Assert.AreEqual(new DateTime(year, 1, 1), publicHolidays[0].Date, $"{publicHolidays[0].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 1, 6), publicHolidays[1].Date, $"{publicHolidays[1].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 4, 16), publicHolidays[2].Date, $"{publicHolidays[2].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 4, 17), publicHolidays[3].Date, $"{publicHolidays[3].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 5, 1), publicHolidays[4].Date, $"{publicHolidays[4].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 5, 25), publicHolidays[5].Date, $"{publicHolidays[5].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 6, 4), publicHolidays[6].Date, $"{publicHolidays[6].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 6, 5), publicHolidays[7].Date, $"{publicHolidays[7].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 6, 15), publicHolidays[8].Date, $"{publicHolidays[8].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 8, 15), publicHolidays[9].Date, $"{publicHolidays[9].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 10, 26), publicHolidays[10].Date, $"{publicHolidays[10].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 11, 1), publicHolidays[11].Date, $"{publicHolidays[11].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 12, 8), publicHolidays[12].Date, $"{publicHolidays[12].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 12, 25), publicHolidays[13].Date, $"{publicHolidays[13].EnglishName} is wrong");
            Assert.AreEqual(new DateTime(year, 12, 26), publicHolidays[14].Date, $"{publicHolidays[14].EnglishName} is wrong");
        }

        [TestMethod]
        public void TestAustriaPublicHolidays2026()
        {
            //Expected values: bmb.gv.at schulfreie Tage (gesetzliche Feiertage) and oenb.at bank holidays
            var year = 2026;
            var publicHolidays = HolidaySystem.GetHolidays(year, CountryCode.AT)
                .Where(o => o.HolidayTypes == Models.HolidayTypes.Public)
                .Select(o => o.Date)
                .ToArray();

            DateTime[] expected =
            [
                new DateTime(2026, 1, 1),
                new DateTime(2026, 1, 6),
                new DateTime(2026, 4, 5),
                new DateTime(2026, 4, 6),
                new DateTime(2026, 5, 1),
                new DateTime(2026, 5, 14),
                new DateTime(2026, 5, 24),
                new DateTime(2026, 5, 25),
                new DateTime(2026, 6, 4),
                new DateTime(2026, 8, 15),
                new DateTime(2026, 10, 26),
                new DateTime(2026, 11, 1),
                new DateTime(2026, 12, 8),
                new DateTime(2026, 12, 25),
                new DateTime(2026, 12, 26),
            ];

            CollectionAssert.AreEquivalent(expected, publicHolidays);
        }

        [TestMethod]
        [DataRow(1991)]
        [DataRow(1992)]
        [DataRow(2026)]
        public void TestAllSoulsDay(int year)
        {
            this.AssertObservedDate(year, "ALLSOULSDAY-01", year, 11, 2);
        }

        [TestMethod]
        [DataRow(1980)]
        [DataRow(1990)]
        public void TestAllSoulsDayNotBefore1991(int year)
        {
            this.AssertNoHoliday(year, "ALLSOULSDAY-01");
        }

        [TestMethod]
        public void TestChristmasEveBankHoliday2026()
        {
            var holiday = HolidaySystem.GetHolidays(2026, CountryCode.AT).Single(o => o.Id.EndsWith("CHRISTMASEVE-01"));
            Assert.AreEqual(new DateTime(2026, 12, 24), holiday.Date);
            Assert.AreEqual(Models.HolidayTypes.Bank, holiday.HolidayTypes);
        }

        [TestMethod]
        public void TestChristmasEveNotBefore2026()
        {
            this.AssertNoHoliday(2025, "CHRISTMASEVE-01");
        }

        [TestMethod]
        public void TestPatronSaintDays2026()
        {
            //Expected values: bmb.gv.at Festtage der Landespatrone
            this.AssertSubdivisionCodes(2026, "SAINTJOSEPHSDAY-01", "AT-2", "AT-6", "AT-7", "AT-8");
            this.AssertSubdivisionCodes(2026, "SAINTFLORIANSDAY-01", "AT-4");
            this.AssertSubdivisionCodes(2026, "SAINTRUPERTSDAY-01", "AT-5");
            this.AssertSubdivisionCodes(2026, "SAINTMARTINSDAY-01", "AT-1");
            this.AssertSubdivisionCodes(2026, "SAINTLEOPOLDSDAY-01", "AT-3", "AT-9");
        }

        [TestMethod]
        [DataRow(2018, 10, 8, false)]
        [DataRow(2018, 10, 9, false)]
        [DataRow(2018, 10, 10, false)]
        [DataRow(2018, 10, 11, false)]
        [DataRow(2018, 10, 12, false)]
        [DataRow(2018, 10, 13, true)]
        [DataRow(2018, 10, 14, true)]
        public void ChecksThatUniversalWeekendIsUsed(int year, int month, int day, bool expectedIsWeekend)
        {
            var date = new DateTime(year, month, day);
            var isWeekend = date.IsWeekend(CountryCode.AT);
            Assert.AreEqual(expectedIsWeekend, isWeekend);
        }
    }
}
