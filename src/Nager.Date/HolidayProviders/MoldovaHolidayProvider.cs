using Nager.Date.Models;
using Nager.Date.ReligiousProviders;
using System;
using System.Collections.Generic;

namespace Nager.Date.HolidayProviders
{
    /// <summary>
    /// Moldova HolidayProvider
    /// </summary>
    internal sealed class MoldovaHolidayProvider : AbstractHolidayProvider
    {
        private readonly IOrthodoxProvider _orthodoxProvider;

        /// <summary>
        /// Moldova HolidayProvider
        /// </summary>
        /// <param name="orthodoxProvider"></param>
        public MoldovaHolidayProvider(
            IOrthodoxProvider orthodoxProvider) : base(CountryCode.MD)
        {
            this._orthodoxProvider = orthodoxProvider;
        }

        /// <inheritdoc/>
        protected override IEnumerable<HolidaySpecification> GetHolidaySpecifications(int year)
        {
            var easterSunday = this._orthodoxProvider.EasterSunday(year);

            var holidaySpecifications = new List<HolidaySpecification>
            {
                new HolidaySpecification
                {
                    Id = "NEWYEARSDAY-01",
                    Date = new DateTime(year, 1, 1),
                    EnglishName = "New Year's Day",
                    LocalName = "Anul Nou",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "OCHRISTMAS-01",
                    Date = new DateTime(year, 1, 7),
                    EnglishName = "Christmas Day (Orthodox)",
                    LocalName = "Crăciunul pe stil vechi",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "OCHRISTMAS-02",
                    Date = new DateTime(year, 1, 8),
                    EnglishName = "Christmas Day (Orthodox)",
                    LocalName = "Crăciunul pe stil vechi",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "INTERNATIONALWOMENSDAY-01",
                    Date = new DateTime(year, 3, 8),
                    EnglishName = "International Women's Day",
                    LocalName = "Ziua internațională a femeii",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "MEMORIALDAY-01",
                    Date = easterSunday.AddDays(8),
                    EnglishName = "Memorial Day",
                    LocalName = "Paștele Blajinilor",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "LABOURDAY-01",
                    Date = new DateTime(year, 5, 1),
                    EnglishName = "Labour Day",
                    LocalName = "Ziua internațională a solidarității oamenilor muncii",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "VICTORYCOMMEMORATIONDAY-01",
                    Date = new DateTime(year, 5, 9),
                    EnglishName = "Victory and Commemoration Day",
                    LocalName = "Ziua Victoriei și a comemorării eroilor căzuți pentru independența Patriei",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "CHILDRENSDAY-01",
                    Date = new DateTime(year, 6, 1),
                    EnglishName = "International Children's Day",
                    LocalName = "Ziua ocrotirii copilului",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "INDEPENDENCEDAY-01",
                    Date = new DateTime(year, 8, 27),
                    EnglishName = "Independence Day",
                    LocalName = "Ziua Independenței",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "NATIONALLANGUAGEDAY-01",
                    Date = new DateTime(year, 8, 31),
                    EnglishName = "National Language Day",
                    LocalName = "Limba noastră",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "CHRISTMASDAY-01",
                    Date = new DateTime(year, 12, 25),
                    EnglishName = "Christmas Day",
                    LocalName = "Crăciunul pe stil nou",
                    HolidayTypes = HolidayTypes.Public,
                },
                this._orthodoxProvider.EasterSunday("Paștele", year),
                this._orthodoxProvider.EasterMonday("Paștele", year),
            };

            return holidaySpecifications;
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetSources()
        {
            return
            [
                "https://en.wikipedia.org/wiki/Public_holidays_in_Moldova",
                "https://www.agepi.gov.md/en/printpdf/14311"
            ];
        }
    }
}
