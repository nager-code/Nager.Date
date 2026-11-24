using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        #region Chuseok

        [TestMethod]
        [DataRow(2017, "CHUSEOK-01", 2017, 10, 4)] //Oct 3 is National Foundation Day
        [DataRow(2017, "CHUSEOK-02", 2017, 10, 5)]
        [DataRow(2017, "CHUSEOK-03", 2017, 10, 6)] //Substitute holiday (korea.kr, 10 day holiday from Sep 30 to Oct 9)
        [DataRow(2025, "CHUSEOK-01", 2025, 10, 6)] //Oct 5 is a Sunday
        [DataRow(2025, "CHUSEOK-02", 2025, 10, 7)]
        [DataRow(2025, "CHUSEOK-03", 2025, 10, 8)] //Substitute holiday (KASI calendar 2025)
        [DataRow(2028, "CHUSEOK-01", 2028, 10, 2)]
        [DataRow(2028, "CHUSEOK-02", 2028, 10, 4)] //Oct 3 is National Foundation Day
        [DataRow(2028, "CHUSEOK-03", 2028, 10, 5)] //Substitute holiday (KASI calendar 2028)
        [DataRow(2036, "CHUSEOK-01", 2036, 10, 4)] //Oct 3 is National Foundation Day
        [DataRow(2036, "CHUSEOK-02", 2036, 10, 6)] //Oct 5 is a Sunday
        [DataRow(2036, "CHUSEOK-03", 2036, 10, 7)]
        [DataRow(2039, "CHUSEOK-01", 2039, 10, 1)]
        [DataRow(2039, "CHUSEOK-02", 2039, 10, 4)] //Oct 2 is a Sunday, Oct 3 is National Foundation Day
        [DataRow(2039, "CHUSEOK-03", 2039, 10, 5)]
        public void TestChuseok(int year, string holidayId, int observedYear, int month, int day)
        {
            this.AssertObservedDate(year, holidayId, observedYear, month, day);
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
    }
}
