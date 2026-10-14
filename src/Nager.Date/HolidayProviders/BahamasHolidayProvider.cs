using Nager.Date.Helpers;
using Nager.Date.Models;
using Nager.Date.ReligiousProviders;
using System;
using System.Collections.Generic;

namespace Nager.Date.HolidayProviders
{
    /// <summary>
    /// Bahamas HolidayProvider
    /// </summary>
    internal sealed class BahamasHolidayProvider : AbstractHolidayProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// Bahamas HolidayProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public BahamasHolidayProvider(
            ICatholicProvider catholicProvider) : base(CountryCode.BS)
        {
            this._catholicProvider = catholicProvider;
        }

        /// <inheritdoc/>
        protected override IEnumerable<HolidaySpecification> GetHolidaySpecifications(int year)
        {
            var firstFridayInJune = DateHelper.FindDay(year, Month.June, DayOfWeek.Friday, Occurrence.First);
            var firstMondayInAugust = DateHelper.FindDay(year, Month.August, DayOfWeek.Monday, Occurrence.First);
            var secondMondayInOctober = DateHelper.FindDay(year, Month.October, DayOfWeek.Monday, Occurrence.Second);

            var observedRuleSet = new ObservedRuleSet
            {
                Saturday = date => date.AddDays(2),
                Sunday = date => date.AddDays(1),
            };

            var christmasObservedRuleSet = new ObservedRuleSet
            {
                Saturday = date => date.AddDays(2),
                Sunday = date => date.AddDays(2),
            };

            var holidaySpecifications = new List<HolidaySpecification>
            {
                new HolidaySpecification
                {
                    Id = "NEWYEARSDAY-01",
                    Date = new DateTime(year, 1, 1),
                    EnglishName = "New Year's Day",
                    LocalName = "New Year's Day",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = observedRuleSet
                },
                new HolidaySpecification
                {
                    Id = "MAJORITYRULEDAY-01",
                    Date = new DateTime(year, 1, 10),
                    EnglishName = "Majority Rule Day",
                    LocalName = "Majority Rule Day",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = observedRuleSet
                },
                new HolidaySpecification
                {
                    Id = "LABOURDAY-01",
                    Date = firstFridayInJune,
                    EnglishName = "Labour Day",
                    LocalName = "Randol Fawkes Labour Day",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "INDEPENDENCEDAY-01",
                    Date = new DateTime(year, 7, 10),
                    EnglishName = "Independence Day",
                    LocalName = "Independence Day",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = observedRuleSet
                },
                new HolidaySpecification
                {
                    Id = "EMANCIPATIONDAY-01",
                    Date = firstMondayInAugust,
                    EnglishName = "Emancipation Day",
                    LocalName = "Emancipation Day",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "NATIONALHEROESDAY-01",
                    Date = secondMondayInOctober,
                    EnglishName = "National Heroes' Day",
                    LocalName = "National Heroes' Day",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "CHRISTMASDAY-01",
                    Date = new DateTime(year, 12, 25),
                    EnglishName = "Christmas Day",
                    LocalName = "Christmas Day",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = christmasObservedRuleSet
                },
                new HolidaySpecification
                {
                    Id = "STSTEPHENSDAY-01",
                    Date = new DateTime(year, 12, 26),
                    EnglishName = "St. Stephen's Day",
                    LocalName = "Boxing Day",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = christmasObservedRuleSet
                },
                this._catholicProvider.GoodFriday("Good Friday", year),
                this._catholicProvider.EasterMonday("Easter Monday", year),
                this._catholicProvider.WhitMonday("Whit Monday", year)
            };

            return holidaySpecifications;
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetSources()
        {
            return
            [
                "https://en.wikipedia.org/wiki/Public_holidays_in_the_Bahamas"
            ];
        }
    }
}
