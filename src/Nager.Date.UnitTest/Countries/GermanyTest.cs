using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nager.Date.Extensions;
using Nager.Date.ReligiousProviders;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class GermanyTest() : BaseTest(CountryCode.DE)
    {
        [TestMethod]
        public void TestGermanyCorpusChristi()
        {
            var yearToTest = 2017;

            var catholicProvider = new CatholicProvider();
            var publicHolidays = HolidaySystem.GetHolidays(yearToTest, CountryCode.DE);
            var easterSunday = catholicProvider.EasterSunday(yearToTest);
            var corpusChristi = publicHolidays.First(x => x.LocalName == "Fronleichnam").Date;
            Assert.AreEqual(easterSunday.AddDays(60), corpusChristi);
        }

        [TestMethod]
        public void TestGermanyCorpusChristi2017()
        {
            var yearToTest = 2017;

            var publicHolidays = HolidaySystem.GetHolidays(yearToTest, CountryCode.DE);
            var corpusChristi = publicHolidays.First(x => x.LocalName == "Fronleichnam").Date;
            var expectedDate = new DateTime(yearToTest, 6, 15);
            Assert.AreEqual(expectedDate, corpusChristi);
        }

        [TestMethod]
        public void TestGermanyCorpusChristi2026()
        {
            var yearToTest = 2026;

            var publicHolidays = HolidaySystem.GetHolidays(yearToTest, CountryCode.DE);
            var corpusChristi = publicHolidays.First(x => x.LocalName == "Fronleichnam").Date;
            var expectedDate = new DateTime(yearToTest, 6, 4);
            Assert.AreEqual(expectedDate, corpusChristi);
        }

        [TestMethod]
        [DataRow(2019, 2021, 2020)]
        [DataRow(2024, 2026, 2025)]
        public void TestGermanyLiberationDay(int startYear, int endYear, int expectedYear)
        {
            var publicHolidays = HolidaySystem.GetHolidays(
                new DateTime(startYear, 5, 8),
                new DateTime(endYear, 5, 8),
                CountryCode.DE);

            var liberationDays = publicHolidays.Where(x => x.LocalName == "Tag der Befreiung").ToList();
            var liberationDay = liberationDays.FirstOrDefault();

            Assert.HasCount(1, liberationDays);
            Assert.IsNotNull(liberationDay);
            Assert.AreEqual(new DateTime(expectedYear, 5, 8), liberationDay.Date);
            Assert.IsNotNull(liberationDay.SubdivisionCodes);
            Assert.HasCount(1, liberationDay.SubdivisionCodes);
            Assert.AreEqual("DE-BE", liberationDay.SubdivisionCodes[0]);
        }

        [TestMethod]
        public void TestGermanyIsOfficialPublicHolidayByCountyWithCountySpecificEpiphany2017()
        {
            var isPublicHolidayInBW = HolidaySystem.IsPublicHoliday(new DateTime(2017, 1, 6), CountryCode.DE, "DE-BW");
            var isPublicHolidayInNW = HolidaySystem.IsPublicHoliday(new DateTime(2017, 1, 6), CountryCode.DE, "DE-NW");

            Assert.IsTrue(isPublicHolidayInBW);
            Assert.IsFalse(isPublicHolidayInNW);
        }

        [TestMethod]
        public void TestGermanyIsOfficialPublicHolidayByCountyWithGlobalChristmasDay2017()
        {
            var isPublicHolidayInBW = HolidaySystem.IsPublicHoliday(new DateTime(2017, 12, 25), CountryCode.DE, "DE-BW");

            Assert.IsTrue(isPublicHolidayInBW);
        }

        [TestMethod]
        public void TestGermanyIsOfficialPublicHolidayByCountyWithCountySpecificWorldChildrensDay()
        {
            var isPublicHolidayInTH2018 = HolidaySystem.IsPublicHoliday(new DateTime(2018, 9, 20), CountryCode.DE, "DE-TH");
            var isPublicHolidayInTH2019 = HolidaySystem.IsPublicHoliday(new DateTime(2019, 9, 20), CountryCode.DE, "DE-TH");
            var isPublicHolidayInTH2020 = HolidaySystem.IsPublicHoliday(new DateTime(2020, 9, 20), CountryCode.DE, "DE-TH");

            Assert.IsFalse(isPublicHolidayInTH2018);
            Assert.IsTrue(isPublicHolidayInTH2019);
            Assert.IsTrue(isPublicHolidayInTH2020);
        }

        [TestMethod]
        public void TestGermanyIsOfficialPublicHolidayByCountyLiberationDay()
        {
            const string CountyCodeBerlin = "DE-BE";

            var isPublicHolidayInBerlin2019 = HolidaySystem.IsPublicHoliday(new DateTime(2019, 5, 8), CountryCode.DE, CountyCodeBerlin);
            var isPublicHolidayInBerlin2020 = HolidaySystem.IsPublicHoliday(new DateTime(2020, 5, 8), CountryCode.DE, CountyCodeBerlin);
            var isPublicHolidayInBerlin2021 = HolidaySystem.IsPublicHoliday(new DateTime(2021, 5, 8), CountryCode.DE, CountyCodeBerlin);

            Assert.IsFalse(isPublicHolidayInBerlin2019);
            Assert.IsTrue(isPublicHolidayInBerlin2020);
            Assert.IsFalse(isPublicHolidayInBerlin2021);
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
            var isWeekend = date.IsWeekend(CountryCode.DE);
            Assert.AreEqual(expectedIsWeekend, isWeekend);
        }

        [TestMethod]
        [DataRow(1989, false)]
        [DataRow(1990, true)]
        public void TestGermanUnityDay(int year, bool expectedIsHoliday)
        {
            var isHoliday = HolidaySystem.IsPublicHoliday(new DateTime(year, 10, 3), CountryCode.DE);
            Assert.AreEqual(expectedIsHoliday, isHoliday);
        }

        [TestMethod]
        [DataRow(1954)]
        [DataRow(1958)]
        [DataRow(1963)]
        [DataRow(1980)]
        [DataRow(1989)]
        [DataRow(1990)]
        public void TestGermanUnityDayJune17(int year)
        {
            //Gesetz über den Tag der deutschen Einheit of 4 August 1953 (BGBl. I S. 778), repealed by the Unification Treaty 1990
            this.AssertObservedDate(year, "GERMANUNITYDAYJUNE17-01", year, 6, 17);

            var isHoliday = HolidaySystem.IsPublicHoliday(new DateTime(year, 6, 17), CountryCode.DE);
            Assert.IsTrue(isHoliday);
        }

        [TestMethod]
        [DataRow(1953)]
        [DataRow(1991)]
        [DataRow(2024)]
        [DataRow(2028)]
        public void TestGermanUnityDayJune17NotOutside1954To1990(int year)
        {
            this.AssertNoHoliday(year, "GERMANUNITYDAYJUNE17-01");
        }

        [TestMethod]
        public void TestGermanUnityDayBothDatesIn1990()
        {
            //1990 is the only year with both: 17 June (Federal Republic, until reunification) and 3 October (Unification Treaty)
            this.AssertObservedDate(1990, "GERMANUNITYDAYJUNE17-01", 1990, 6, 17);
            this.AssertObservedDate(1990, "GERMANUNITYDAY-01", 1990, 10, 3);
        }

        [TestMethod]
        [DataRow(1980, true)]
        [DataRow(1981, false)]
        [DataRow(1983, false)]
        [DataRow(1984, true)]
        [DataRow(2026, true)]
        public void TestAllSaintsDayBavaria(int year, bool expectedBavaria)
        {
            //Bavaria, Feiertagsgesetz 1980 Art. 9 Abs. 2: statewide only from 1 January 1984, before only in predominantly Catholic municipalities
            string[] expected = expectedBavaria
                ? ["DE-BW", "DE-BY", "DE-NW", "DE-RP", "DE-SL"]
                : ["DE-BW", "DE-NW", "DE-RP", "DE-SL"];

            this.AssertSubdivisionCodes(year, "ALLSAINTSDAY-01", expected);
        }

        [TestMethod]
        [DataRow(2018, new string[0])]
        [DataRow(2019, new[] { "DE-BE" })]
        [DataRow(2022, new[] { "DE-BE" })]
        [DataRow(2023, new[] { "DE-BE", "DE-MV" })]
        public void TestInternationalWomensDay(int year, string[] expectedSubdivisionCodes)
        {
            //Berlin since the law in force 7 February 2019, Mecklenburg-Vorpommern since the law of 7 July 2022 (in force 13 July 2022)
            if (expectedSubdivisionCodes.Length == 0)
            {
                this.AssertNoHoliday(year, "INTERNATIONALWOMENSDAY-01");
                return;
            }

            this.AssertSubdivisionCodes(year, "INTERNATIONALWOMENSDAY-01", expectedSubdivisionCodes);
        }

        [TestMethod]
        public void TestReformationDay()
        {
            //2017 one-time nationwide (500th anniversary), Bremen and Hamburg (and NI, SH) since 2018
            this.AssertSubdivisionCodes(2016, "REFORMATIONDAY-01", "DE-BB", "DE-MV", "DE-SN", "DE-ST", "DE-TH");
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(2017, 10, 31), CountryCode.DE, "DE-BY"));
            this.AssertSubdivisionCodes(2018, "REFORMATIONDAY-01", "DE-BB", "DE-MV", "DE-SN", "DE-ST", "DE-TH", "DE-HB", "DE-HH", "DE-NI", "DE-SH");
        }

        [TestMethod]
        public void TestRepentanceAndPrayerDay()
        {
            //Bremen law: listed until 9 December 1994, Berlin law version from 1 January 1995 without it
            Assert.IsTrue(HolidaySystem.IsPublicHoliday(new DateTime(1994, 11, 16), CountryCode.DE, "DE-HB"));
            this.AssertSubdivisionCodes(1995, "REPENTANCEANDPRAYERDAY-01", "DE-SN");
        }

        [TestMethod]
        public void TestBerlinUprisingOfJune17Anniversary2028()
        {
            //Berlin, one-time public holiday, 4th law amending the Sonn- und Feiertagsgesetz (GVBl. 2024 Nr. 28 S. 460)
            this.AssertObservedDate(2028, "75THANNIVERSARYOFTHEUPRISING-01", 2028, 6, 17);
            this.AssertSubdivisionCodes(2028, "75THANNIVERSARYOFTHEUPRISING-01", "DE-BE");
            this.AssertNoHoliday(2027, "75THANNIVERSARYOFTHEUPRISING-01");
            this.AssertNoHoliday(2029, "75THANNIVERSARYOFTHEUPRISING-01");
        }
    }
}
