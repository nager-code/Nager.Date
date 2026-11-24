using Nager.Date.Models;
using Nager.Date.ReligiousProviders;
using System;
using System.Collections.Generic;

namespace Nager.Date.HolidayProviders
{
    /// <summary>
    /// Greenland HolidayProvider
    /// </summary>
    internal sealed class GreenlandHolidayProvider : AbstractHolidayProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// Greenland HolidayProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public GreenlandHolidayProvider(
            ICatholicProvider catholicProvider) : base(CountryCode.GL)
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
                    LocalName = "Ukiortaaq",
                    HolidayTypes = HolidayTypes.Public,
                },
                // 6 January is only listed as a flag day (Ullut erfalasulisaarfissat), not as a day off
                // Hjemmestyrets bekendtgørelse nr. 26 af 11.11.2008 and Selvstyrets bekendtgørelse nr. 55 af 11.07.2025 (Annex 1)
                new HolidaySpecification
                {
                    Id = "EPIPHANY-01",
                    Date = new DateTime(year, 1, 6),
                    EnglishName = "Epiphany",
                    LocalName = "Kunngit pingasut ulluat",
                    HolidayTypes = HolidayTypes.Observance
                },
                new HolidaySpecification
                {
                    Id = "GENERALPRAYERDAY-01",
                    Date = easterSunday.AddDays(26),
                    EnglishName = "General Prayer Day",
                    LocalName = "Tussiarfik",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "BANKCLOSINGDAY-01",
                    Date = easterSunday.AddDays(40),
                    EnglishName = "Bank closing day",
                    LocalName = "Atuanngiffik",
                    HolidayTypes = HolidayTypes.Bank | HolidayTypes.School | HolidayTypes.Optional
                },
                new HolidaySpecification
                {
                    Id = "ULLORTUNEQ-01",
                    Date = new DateTime(year, 6, 21),
                    EnglishName = "Ullortuneq",
                    LocalName = "Ullortuneq",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "CHRISTMASEVE-01",
                    Date = new DateTime(year, 12, 24),
                    EnglishName = "Christmas Eve",
                    LocalName = "Juulliaraq",
                    HolidayTypes = HolidayTypes.Bank | HolidayTypes.School | HolidayTypes.Optional
                },
                new HolidaySpecification
                {
                    Id = "CHRISTMASDAY-01",
                    Date = new DateTime(year, 12, 25),
                    EnglishName = "Christmas Day",
                    LocalName = "Juullip ullua",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "STSTEPHENSDAY-01",
                    Date = new DateTime(year, 12, 26),
                    EnglishName = "St. Stephen's Day",
                    LocalName = "Juullip-aappaa",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "NEWYEARSEVE-01",
                    Date = new DateTime(year, 12, 31),
                    EnglishName = "New Year's Eve",
                    LocalName = "Ukiutoqaq",
                    HolidayTypes = HolidayTypes.Bank | HolidayTypes.School | HolidayTypes.Optional
                },
                this._catholicProvider.MaundyThursday("Sisamanngortoq illernartoq", year),
                this._catholicProvider.GoodFriday("Tallimanngorneq tannaartoq", year),
                this._catholicProvider.EasterSunday("Poorskip ullua", year),
                this._catholicProvider.EasterMonday("Poorskip-aappaa", year),
                this._catholicProvider.AscensionDay("Qilaliarfik", year),
                this._catholicProvider.Pentecost("Piinsi", year),
                this._catholicProvider.WhitMonday("Piinsip-aappaa", year)
            };

            return holidaySpecifications;
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetSources()
        {
            return
            [
                "https://en.wikipedia.org/wiki/Public_holidays_in_Greenland",
                "https://www.norden.org/da/info-norden/helligdage-i-groenland",
                "https://nalunaarutit.gl/arkiv/gh.gl-love/dk/2008/bkg/bkg_nr_26-2008_dk.htm", // Flag days, 2008-2025
                "https://nalunaarutit.gl/Groenlandsk-lovgivning/2025/Selvstyrets-bekendtgoerelse-nr-55-af-11_07_2025" // Flag days, since 2025-08-01
            ];
        }
    }
}
