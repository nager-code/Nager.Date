using Nager.Date.Extensions;
using Nager.Date.Models;
using Nager.Date.ReligiousProviders;
using System;
using System.Collections.Generic;

namespace Nager.Date.HolidayProviders
{
    /// <summary>
    /// Nicaragua HolidayProvider
    /// </summary>
    internal sealed class NicaraguaHolidayProvider : AbstractHolidayProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// Nicaragua HolidayProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public NicaraguaHolidayProvider(
            ICatholicProvider catholicProvider) : base(CountryCode.NI)
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
                    LocalName = "Año Nuevo",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "LABOURDAY-01",
                    Date = new DateTime(year, 5, 1),
                    EnglishName = "Labour Day",
                    LocalName = "Día Internacional de los Trabajadores",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "LIBERATIONDAY-01",
                    Date = new DateTime(year, 7, 19),
                    EnglishName = "Liberation Day",
                    LocalName = "Día de la Revolución Popular Sandinista",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "BATTLESANJACINTO-01",
                    Date = new DateTime(year, 9, 14),
                    EnglishName = "Battle of San Jacinto",
                    LocalName = "Batalla de San Jacinto",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "INDEPENDENCEDAY-01",
                    Date = new DateTime(year, 9, 15),
                    EnglishName = "Independence Day",
                    LocalName = "Día de la Independencia",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "IMMACULATECONCEPTION-01",
                    Date = new DateTime(year, 12, 8),
                    EnglishName = "Immaculate Conception",
                    LocalName = "Día de la Purísima Concepción",
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
                this._catholicProvider.MaundyThursday("Jueves Santo", year),
                this._catholicProvider.GoodFriday("Viernes Santo", year),
            };

            holidaySpecifications.AddIfNotNull(this.MothersDay(year));
            holidaySpecifications.AddRangeIfNotNull(this.HolidaysSince2026(year));

            return holidaySpecifications;
        }

        private HolidaySpecification? MothersDay(int year)
        {
            //Ley N.º 1118, La Gaceta N.º 92 of 20 May 2022
            if (year < 2022)
            {
                return null;
            }

            return new HolidaySpecification
            {
                Id = "MOTHERSDAY-01",
                Date = new DateTime(year, 5, 30),
                EnglishName = "Mother's Day",
                LocalName = "Día de las Madres",
                HolidayTypes = HolidayTypes.Public,
            };
        }

        private HolidaySpecification[] HolidaysSince2026(int year)
        {
            //Ley N.º 1272, La Gaceta N.º 11 of 20 January 2026
            if (year < 2026)
            {
                return [];
            }

            return
            [
                new HolidaySpecification
                {
                    Id = "RUBENDARIOBIRTHDAY-01",
                    Date = new DateTime(year, 1, 18),
                    EnglishName = "Birthday of Rubén Darío",
                    LocalName = "Natalicio de Rubén Darío",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "RECONCILIATIONANDPEACEDAY-01",
                    Date = new DateTime(year, 2, 2),
                    EnglishName = "National Day of Reconciliation and Peace",
                    LocalName = "Día Nacional de la Reconciliación y la Paz",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "SANDINODAY-01",
                    Date = new DateTime(year, 2, 21),
                    EnglishName = "Anniversary of the Death of Augusto C. Sandino",
                    LocalName = "Paso a la Inmortalidad del General Augusto C. Sandino",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "CARLOSFONSECADAY-01",
                    Date = new DateTime(year, 11, 8),
                    EnglishName = "Anniversary of the Death of Carlos Fonseca",
                    LocalName = "Paso a la Inmortalidad del Comandante Carlos Fonseca Amador",
                    HolidayTypes = HolidayTypes.Public,
                },
            ];
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetSources()
        {
            return
            [
                "https://en.wikipedia.org/wiki/Public_holidays_in_Nicaragua",
                "https://www.revistaeyn.com/centroamericaymundo/nicaragua-con-mas-descansos-en-el-ano-14-feriados-NE28979444",
                "https://www.bloomberglinea.com/2025/07/17/esto-dice-mitrab-sobre-el-pago-del-feriado-del-19-de-julio-en-nicaragua/"
            ];
        }
    }
}
