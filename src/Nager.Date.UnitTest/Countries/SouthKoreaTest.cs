using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nager.Date.Models;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    /// <summary>
    /// Expected dates are derived from the Regulation on Holidays of Government Offices (관공서의 공휴일에 관한 규정).
    /// The amendment of May 2023 extended the substitute holiday to Buddha's Birthday and Christmas Day
    /// (https://m.korea.kr/news/policyNewsView.do?newsId=148912746,
    /// https://www.mpm.go.kr/mpm/comm/newsPress/newsPressRelease/?cntId=3590&amp;mode=view).
    /// A holiday on a Saturday or Sunday is substituted by the following first non-holiday weekday.
    /// </summary>
    [TestClass]
    public class SouthKoreaTest() : BaseTest(CountryCode.KR)
    {
        #region Substitute holiday from 2023

        [TestMethod]
        [DataRow(2018, 2018, 12, 25)]
        [DataRow(2019, 2019, 12, 25)]
        [DataRow(2020, 2020, 12, 25)]
        [DataRow(2021, 2021, 12, 25)] //Saturday, no substitute holiday before 2023
        [DataRow(2022, 2022, 12, 25)] //Sunday, no substitute holiday before 2023
        [DataRow(2023, 2023, 12, 25)]
        [DataRow(2024, 2024, 12, 25)]
        [DataRow(2025, 2025, 12, 25)]
        [DataRow(2026, 2026, 12, 25)]
        [DataRow(2027, 2027, 12, 27)] //Saturday, substituted by Monday
        [DataRow(2028, 2028, 12, 25)]
        [DataRow(2029, 2029, 12, 25)]
        [DataRow(2030, 2030, 12, 25)]
        [DataRow(2031, 2031, 12, 25)]
        [DataRow(2032, 2032, 12, 27)] //Saturday, substituted by Monday
        [DataRow(2033, 2033, 12, 26)] //Sunday, substituted by Monday
        [DataRow(2034, 2034, 12, 25)]
        public void TestChristmasDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "CHRISTMASDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2022, 2022, 5, 8)] //Sunday, no substitute holiday before 2023
        [DataRow(2023, 2023, 5, 29)] //Saturday, first substitute holiday after the amendment
        [DataRow(2024, 2024, 5, 15)]
        [DataRow(2025, 2025, 5, 6)] //Same day as Children's Day, substituted by the next non-holiday (KASI calendar 2025)
        [DataRow(2026, 2026, 5, 25)] //Sunday, substituted by Monday
        [DataRow(2027, 2027, 5, 13)]
        [DataRow(2036, 2036, 5, 6)] //Saturday, Monday is the substitute holiday of Children's Day, substituted by Tuesday
        [DataRow(2039, 2039, 5, 3)] //Saturday, Monday is the substitute holiday of Labour Day, substituted by Tuesday
        [DataRow(2044, 2044, 5, 6)] //Same day as Children's Day, substituted by the next non-holiday
        public void TestBuddhasBirthday(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "BUDDHASBIRTHDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2025, 2025, 5, 5)]
        [DataRow(2036, 2036, 5, 5)]
        [DataRow(2044, 2044, 5, 5)]
        public void TestChildrensDayOnSameDayAsBuddhasBirthday(int year, int observedYear, int month, int day)
        {
            //The substitute holiday is assigned to Buddha's Birthday, Children's Day keeps its date
            this.AssertObservedDate(year, "CHILDRENSDAY-01", observedYear, month, day);
        }

        #endregion

        #region History of the Regulation on Holidays of Government Offices

        [TestMethod]
        public void TestNewYearHolidays()
        {
            //January 1 to 3 until 1989, January 1 and 2 until 1998 (Presidential Decrees No. 12616 and No. 15939)
            this.AssertObservedDate(1989, "NEWYEARSDAY-02", 1989, 1, 2);
            this.AssertObservedDate(1989, "NEWYEARSDAY-03", 1989, 1, 3);

            this.AssertObservedDate(1990, "NEWYEARSDAY-02", 1990, 1, 2);
            this.AssertNoHoliday(1990, "NEWYEARSDAY-03");

            this.AssertObservedDate(1998, "NEWYEARSDAY-02", 1998, 1, 2);
            this.AssertNoHoliday(1999, "NEWYEARSDAY-02");
            this.AssertObservedDate(1999, "NEWYEARSDAY-01", 1999, 1, 1);
        }

        [TestMethod]
        public void TestArborDay()
        {
            //Removed from 2006 (Presidential Decree No. 18893)
            this.AssertObservedDate(1980, "ARBORDAY-01", 1980, 4, 5);
            this.AssertObservedDate(2005, "ARBORDAY-01", 2005, 4, 5);
            this.AssertNoHoliday(2006, "ARBORDAY-01");
        }

        [TestMethod]
        public void TestArmedForcesDay()
        {
            //Public holiday from 1976 (Presidential Decree No. 8235) until 1990 (Presidential Decree No. 13155)
            this.AssertNoHoliday(1975, "ARMEDFORCESDAY-01");
            this.AssertObservedDate(1976, "ARMEDFORCESDAY-01", 1976, 10, 1);
            this.AssertObservedDate(1990, "ARMEDFORCESDAY-01", 1990, 10, 1);
            this.AssertNoHoliday(1991, "ARMEDFORCESDAY-01");
        }

        [TestMethod]
        public void TestNextDayRule1989()
        {
            //Presidential Decree No. 12616: a public holiday outside of the holiday periods on a Sunday, the next day is a public holiday as well
            this.AssertObservedDate(1989, "ARMEDFORCESDAY-01", 1989, 10, 2); //Sunday

            //Not in force before 1989 and after 1990
            this.AssertObservedDate(1988, "CONSTITUTIONDAY-01", 1988, 7, 17); //Sunday
            this.AssertObservedDate(1991, "CHILDRENSDAY-01", 1991, 5, 5); //Sunday
        }

        [TestMethod]
        [DataRow(1990, HolidayTypes.Public)]
        [DataRow(1991, HolidayTypes.Observance)] //Removed from the public holidays (Presidential Decree No. 13155)
        [DataRow(2012, HolidayTypes.Observance)]
        [DataRow(2013, HolidayTypes.Public)] //Public holiday again (Presidential Decree No. 24273)
        public void TestHangulDayHolidayType(int year, HolidayTypes expectedHolidayTypes)
        {
            var holiday = HolidaySystem.GetHolidays(year, CountryCode.KR).Single(o => o.Id.EndsWith("HANGULDAY-01"));

            Assert.AreEqual(new DateTime(year, 10, 9), holiday.Date);
            Assert.AreEqual(expectedHolidayTypes, holiday.HolidayTypes);
        }

        [TestMethod]
        [DataRow(1990, 1990, 5, 5)] //Saturday, no substitute holiday before 2014
        [DataRow(2012, 2012, 5, 5)] //Saturday, no substitute holiday before 2014
        [DataRow(2013, 2013, 5, 5)] //Sunday, no substitute holiday before 2014
        [DataRow(2014, 2014, 5, 5)]
        [DataRow(2018, 2018, 5, 7)] //Saturday, first substitute holiday after the amendment of November 2013
        [DataRow(2019, 2019, 5, 6)] //Sunday, substituted by Monday
        [DataRow(2024, 2024, 5, 6)] //Sunday, substituted by Monday (KASI calendar 2024)
        public void TestChildrensDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "CHILDRENSDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2020, "LIBERATIONDAY-01", 2020, 8, 15)] //Saturday, no substitute holiday before the amendment of August 2021
        [DataRow(2020, "NATIONALFOUNDATIONDAY-01", 2020, 10, 3)] //Saturday, no substitute holiday before the amendment of August 2021
        [DataRow(2021, "INDEPENDENCEMOVEMENTDAY-01", 2021, 3, 1)]
        [DataRow(2021, "LIBERATIONDAY-01", 2021, 8, 16)] //Sunday, substituted by Monday (korea.kr)
        [DataRow(2021, "NATIONALFOUNDATIONDAY-01", 2021, 10, 4)] //Sunday, substituted by Monday (korea.kr)
        [DataRow(2021, "HANGULDAY-01", 2021, 10, 11)] //Saturday, substituted by Monday (korea.kr)
        [DataRow(2025, "INDEPENDENCEMOVEMENTDAY-01", 2025, 3, 3)] //Saturday, substituted by Monday (KASI calendar 2025)
        [DataRow(2027, "LIBERATIONDAY-01", 2027, 8, 16)] //Sunday, substituted by Monday (KASI calendar 2027)
        [DataRow(2027, "NATIONALFOUNDATIONDAY-01", 2027, 10, 4)] //Sunday, substituted by Monday (KASI calendar 2027)
        [DataRow(2027, "HANGULDAY-01", 2027, 10, 11)] //Saturday, substituted by Monday (KASI calendar 2027)
        public void TestNationalHolidaySubstituteHoliday(int year, string holidayId, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, holidayId, observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 7, 17)] //Saturday, public holiday without substitute holiday
        [DataRow(2021, 2021, 7, 17)] //Saturday, not a public holiday from 2008 to 2025
        [DataRow(2026, 2026, 7, 17)]
        [DataRow(2027, 2027, 7, 19)] //Saturday, substituted by Monday (KASI calendar 2027)
        public void TestConstitutionDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "CONSTITUTIONDAY-01", observedYear, month, day);
        }

        #endregion

        #region Seollal

        [TestMethod]
        [DataRow(1985, "LUNARNEWYEAR-01", 2, 20, 2, 20)] //First year as public holiday (Presidential Decree No. 11615)
        [DataRow(1988, "LUNARNEWYEAR-01", 2, 18, 2, 18)]
        [DataRow(1989, "LUNARNEWYEAR-03", 2, 5, 2, 5)] //Three days since 1989 (Presidential Decree No. 12616)
        [DataRow(1989, "LUNARNEWYEAR-01", 2, 6, 2, 6)]
        [DataRow(1989, "LUNARNEWYEAR-02", 2, 7, 2, 7)]
        [DataRow(1990, "LUNARNEWYEAR-03", 1, 26, 1, 26)]
        [DataRow(1990, "LUNARNEWYEAR-01", 1, 27, 1, 27)]
        [DataRow(1990, "LUNARNEWYEAR-02", 1, 28, 1, 28)] //Sunday, the next-day rule of 1989 and 1990 did not apply to the holiday periods
        [DataRow(2012, "LUNARNEWYEAR-03", 1, 22, 1, 22)] //Sunday, no substitute holiday before 2014
        [DataRow(2012, "LUNARNEWYEAR-01", 1, 23, 1, 23)]
        [DataRow(2012, "LUNARNEWYEAR-02", 1, 24, 1, 24)]
        [DataRow(2013, "LUNARNEWYEAR-03", 2, 9, 2, 9)]
        [DataRow(2013, "LUNARNEWYEAR-01", 2, 10, 2, 10)] //Sunday, no substitute holiday before 2014
        [DataRow(2013, "LUNARNEWYEAR-02", 2, 11, 2, 11)]
        [DataRow(2014, "LUNARNEWYEAR-03", 1, 30, 1, 30)]
        [DataRow(2014, "LUNARNEWYEAR-01", 1, 31, 1, 31)]
        [DataRow(2014, "LUNARNEWYEAR-02", 2, 1, 2, 1)] //Saturday, no substitute holiday
        [DataRow(2016, "LUNARNEWYEAR-03", 2, 7, 2, 10)] //Sunday, substitute holiday (KASI, holidays from Feb 7 to Feb 10)
        [DataRow(2016, "LUNARNEWYEAR-01", 2, 8, 2, 8)]
        [DataRow(2016, "LUNARNEWYEAR-02", 2, 9, 2, 9)]
        [DataRow(2023, "LUNARNEWYEAR-03", 1, 21, 1, 21)] //Saturday, no substitute holiday
        [DataRow(2023, "LUNARNEWYEAR-01", 1, 22, 1, 24)] //Sunday, substitute holiday (KASI calendar 2023)
        [DataRow(2023, "LUNARNEWYEAR-02", 1, 23, 1, 23)]
        [DataRow(2024, "LUNARNEWYEAR-03", 2, 9, 2, 9)]
        [DataRow(2024, "LUNARNEWYEAR-01", 2, 10, 2, 10)] //Saturday, no substitute holiday
        [DataRow(2024, "LUNARNEWYEAR-02", 2, 11, 2, 12)] //Sunday, substitute holiday (KASI calendar 2024)
        [DataRow(2025, "LUNARNEWYEAR-03", 1, 28, 1, 28)]
        [DataRow(2025, "LUNARNEWYEAR-01", 1, 29, 1, 29)]
        [DataRow(2025, "LUNARNEWYEAR-02", 1, 30, 1, 30)]
        [DataRow(2027, "LUNARNEWYEAR-03", 2, 6, 2, 6)] //Saturday, no substitute holiday
        [DataRow(2027, "LUNARNEWYEAR-01", 2, 7, 2, 9)] //Sunday, substitute holiday (KASI calendar 2027)
        [DataRow(2027, "LUNARNEWYEAR-02", 2, 8, 2, 8)]
        [DataRow(2036, "LUNARNEWYEAR-03", 1, 27, 1, 30)] //Sunday, substituted by Wednesday
        [DataRow(2036, "LUNARNEWYEAR-01", 1, 28, 1, 28)]
        [DataRow(2036, "LUNARNEWYEAR-02", 1, 29, 1, 29)]
        public void TestSeollal(int year, string holidayId, int month, int day, int observedMonth, int observedDay)
        {
            //LUNARNEWYEAR-03 is the day before Seollal, LUNARNEWYEAR-01 is Seollal, LUNARNEWYEAR-02 is the day after Seollal
            this.AssertDateAndObservedDate(year, holidayId, month, day, observedMonth, observedDay);
        }

        [TestMethod]
        public void TestSeollalBefore1989()
        {
            //The first day of the lunisolar year is a public holiday since 1985, until 1988 as a single day (민속의 날)
            this.AssertNoHoliday(1984, "LUNARNEWYEAR-01");
            this.AssertNoHoliday(1984, "LUNARNEWYEAR-02");
            this.AssertNoHoliday(1984, "LUNARNEWYEAR-03");

            this.AssertNoHoliday(1985, "LUNARNEWYEAR-02");
            this.AssertNoHoliday(1985, "LUNARNEWYEAR-03");
            this.AssertNoHoliday(1988, "LUNARNEWYEAR-02");
            this.AssertNoHoliday(1988, "LUNARNEWYEAR-03");

            var holiday1988 = HolidaySystem.GetHolidays(1988, CountryCode.KR).Single(o => o.Id.EndsWith("LUNARNEWYEAR-01"));
            Assert.AreEqual("민속의 날", holiday1988.LocalName);

            var holiday1989 = HolidaySystem.GetHolidays(1989, CountryCode.KR).Single(o => o.Id.EndsWith("LUNARNEWYEAR-01"));
            Assert.AreEqual("설날", holiday1989.LocalName);
        }

        #endregion

        #region Chuseok

        [TestMethod]
        [DataRow(1985, "CHUSEOK-02", 9, 29, 9, 29)] //Only Chuseok itself is a public holiday until 1985
        [DataRow(1986, "CHUSEOK-02", 9, 18, 9, 18)]
        [DataRow(1986, "CHUSEOK-03", 9, 19, 9, 19)] //The day after Chuseok since 1986 (Presidential Decree No. 11962)
        [DataRow(1988, "CHUSEOK-02", 9, 25, 9, 25)]
        [DataRow(1988, "CHUSEOK-03", 9, 26, 9, 26)]
        [DataRow(1989, "CHUSEOK-01", 9, 13, 9, 13)] //The day before Chuseok since 1989 (Presidential Decree No. 12616)
        [DataRow(1989, "CHUSEOK-02", 9, 14, 9, 14)]
        [DataRow(1989, "CHUSEOK-03", 9, 15, 9, 15)]
        [DataRow(2012, "CHUSEOK-01", 9, 29, 9, 29)]
        [DataRow(2012, "CHUSEOK-02", 9, 30, 9, 30)] //Sunday, no substitute holiday before 2014
        [DataRow(2012, "CHUSEOK-03", 10, 1, 10, 1)]
        [DataRow(2013, "CHUSEOK-01", 9, 18, 9, 18)]
        [DataRow(2013, "CHUSEOK-02", 9, 19, 9, 19)]
        [DataRow(2013, "CHUSEOK-03", 9, 20, 9, 20)]
        [DataRow(2014, "CHUSEOK-01", 9, 7, 9, 10)] //Sunday, first substitute holiday after the amendment of November 2013
        [DataRow(2014, "CHUSEOK-02", 9, 8, 9, 8)]
        [DataRow(2014, "CHUSEOK-03", 9, 9, 9, 9)]
        [DataRow(2017, "CHUSEOK-01", 10, 3, 10, 6)] //Oct 3 is National Foundation Day, substitute holiday (korea.kr, 10 day holiday from Sep 30 to Oct 9)
        [DataRow(2017, "CHUSEOK-02", 10, 4, 10, 4)]
        [DataRow(2017, "CHUSEOK-03", 10, 5, 10, 5)]
        [DataRow(2025, "CHUSEOK-01", 10, 5, 10, 8)] //Sunday, substitute holiday (KASI calendar 2025)
        [DataRow(2025, "CHUSEOK-02", 10, 6, 10, 6)]
        [DataRow(2025, "CHUSEOK-03", 10, 7, 10, 7)]
        [DataRow(2026, "CHUSEOK-01", 9, 24, 9, 24)]
        [DataRow(2026, "CHUSEOK-02", 9, 25, 9, 25)]
        [DataRow(2026, "CHUSEOK-03", 9, 26, 9, 26)] //Saturday, no substitute holiday
        [DataRow(2028, "CHUSEOK-01", 10, 2, 10, 2)]
        [DataRow(2028, "CHUSEOK-02", 10, 3, 10, 5)] //Oct 3 is National Foundation Day, substitute holiday (KASI calendar 2028)
        [DataRow(2028, "CHUSEOK-03", 10, 4, 10, 4)]
        [DataRow(2036, "CHUSEOK-01", 10, 3, 10, 6)] //Oct 3 is National Foundation Day, substituted by Monday
        [DataRow(2036, "CHUSEOK-02", 10, 4, 10, 4)]
        [DataRow(2036, "CHUSEOK-03", 10, 5, 10, 7)] //Sunday, Monday is already a substitute holiday, substituted by Tuesday
        [DataRow(2039, "CHUSEOK-01", 10, 1, 10, 1)]
        [DataRow(2039, "CHUSEOK-02", 10, 2, 10, 4)] //Sunday, Monday is National Foundation Day, substituted by Tuesday
        [DataRow(2039, "CHUSEOK-03", 10, 3, 10, 5)] //Oct 3 is National Foundation Day, substituted by Wednesday
        public void TestChuseok(int year, string holidayId, int month, int day, int observedMonth, int observedDay)
        {
            this.AssertDateAndObservedDate(year, holidayId, month, day, observedMonth, observedDay);
        }

        [TestMethod]
        public void TestChuseokBefore1989()
        {
            this.AssertNoHoliday(1985, "CHUSEOK-01");
            this.AssertNoHoliday(1985, "CHUSEOK-03");

            this.AssertNoHoliday(1986, "CHUSEOK-01");
            this.AssertNoHoliday(1988, "CHUSEOK-01");
        }

        [TestMethod]
        [DataRow(2017)]
        [DataRow(2028)]
        [DataRow(2036)]
        [DataRow(2039)]
        public void TestNationalFoundationDayDuringChuseok(int year)
        {
            this.AssertObservedDate(year, "NATIONALFOUNDATIONDAY-01", year, 10, 3);
        }

        #endregion

        #region Election days

        [TestMethod]
        [DataRow(2002, 2002, 12, 19)]
        [DataRow(2007, 2007, 12, 19)]
        [DataRow(2012, 2012, 12, 19)]
        [DataRow(2017, 2017, 5, 9)]
        [DataRow(2022, 2022, 3, 9)]
        [DataRow(2025, 2025, 6, 3)]
        public void TestPresidentialElectionDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "PRESIDENTIALELECTIONDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2004, 2004, 4, 15)]
        [DataRow(2008, 2008, 4, 9)]
        [DataRow(2012, 2012, 4, 11)]
        [DataRow(2016, 2016, 4, 13)]
        [DataRow(2020, 2020, 4, 15)]
        [DataRow(2024, 2024, 4, 10)]
        public void TestNationalAssemblyElectionDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "NATIONALASSEMBLYELECTIONDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2002, 2002, 6, 13)]
        [DataRow(2006, 2006, 5, 31)]
        [DataRow(2010, 2010, 6, 2)]
        [DataRow(2014, 2014, 6, 4)]
        [DataRow(2018, 2018, 6, 13)]
        [DataRow(2022, 2022, 6, 1)]
        [DataRow(2026, 2026, 6, 3)]
        public void TestLocalElectionDay(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "LOCALELECTIONDAY-01", observedYear, month, day);
        }

        [TestMethod]
        [DataRow(2001)]
        [DataRow(2023)]
        [DataRow(2027)]
        public void TestNoElectionDay(int year)
        {
            this.AssertNoHoliday(year, "PRESIDENTIALELECTIONDAY-01");
            this.AssertNoHoliday(year, "NATIONALASSEMBLYELECTIONDAY-01");
            this.AssertNoHoliday(year, "LOCALELECTIONDAY-01");
        }

        #endregion

        #region Temporary public holidays

        [TestMethod]
        [DataRow(1980, 1980, 10, 22)]
        [DataRow(1988, 1988, 9, 17)]
        [DataRow(2002, 2002, 7, 1)]
        [DataRow(2015, 2015, 8, 14)]
        [DataRow(2016, 2016, 5, 6)]
        [DataRow(2017, 2017, 10, 2)]
        [DataRow(2020, 2020, 8, 17)]
        [DataRow(2023, 2023, 10, 2)]
        [DataRow(2024, 2024, 10, 1)]
        [DataRow(2025, 2025, 1, 27)]
        public void TestNationwideTemporaryPublicHoliday(int year, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, "TEMPORARYPUBLICHOLIDAY-01", observedYear, month, day);
        }

        [TestMethod]
        public void TestRegionalTemporaryPublicHolidays2005()
        {
            this.AssertObservedDate(2005, "TEMPORARYPUBLICHOLIDAY-01", 2005, 7, 27);
            this.AssertSubdivisionCodes(2005, "TEMPORARYPUBLICHOLIDAY-01", "KR-49");

            this.AssertObservedDate(2005, "TEMPORARYPUBLICHOLIDAY-02", 2005, 11, 18);
            this.AssertSubdivisionCodes(2005, "TEMPORARYPUBLICHOLIDAY-02", "KR-26");
        }

        [TestMethod]
        [DataRow(2019)]
        [DataRow(2026)]
        public void TestNoTemporaryPublicHoliday(int year)
        {
            this.AssertNoHoliday(year, "TEMPORARYPUBLICHOLIDAY-01");
        }

        #endregion

        private void AssertDateAndObservedDate(int year, string holidayId, int month, int day, int observedMonth, int observedDay)
        {
            //The date is the day of the lunisolar calendar, a substitute holiday only changes the observed date
            var holiday = HolidaySystem.GetHolidays(year, CountryCode.KR).Single(o => o.Id.EndsWith(holidayId));

            Assert.AreEqual(new DateTime(year, month, day), holiday.Date);
            Assert.AreEqual(new DateTime(year, observedMonth, observedDay), holiday.ObservedDate);
        }
    }
}
