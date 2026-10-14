using Nager.Date.Extensions;
using Nager.Date.Models;
using Nager.Date.ReligiousProviders;
using System;
using System.Collections.Generic;

namespace Nager.Date.HolidayProviders
{
    /// <summary>
    /// Bolivia HolidayProvider
    /// </summary>
    internal sealed class BoliviaHolidayProvider : AbstractHolidayProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// Bolivia HolidayProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public BoliviaHolidayProvider(
            ICatholicProvider catholicProvider) : base(CountryCode.BO)
        {
            this._catholicProvider = catholicProvider;
        }

        /// <inheritdoc/>
        protected override IEnumerable<HolidaySpecification> GetHolidaySpecifications(int year)
        {
            var easterSunday = this._catholicProvider.EasterSunday(year);

            var holidaySpecifications = new List<HolidaySpecification>
            {
                new HolidaySpecification
                {
                    Id = "NEWYEARSDAY-01",
                    Date = new DateTime(year, 1, 1),
                    EnglishName = "New Year's Day",
                    LocalName = "Año Nuevo",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "LABOURDAY-01",
                    Date = new DateTime(year, 5, 1),
                    EnglishName = "Labour Day",
                    LocalName = "Dia del trabajo",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "ANDEANNEWYEAR-01",
                    Date = new DateTime(year, 6, 21),
                    EnglishName = "Andean New Year",
                    LocalName = "Año Nuevo Andino",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "INDEPENDENCEDAY-01",
                    Date = new DateTime(year, 8, 6),
                    EnglishName = "Independence Day",
                    LocalName = "Dia de la Patria",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "ALLSAINTSDAY-01",
                    Date = new DateTime(year, 11, 2),
                    EnglishName = "All Saints' Day",
                    LocalName = "Todos Santos",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "CHRISTMASDAY-01",
                    Date = new DateTime(year, 12, 25),
                    EnglishName = "Christmas Day",
                    LocalName = "Navidad",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "CARNIVAL-01",
                    Date = easterSunday.AddDays(-48),
                    EnglishName = "Carnival",
                    LocalName = "Feriado por Carnaval",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "CARNIVAL-02",
                    Date = easterSunday.AddDays(-47),
                    EnglishName = "Carnival",
                    LocalName = "Feriado por Carnaval",
                    HolidayTypes = HolidayTypes.Public,
                },
                this._catholicProvider.GoodFriday("Viernes Santo", year),
                this._catholicProvider.CorpusChristi("Corpus Christi", year)
            };

            holidaySpecifications.AddIfNotNull(this.PlurinationalStateFoundationDay(year));

            return holidaySpecifications;
        }

        private HolidaySpecification? PlurinationalStateFoundationDay(int year)
        {
            //Decreto Supremo N° 405 of 20 January 2010
            if (year < 2010)
            {
                return null;
            }

            return new HolidaySpecification
            {
                Id = "PLURINATIONALSTATEFOUNDATIONDAY-01",
                Date = new DateTime(year, 1, 22),
                EnglishName = "Plurinational State Foundation Day",
                LocalName = "Día de la Creación del Estado Plurinacional de Bolivia",
                HolidayTypes = HolidayTypes.Public,
            };
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetSources()
        {
            return
            [
                "https://en.wikipedia.org/wiki/Public_holidays_in_Bolivia",
                "https://ferrere.com/es/novedades/gobierno-central-define-nuevos-feriados-y-traslados-para-la-gestion-2026/"
            ];
        }
    }
}
