using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nager.Date.Extensions;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    /// <summary>
    /// Expected dates are taken from the official open data holiday calendar of the City of Vienna
    /// (https://www.wien.gv.at/gogv/l9ogdfeiertagskalender, type "HF", years 2004-2034)
    /// and the Feiertagsruhegesetz 1957 (BGBl. Nr. 153/1957).
    /// </summary>
    [TestClass]
    public class AustriaTest() : BaseTest(CountryCode.AT)
    {
        #region Fixed public holidays

        [TestMethod]
        [DataRow(2004, 2004, 1, 1)]
        [DataRow(2017, 2017, 1, 1)]
        [DataRow(2025, 2025, 1, 1)]
        [DataRow(2026, 2026, 1, 1)]
        [DataRow(2034, 2034, 1, 1)]
        public void TestNewYearsDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "NEWYEARSDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 1, 6)]
        [DataRow(2017, 2017, 1, 6)]
        [DataRow(2025, 2025, 1, 6)]
        [DataRow(2026, 2026, 1, 6)]
        [DataRow(2034, 2034, 1, 6)]
        public void TestEpiphany(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "EPIPHANY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 5, 1)]
        [DataRow(2008, 2008, 5, 1)]
        [DataRow(2019, 2019, 5, 1)]
        [DataRow(2020, 2020, 5, 1)]
        [DataRow(2026, 2026, 5, 1)]
        [DataRow(2034, 2034, 5, 1)]
        public void TestStaatsfeiertag(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "NATIONALHOLIDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 8, 15)]
        [DataRow(2017, 2017, 8, 15)]
        [DataRow(2026, 2026, 8, 15)]
        [DataRow(2034, 2034, 8, 15)]
        public void TestAssumptionDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "ASSUMPTIONDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 10, 26)]
        [DataRow(2017, 2017, 10, 26)]
        [DataRow(2026, 2026, 10, 26)]
        [DataRow(2034, 2034, 10, 26)]
        public void TestNationalfeiertag(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "NATIONALHOLIDAY-02", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 11, 1)]
        [DataRow(2017, 2017, 11, 1)]
        [DataRow(2026, 2026, 11, 1)]
        [DataRow(2034, 2034, 11, 1)]
        public void TestAllSaintsDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "ALLSAINTSDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 12, 8)]
        [DataRow(2017, 2017, 12, 8)]
        [DataRow(2026, 2026, 12, 8)]
        [DataRow(2034, 2034, 12, 8)]
        public void TestImmaculateConception(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "IMMACULATECONCEPTION-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 12, 25)]
        [DataRow(2017, 2017, 12, 25)]
        [DataRow(2026, 2026, 12, 25)]
        [DataRow(2034, 2034, 12, 25)]
        public void TestChristmasDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "CHRISTMASDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 12, 26)]
        [DataRow(2017, 2017, 12, 26)]
        [DataRow(2026, 2026, 12, 26)]
        [DataRow(2034, 2034, 12, 26)]
        public void TestStStephensDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "STSTEPHENSDAY-01", observedYear, month, day);
        }

        #endregion

        #region Easter based public holidays

        [TestMethod]
        [DataRow(2004, 2004, 4, 12)]
        [DataRow(2008, 2008, 3, 24)]
        [DataRow(2011, 2011, 4, 25)]
        [DataRow(2016, 2016, 3, 28)]
        [DataRow(2017, 2017, 4, 17)]
        [DataRow(2024, 2024, 4, 1)]
        [DataRow(2025, 2025, 4, 21)]
        [DataRow(2026, 2026, 4, 6)]
        [DataRow(2027, 2027, 3, 29)]
        [DataRow(2034, 2034, 4, 10)]
        public void TestEasterMonday(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "CEASTERMONDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 5, 20)]
        [DataRow(2008, 2008, 5, 1)] //Same day as Staatsfeiertag
        [DataRow(2011, 2011, 6, 2)]
        [DataRow(2017, 2017, 5, 25)]
        [DataRow(2024, 2024, 5, 9)]
        [DataRow(2025, 2025, 5, 29)]
        [DataRow(2026, 2026, 5, 14)]
        [DataRow(2027, 2027, 5, 6)]
        [DataRow(2034, 2034, 5, 18)]
        public void TestAscensionDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "CASCENSIONDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 5, 31)]
        [DataRow(2008, 2008, 5, 12)]
        [DataRow(2011, 2011, 6, 13)]
        [DataRow(2017, 2017, 6, 5)]
        [DataRow(2024, 2024, 5, 20)]
        [DataRow(2025, 2025, 6, 9)]
        [DataRow(2026, 2026, 5, 25)]
        [DataRow(2027, 2027, 5, 17)]
        [DataRow(2034, 2034, 5, 29)]
        public void TestWhitMonday(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "CWHITMONDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 6, 10)]
        [DataRow(2008, 2008, 5, 22)]
        [DataRow(2011, 2011, 6, 23)]
        [DataRow(2017, 2017, 6, 15)]
        [DataRow(2024, 2024, 5, 30)]
        [DataRow(2025, 2025, 6, 19)]
        [DataRow(2026, 2026, 6, 4)]
        [DataRow(2027, 2027, 5, 27)]
        [DataRow(2034, 2034, 6, 8)]
        public void TestCorpusChristi(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "CCORPUSCHRISTI-01", observedYear, month, day);
        }

        #endregion

        #region Not a public holiday

        [TestMethod]
        [DataRow(2004)]
        [DataRow(2018)]
        [DataRow(2019)]
        [DataRow(2026)]
        public void TestGoodFridayIsNotAPublicHoliday(int year)
        {
            //Karfreitag is only marked as a non-statutory day ("A") in the Vienna calendar,
            //since 2019 it is replaced by the "persönlicher Feiertag" (BGBl. I Nr. 22/2019)
            this.AssertNoHoliday(year, "CGOODFRIDAY-01");
        }

        #endregion

        #region Public holidays per year

        [TestMethod]
        [DataRow(2008)]
        [DataRow(2017)]
        [DataRow(2025)]
        [DataRow(2026)]
        [DataRow(2034)]
        public void TestNationwidePublicHolidayCount(int year)
        {
            //13 statutory holidays (Feiertagsruhegesetz 1957 §1) plus Easter Sunday and Whit Sunday
            var publicHolidays = HolidaySystem.GetHolidays(year, CountryCode.AT)
                .Where(o => o.HolidayTypes.HasFlag(Models.HolidayTypes.Public) && o.NationalHoliday)
                .ToArray();

            Assert.HasCount(15, publicHolidays);
        }

        #endregion

        #region Regional school holidays (Landespatrone)

        [TestMethod]
        public void TestSaintJosephsDay()
        {
            this.AssertObservedDate(2026, "SAINTJOSEPHSDAY-01", 2026, 3, 19);
            this.AssertSubdivisionCodes(2026, "SAINTJOSEPHSDAY-01", "AT-2", "AT-6", "AT-7", "AT-8");
        }

        [TestMethod]
        public void TestSaintFloriansDay()
        {
            this.AssertObservedDate(2026, "SAINTFLORIANSDAY-01", 2026, 5, 4);
            this.AssertSubdivisionCodes(2026, "SAINTFLORIANSDAY-01", "AT-4");
        }

        [TestMethod]
        public void TestSaintRupertsDay()
        {
            this.AssertObservedDate(2026, "SAINTRUPERTSDAY-01", 2026, 9, 24);
            this.AssertSubdivisionCodes(2026, "SAINTRUPERTSDAY-01", "AT-5");
        }

        [TestMethod]
        public void TestSaintMartinsDay()
        {
            this.AssertObservedDate(2026, "SAINTMARTINSDAY-01", 2026, 11, 11);
            this.AssertSubdivisionCodes(2026, "SAINTMARTINSDAY-01", "AT-1");
        }

        [TestMethod]
        public void TestSaintLeopoldsDay()
        {
            this.AssertObservedDate(2026, "SAINTLEOPOLDSDAY-01", 2026, 11, 15);
            this.AssertSubdivisionCodes(2026, "SAINTLEOPOLDSDAY-01", "AT-3", "AT-9");
        }

        #endregion

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
