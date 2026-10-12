using Nager.Date.Extensions;
using Nager.Date.Models;
using Nager.Date.ReligiousProviders;
using System;
using System.Collections.Generic;

namespace Nager.Date.HolidayProviders
{
    /// <summary>
    /// Cuba HolidayProvider
    /// </summary>
    internal sealed class CubaHolidayProvider : AbstractHolidayProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// Cuba HolidayProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public CubaHolidayProvider(
            ICatholicProvider catholicProvider) : base(CountryCode.CU)
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
                    Id = "TRIUMPHREVOLUTION-01",
                    Date = new DateTime(year, 1, 1),
                    EnglishName = "Triumph of the Revolution",
                    LocalName = "Triunfo de la Revolución",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "VICTORYFIDELCASTRO-01",
                    Date = new DateTime(year, 1, 2),
                    EnglishName = "Victory of Fidel Castro",
                    LocalName = "Día de Victoria de las Fuerzas Armadas",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "LABOURDAY-01",
                    Date = new DateTime(year, 5, 1),
                    EnglishName = "Labour Day",
                    LocalName = "Día de los trabajadores",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "MONCADAGARRISON-01",
                    Date = new DateTime(year, 7, 25),
                    EnglishName = "Day before the Commemoration of the Assault of the Moncada garrison",
                    LocalName = "Conmemoración del asalto a Moncada",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "NATIONALRESISTANCEDAY-01",
                    Date = new DateTime(year, 7, 26),
                    EnglishName = "Commemoration of the Assault of the Moncada garrison",
                    LocalName = "Día de la Rebeldía Nacional",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "MONCADAGARRISON-02",
                    Date = new DateTime(year, 7, 27),
                    EnglishName = "Day after the Commemoration of the Assault of the Moncada garrison",
                    LocalName = "Conmemoración del asalto a Moncada",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "INDEPENDENCEDAY-01",
                    Date = new DateTime(year, 10, 10),
                    EnglishName = "Independence Day",
                    LocalName = "Día de la Independencia",
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
            };

            holidaySpecifications.AddIfNotNull(this.GoodFriday(year));
            holidaySpecifications.AddIfNotNull(this.NewYearsEve(year));

            return holidaySpecifications;
        }

        private HolidaySpecification? GoodFriday(int year)
        {
            //Good Friday is a public holiday since 2012
            if (year < 2012)
            {
                return null;
            }

            return this._catholicProvider.GoodFriday("Viernes Santo", year);
        }

        private HolidaySpecification? NewYearsEve(int year)
        {
            //31 December and 2 January are public holidays since 2007 (Ley No. 116, Código de Trabajo)
            if (year < 2007)
            {
                return null;
            }

            return new HolidaySpecification
            {
                Id = "NEWYEARSEVE-01",
                Date = new DateTime(year, 12, 31),
                EnglishName = "New Year's Eve",
                LocalName = "Fin de Año",
                HolidayTypes = HolidayTypes.Public,
            };
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetSources()
        {
            return
            [
                "https://en.wikipedia.org/wiki/Public_holidays_in_Cuba",
                "https://www.directoriocubano.info/feriados",
            ];
        }
    }
}
