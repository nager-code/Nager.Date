using Nager.Date.Extensions;
using Nager.Date.Models;
using Nager.Date.ReligiousProviders;
using System;
using System.Collections.Generic;

namespace Nager.Date.HolidayProviders
{
    /// <summary>
    /// Gabon HolidayProvider
    /// </summary>
    internal sealed class GabonHolidayProvider : AbstractHolidayProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// Gabon HolidayProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public GabonHolidayProvider(
            ICatholicProvider catholicProvider) : base(CountryCode.GA)
        {
            this._catholicProvider = catholicProvider;
        }

        /// <inheritdoc/>
        protected override IEnumerable<HolidaySpecification> GetHolidaySpecifications(int year)
        {
            var holidaySpecifications = new List<HolidaySpecification>
            {
                new HolidaySpecification
                {
                    Id = "NEWYEARSDAY-01",
                    Date = new DateTime(year, 1, 1),
                    EnglishName = "New Year's Day",
                    LocalName = "Jour de l'an",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "RENOVATIONDAY-01",
                    Date = new DateTime(year, 3, 12),
                    EnglishName = "Renovation Day",
                    LocalName = "Fête de la Rénovation",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "WOMENSDAY-01",
                    Date = new DateTime(year, 4, 17),
                    EnglishName = "Women's Day",
                    LocalName = "Journée de la femme",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "LABOURDAY-01",
                    Date = new DateTime(year, 5, 1),
                    EnglishName = "Labour Day",
                    LocalName = "Fête du Travail",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "MARTYRSDAY-01",
                    Date = new DateTime(year, 5, 6),
                    EnglishName = "Martyr's Day",
                    LocalName = "Journée des martyrs",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "ASSUMPTIONDAY-01",
                    Date = new DateTime(year, 8, 15),
                    EnglishName = "Assumption Day",
                    LocalName = "Assomption",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "INDEPENDENCEDAY-02",
                    Date = new DateTime(year, 8, 16),
                    EnglishName = "Independence Day",
                    LocalName = "Fête de l'Indépendance",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "INDEPENDENCEDAY-01",
                    Date = new DateTime(year, 8, 17),
                    EnglishName = "Independence Day",
                    LocalName = "Fête de l'Indépendance",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "ALLSAINTSDAY-01",
                    Date = new DateTime(year, 11, 1),
                    EnglishName = "All Saints' Day",
                    LocalName = "Toussaint",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "CHRISTMASDAY-01",
                    Date = new DateTime(year, 12, 25),
                    EnglishName = "Christmas Day",
                    LocalName = "Noël",
                    HolidayTypes = HolidayTypes.Public,
                },
                this._catholicProvider.EasterMonday("Lundi de Pâques", year),
                this._catholicProvider.WhitMonday("Lundi de Pentecôte", year)
            };

            holidaySpecifications.AddIfNotNull(this.LiberationDay(year));

            return holidaySpecifications;
        }

        private HolidaySpecification? LiberationDay(int year)
        {
            //Decree of January 2024, commemorates the liberation of 30 August 2023
            if (year < 2024)
            {
                return null;
            }

            return new HolidaySpecification
            {
                Id = "LIBERATIONDAY-01",
                Date = new DateTime(year, 8, 30),
                EnglishName = "National Liberation Day",
                LocalName = "Journée nationale de la Libération",
                HolidayTypes = HolidayTypes.Public,
            };
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetSources()
        {
            return
            [
                "https://en.wikipedia.org/wiki/Public_holidays_in_Gabon",
                "https://gabonmediatime.com/gabon-le-30-aout-decretee-une-journee-nationale-de-la-liberation/",
                "https://gabonmediatime.com/gabon-les-15-16-et-17-aout-2026-declares-feries-chomes-et-payes/",
            ];
        }
    }
}
