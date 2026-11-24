using Nager.Date.Extensions;
using Nager.Date.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Nager.Date.HolidayProviders
{
    /// <summary>
    /// South Korea HolidayProvider
    /// </summary>
    internal sealed class SouthKoreaHolidayProvider : AbstractHolidayProvider,
        ISubdivisionCodesProvider,
        IObservedRuleSetHolidayProvider
    {
        /// <summary>
        /// South Korea HolidayProvider
        /// </summary>
        public SouthKoreaHolidayProvider() : base(CountryCode.KR)
        {
        }

        /// <inheritdoc/>
        public IDictionary<string, string> GetSubdivisionCodes()
        {
            return new Dictionary<string, string>
            {
                { "KR-11", "Seoul-teukbyeolsi" },
                { "KR-26", "Busan-gwangyeoksi" },
                { "KR-27", "Daegu-gwangyeoksi" },
                { "KR-28", "Incheon-gwangyeoksi" },
                { "KR-29", "Gwangju-gwangyeoksi" },
                { "KR-30", "Daejeon-gwangyeoksi" },
                { "KR-31", "Ulsan-gwangyeoksi" },
                { "KR-41", "Gyeonggi-do" },
                { "KR-43", "Chungcheongbuk-do" },
                { "KR-44", "Chungcheongnam-do" },
                { "KR-46", "Jeollanam-do" },
                { "KR-47", "Gyeongsangbuk-do" },
                { "KR-48", "Gyeongsangnam-do" },
                { "KR-49", "Jeju-teukbyeoljachido" },
                { "KR-50", "Sejong" },
                { "KR-51", "Gangwon-teukbyeoljachido" },
                { "KR-52", "Jeonbuk-teukbyeoljachido" },
            };
        }

        /// <inheritdoc/>
        protected override IEnumerable<HolidaySpecification> GetHolidaySpecifications(int year)
        {
            var weekendObservedRuleSet = new ObservedRuleSet
            {
                Saturday = date => date.AddDays(2),
                Sunday = date => date.AddDays(1),
            };

            var holidaySpecifications = new List<HolidaySpecification>
            {
                new HolidaySpecification
                {
                    Id = "NEWYEARSDAY-01",
                    Date = new DateTime(year, 1, 1),
                    EnglishName = "New Year's Day",
                    LocalName = "새해",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "CHILDRENSDAY-01",
                    Date = new DateTime(year, 5, 5),
                    EnglishName = "Children's Day",
                    LocalName = "어린이날",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = weekendObservedRuleSet,
                },
                new HolidaySpecification
                {
                    Id = "MEMORIALDAY-01",
                    Date = new DateTime(year, 6, 6),
                    EnglishName = "Memorial Day",
                    LocalName = "현충일",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "CHRISTMASDAY-01",
                    Date = new DateTime(year, 12, 25),
                    EnglishName = "Christmas Day",
                    LocalName = "크리스마스",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = year >= 2023 ? weekendObservedRuleSet : null,
                }
            };

            holidaySpecifications.AddIfNotNull(this.LabourDay(year, weekendObservedRuleSet));
            holidaySpecifications.AddIfNotNull(this.ConstitutionDay(year, weekendObservedRuleSet));
            holidaySpecifications.AddIfNotNull(this.IndependenceMovementDay(year, weekendObservedRuleSet));
            holidaySpecifications.AddIfNotNull(this.LiberationDay(year, weekendObservedRuleSet));
            holidaySpecifications.AddIfNotNull(this.NationalFoundationDay(year, weekendObservedRuleSet));
            holidaySpecifications.AddIfNotNull(this.HangulDay(year, weekendObservedRuleSet));

            //Order matters: the substitute holidays of the lunisolar holidays depend on the dates of all
            //solar calendar holidays, add every new solar calendar holiday above this line.
            //Election days and temporary public holidays are added afterwards and are not considered.
            var solarPublicHolidayDates = this.GetPublicHolidayDates(holidaySpecifications);
            holidaySpecifications.AddRangeIfNotNull(this.GetKoreanLunisolarHolidays(year, solarPublicHolidayDates));

            holidaySpecifications.AddIfNotNull(this.PresidentialElectionDay(year));
            holidaySpecifications.AddIfNotNull(this.NationalAssemblyElectionDay(year));
            holidaySpecifications.AddIfNotNull(this.LocalElectionDay(year));
            holidaySpecifications.AddRangeIfNotNull(this.TemporaryPublicHolidays(year));

            return holidaySpecifications;
        }

        /// <summary>
        /// Temporary public holidays (임시공휴일), designated individually by the government
        /// </summary>
        private HolidaySpecification[] TemporaryPublicHolidays(int year)
        {
            //The position within a year defines the Id (TEMPORARYPUBLICHOLIDAY-01, -02, ...),
            //only append new entries of a year at the end, otherwise the existing Ids change
            (DateTime Date, string EnglishName, string[]? SubdivisionCodes)[] items = year switch
            {
                //Cabinet decision 1980-10-10 (National Archives of Korea)
                1980 => [(new DateTime(1980, 10, 22), "Constitutional Referendum Day", null)],
                //Cabinet decision 1988-08-11 (National Archives of Korea)
                1988 => [(new DateTime(1988, 9, 17), "Seoul Olympics Opening Day", null)],
                //행정자치부공고 제2002-62호
                2002 => [(new DateTime(2002, 7, 1), "FIFA World Cup Celebration Day", null)],
                //Regional temporary public holidays, 행정자치부공고 제2005-122호 and 제2005-17호
                2005 => [
                    (new DateTime(2005, 7, 27), "Residents' Referendum Day", ["KR-49"]),
                    (new DateTime(2005, 11, 18), "APEC Summit Day", ["KR-26"])
                ],
                //인사혁신처공고 제2015-358호
                2015 => [(new DateTime(2015, 8, 14), "Temporary Public Holiday", null)],
                //인사혁신처공고 제2016-218호
                2016 => [(new DateTime(2016, 5, 6), "Temporary Public Holiday", null)],
                //인사혁신처공고 제2017-299호
                2017 => [(new DateTime(2017, 10, 2), "Temporary Public Holiday", null)],
                //인사혁신처공고 제2020-378호
                2020 => [(new DateTime(2020, 8, 17), "Temporary Public Holiday", null)],
                //인사혁신처공고 제2023-400호
                2023 => [(new DateTime(2023, 10, 2), "Temporary Public Holiday", null)],
                //인사혁신처공고 제2024-309호
                2024 => [(new DateTime(2024, 10, 1), "Armed Forces Day", null)],
                //인사혁신처공고 제2025-28호
                2025 => [(new DateTime(2025, 1, 27), "Temporary Public Holiday", null)],
                _ => []
            };

            var holidaySpecifications = new List<HolidaySpecification>();

            for (var i = 0; i < items.Length; i++)
            {
                holidaySpecifications.Add(new HolidaySpecification
                {
                    Id = $"TEMPORARYPUBLICHOLIDAY-{i + 1:00}",
                    Date = items[i].Date,
                    EnglishName = items[i].EnglishName,
                    LocalName = "임시공휴일",
                    HolidayTypes = HolidayTypes.Public,
                    SubdivisionCodes = items[i].SubdivisionCodes,
                });
            }

            return [.. holidaySpecifications];
        }

        // Election days
        // Until 2006 every election day was designated individually as a temporary public holiday (관보 notices),
        // since 2006-09-06 the election day of a term-expiration election is a public holiday by the
        // Regulation on Holidays of Government Offices, Art. 2 No. 10-2 (Presidential Decree No. 19674).
        // Special elections (e.g. 2017, 2025) are still designated individually as temporary public holidays.
        // Election days before 2002 are not included, no official source has been found yet.

        private HolidaySpecification? PresidentialElectionDay(int year)
        {
            DateTime? date = year switch
            {
                2002 => new DateTime(2002, 12, 19), //Temporary public holiday, 행정자치부공고 제2002-150호
                2007 => new DateTime(2007, 12, 19),
                2012 => new DateTime(2012, 12, 19),
                2017 => new DateTime(2017, 5, 9), //Special election, temporary public holiday, 인사혁신처공고 제2017-90호
                2022 => new DateTime(2022, 3, 9),
                2025 => new DateTime(2025, 6, 3), //Special election, temporary public holiday, 인사혁신처공고 제2025-133호
                _ => null
            };

            if (date is null)
            {
                return null;
            }

            return new HolidaySpecification
            {
                Id = "PRESIDENTIALELECTIONDAY-01",
                Date = date.Value,
                EnglishName = "Presidential Election Day",
                LocalName = "대통령 선거일",
                HolidayTypes = HolidayTypes.Public,
            };
        }

        private HolidaySpecification? NationalAssemblyElectionDay(int year)
        {
            DateTime? date = year switch
            {
                2004 => new DateTime(2004, 4, 15), //Temporary public holiday, 행정자치부공고 제2004-47호
                2008 => new DateTime(2008, 4, 9),
                2012 => new DateTime(2012, 4, 11),
                2016 => new DateTime(2016, 4, 13),
                2020 => new DateTime(2020, 4, 15),
                2024 => new DateTime(2024, 4, 10),
                _ => null
            };

            if (date is null)
            {
                return null;
            }

            return new HolidaySpecification
            {
                Id = "NATIONALASSEMBLYELECTIONDAY-01",
                Date = date.Value,
                EnglishName = "National Assembly Election Day",
                LocalName = "국회의원 선거일",
                HolidayTypes = HolidayTypes.Public,
            };
        }

        private HolidaySpecification? LocalElectionDay(int year)
        {
            DateTime? date = year switch
            {
                2002 => new DateTime(2002, 6, 13), //Temporary public holiday, 행정자치부공고 제2002-46호
                2006 => new DateTime(2006, 5, 31), //Temporary public holiday, 행정자치부공고 제2006-68호
                2010 => new DateTime(2010, 6, 2),
                2014 => new DateTime(2014, 6, 4),
                2018 => new DateTime(2018, 6, 13),
                2022 => new DateTime(2022, 6, 1),
                2026 => new DateTime(2026, 6, 3),
                _ => null
            };

            if (date is null)
            {
                return null;
            }

            return new HolidaySpecification
            {
                Id = "LOCALELECTIONDAY-01",
                Date = date.Value,
                EnglishName = "Local Election Day",
                LocalName = "지방 선거일",
                HolidayTypes = HolidayTypes.Public,
            };
        }

        private HolidaySpecification IndependenceMovementDay(
            int year,
            ObservedRuleSet observedRuleSet)
        {
            var holidayObservedRuleSet = observedRuleSet;

            if (year < 2022)
            {
                holidayObservedRuleSet = null;
            }

            return new HolidaySpecification
            {
                Id = "INDEPENDENCEMOVEMENTDAY-01",
                Date = new DateTime(year, 3, 1),
                EnglishName = "Independence Movement Day",
                LocalName = "3·1절",
                HolidayTypes = HolidayTypes.Public,
                ObservedRuleSet = holidayObservedRuleSet
            };
        }

        private HolidaySpecification LiberationDay(
            int year,
            ObservedRuleSet observedRuleSet)
        {
            var holidayObservedRuleSet = observedRuleSet;

            if (year < 2022)
            {
                holidayObservedRuleSet = null;
            }

            return new HolidaySpecification
            {
                Id = "LIBERATIONDAY-01",
                Date = new DateTime(year, 8, 15),
                EnglishName = "Liberation Day",
                LocalName = "광복절",
                HolidayTypes = HolidayTypes.Public,
                ObservedRuleSet = holidayObservedRuleSet
            };
        }

        private HolidaySpecification NationalFoundationDay(
            int year,
            ObservedRuleSet observedRuleSet)
        {
            var holidayObservedRuleSet = observedRuleSet;

            if (year < 2022)
            {
                holidayObservedRuleSet = null;
            }

            return new HolidaySpecification
            {
                Id = "NATIONALFOUNDATIONDAY-01",
                Date = new DateTime(year, 10, 3),
                EnglishName = "National Foundation Day",
                LocalName = "개천절",
                HolidayTypes = HolidayTypes.Public,
                ObservedRuleSet = holidayObservedRuleSet
            };
        }

        private HolidaySpecification HangulDay(
            int year,
            ObservedRuleSet observedRuleSet)
        {
            var holidayObservedRuleSet = observedRuleSet;

            if (year < 2022)
            {
                holidayObservedRuleSet = null;
            }

            return new HolidaySpecification
            {
                Id = "HANGULDAY-01",
                Date = new DateTime(year, 10, 9),
                EnglishName = "Hangul Day",
                LocalName = "한글날",
                HolidayTypes = HolidayTypes.Public,
                ObservedRuleSet = holidayObservedRuleSet
            };
        }

        private HolidaySpecification? LabourDay(
            int year,
            ObservedRuleSet observedRuleSet)
        {
            if (year < 2026)
            {
                return null;
            }

            return new HolidaySpecification
            {
                Id = "LABOURDAY-01",
                Date = new DateTime(year, 5, 1),
                EnglishName = "Labour Day",
                LocalName = "노동절",
                HolidayTypes = HolidayTypes.Public,
                ObservedRuleSet = observedRuleSet
            };
        }

        private HolidaySpecification ConstitutionDay(
            int year,
            ObservedRuleSet observedRuleSet)
        {
            var holidayTypes = HolidayTypes.Public;

            if (year >= 2008 && year < 2026)
            {
                holidayTypes = HolidayTypes.Observance;
            }

            return new HolidaySpecification
            {
                Id = "CONSTITUTIONDAY-01",
                Date = new DateTime(year, 7, 17),
                EnglishName = "Constitution Day",
                LocalName = "제헌절",
                HolidayTypes = holidayTypes,
                ObservedRuleSet = observedRuleSet
            };
        }

        /// <summary>
        /// Collects the dates (including substitute dates) of the given public holidays
        /// </summary>
        private HashSet<DateTime> GetPublicHolidayDates(IEnumerable<HolidaySpecification> holidaySpecifications)
        {
            var holidayDates = new HashSet<DateTime>();

            foreach (var holidaySpecification in holidaySpecifications)
            {
                if (!holidaySpecification.HolidayTypes.HasFlag(HolidayTypes.Public))
                {
                    continue;
                }

                holidayDates.Add(holidaySpecification.Date);

                var observedDate = holidaySpecification.ObservedRuleSet?.GetObservedDate(holidaySpecification.Date);
                if (observedDate.HasValue)
                {
                    holidayDates.Add(observedDate.Value);
                }
            }

            return holidayDates;
        }

        /// <summary>
        /// Gets the first day on or after the given date that is neither a Saturday, a Sunday nor one of the given holidays
        /// </summary>
        private DateTime GetNextNonHoliday(DateTime date, HashSet<DateTime> holidayDates)
        {
            while (date.DayOfWeek == DayOfWeek.Saturday ||
                date.DayOfWeek == DayOfWeek.Sunday ||
                holidayDates.Contains(date))
            {
                date = date.AddDays(1);
            }

            return date;
        }

        /// <summary>
        /// Creates a rule set that applies the same rule on every day of the week
        /// </summary>
        private ObservedRuleSet GetObservedRuleSet(Func<DateTime, DateTime> getObservedDate)
        {
            return new ObservedRuleSet
            {
                Monday = getObservedDate,
                Tuesday = getObservedDate,
                Wednesday = getObservedDate,
                Thursday = getObservedDate,
                Friday = getObservedDate,
                Saturday = getObservedDate,
                Sunday = getObservedDate,
            };
        }

        /// <summary>
        /// Substitute holiday: if the holiday falls on a Saturday, a Sunday or another public holiday,
        /// the first following day that is neither a weekend day nor a public holiday is a holiday
        /// </summary>
        private ObservedRuleSet GetSubstituteHolidayObservedRuleSet(HashSet<DateTime> otherHolidayDates)
        {
            return this.GetObservedRuleSet(date => this.GetNextNonHoliday(date, otherHolidayDates));
        }

        /// <summary>
        /// Substitute holidays of a holiday period of consecutive days (Seollal, Chuseok): every day that falls on a Sunday
        /// or another public holiday is substituted by the first day after it that is neither a weekend day,
        /// a public holiday, a day of the holiday period nor an already assigned substitute holiday
        /// </summary>
        /// <returns>The rule set for each given date, <see langword="null"/> if the day has no substitute holiday</returns>
        private ObservedRuleSet?[] GetHolidayPeriodObservedRuleSets(DateTime[] dates, HashSet<DateTime> otherHolidayDates)
        {
            var holidayDates = new HashSet<DateTime>(otherHolidayDates);
            holidayDates.UnionWith(dates);

            var observedRuleSets = new ObservedRuleSet?[dates.Length];

            for (var i = 0; i < dates.Length; i++)
            {
                var date = dates[i];
                if (date.DayOfWeek != DayOfWeek.Sunday && !otherHolidayDates.Contains(date))
                {
                    continue;
                }

                var substituteDate = this.GetNextNonHoliday(date.AddDays(1), holidayDates);
                holidayDates.Add(substituteDate);

                observedRuleSets[i] = this.GetObservedRuleSet(_ => substituteDate);
            }

            return observedRuleSets;
        }

        private HolidaySpecification[] GetKoreanLunisolarHolidays(int year, HashSet<DateTime> solarPublicHolidayDates)
        {
            var holidaySpecifications = new List<HolidaySpecification>();

            var koreanCalendar = new KoreanLunisolarCalendar();
            if (year >= koreanCalendar.MinSupportedDateTime.Year && year < koreanCalendar.MaxSupportedDateTime.Year)
            {
                var leapMonth = koreanCalendar.GetLeapMonth(year);

                var lunarNewYear1 = koreanCalendar.ToDateTime(year, this.MoveMonth(1, leapMonth), 1, 0, 0, 0, 0);
                var lunarNewYear2 = lunarNewYear1.AddDays(+1); //The day after Seollal
                var lunarNewYear3 = lunarNewYear1.AddDays(-1); //The day before Seollal

                var buddhaBday = koreanCalendar.ToDateTime(year, this.MoveMonth(4, leapMonth), 8, 0, 0, 0, 0);

                var chuseok1 = koreanCalendar.ToDateTime(year, this.MoveMonth(8, leapMonth), 14, 0, 0, 0, 0);
                var chuseok2 = chuseok1.AddDays(+1);
                var chuseok3 = chuseok2.AddDays(+1);

                //The dates stay on the days of the lunisolar calendar, a substitute holiday only changes the observed date.
                //Regulation on Holidays of Government Offices, Art. 3 (added by Presidential Decree No. 24828 of
                //November 2013, first applied in 2014): a Seollal or Chuseok day on a Sunday or another public holiday
                //is substituted by the next non-holiday (e.g. 2016, the day before Seollal on Sunday Feb 7 ->
                //substitute holiday on Feb 10, 2017 and 2028, Chuseok on National Foundation Day).
                //Before 2014 there was no substitute holiday for Seollal and Chuseok, the next-day rule of 1989 and 1990
                //(Presidential Decree No. 12616) did not apply to the holiday periods (연휴외의 공휴일).
                var lunarNewYearObservedRuleSets = new ObservedRuleSet?[3];
                var chuseokObservedRuleSets = new ObservedRuleSet?[3];

                if (year >= 2014)
                {
                    lunarNewYearObservedRuleSets = this.GetHolidayPeriodObservedRuleSets(
                        [lunarNewYear3, lunarNewYear1, lunarNewYear2],
                        solarPublicHolidayDates);

                    chuseokObservedRuleSets = this.GetHolidayPeriodObservedRuleSets(
                        [chuseok1, chuseok2, chuseok3],
                        solarPublicHolidayDates);
                }

                holidaySpecifications.Add(new HolidaySpecification
                {
                    Id = "LUNARNEWYEAR-01",
                    Date = lunarNewYear1,
                    EnglishName = "Lunar New Year",
                    LocalName = "설날",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = lunarNewYearObservedRuleSets[1],
                });
                holidaySpecifications.Add(new HolidaySpecification
                {
                    Id = "LUNARNEWYEAR-02",
                    Date = lunarNewYear2,
                    EnglishName = "Lunar New Year",
                    LocalName = "설날",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = lunarNewYearObservedRuleSets[2],
                });
                holidaySpecifications.Add(new HolidaySpecification
                {
                    Id = "LUNARNEWYEAR-03",
                    Date = lunarNewYear3,
                    EnglishName = "Lunar New Year",
                    LocalName = "설날",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = lunarNewYearObservedRuleSets[0],
                });
                holidaySpecifications.Add(new HolidaySpecification
                {
                    Id = "BUDDHASBIRTHDAY-01",
                    Date = buddhaBday,
                    EnglishName = "Buddha's Birthday",
                    LocalName = "부처님 오신 날",
                    HolidayTypes = HolidayTypes.Public,
                    //Regulation on Holidays of Government Offices, amendment of May 2023:
                    //substitute holiday also if Buddha's Birthday falls on a weekend or another public holiday
                    //(e.g. 2025, Buddha's Birthday and Children's Day on May 5 -> substitute holiday on May 6)
                    ObservedRuleSet = year >= 2023 ? this.GetSubstituteHolidayObservedRuleSet(solarPublicHolidayDates) : null
                });
                holidaySpecifications.Add(new HolidaySpecification
                {
                    Id = "CHUSEOK-01",
                    Date = chuseok1,
                    EnglishName = "Chuseok",
                    LocalName = "추석",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = chuseokObservedRuleSets[0],
                });
                holidaySpecifications.Add(new HolidaySpecification
                {
                    Id = "CHUSEOK-02",
                    Date = chuseok2,
                    EnglishName = "Chuseok",
                    LocalName = "추석",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = chuseokObservedRuleSets[1],
                });
                holidaySpecifications.Add(new HolidaySpecification
                {
                    Id = "CHUSEOK-03",
                    Date = chuseok3,
                    EnglishName = "Chuseok",
                    LocalName = "추석",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = chuseokObservedRuleSets[2],
                });
            }

            return [.. holidaySpecifications];
        }

        private int MoveMonth(int month, int leapMonth)
        {
            if (leapMonth == 0)
            {
                return month;
            }

            if (leapMonth < month)
            {
                return ++month;
            }

            return month;
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetSources()
        {
            return
            [
                "https://en.wikipedia.org/wiki/Public_holidays_in_South_Korea", //South Korea's public holidays
                "https://www.law.go.kr/lsInfoP.do?lsiSeq=233829#0000",
                "https://m.korea.kr/news/policyNewsView.do?newsId=148912746", //Substitute holiday for Buddha's Birthday and Christmas Day (2023)
                "https://astro.kasi.re.kr/life/post/calendarData?year=2025", //Substitute holiday for Buddha's Birthday on May 6 (2025)
                "https://news.seoul.go.kr/gov/?p=61716", //Substitute holiday for Seollal, Chuseok and Children's Day (2013)
                "https://m.korea.kr/news/issueQAView.do?newsId=148842064", //Chuseok holidays from September 30 to October 9 (2017)
                "https://astro.kasi.re.kr/life/post/calendarData?year=2028", //Substitute holiday for Chuseok on October 5 (2028)
                "https://www.law.go.kr/LSW/lsInfoP.do?lsiSeq=145846", //Substitute holiday for Seollal and Chuseok on a Sunday or another public holiday, Art. 3 (2013)
                "https://www.law.go.kr/LSW/lsRvsRsnListP.do?lsId=002404&chrClsCd=010102", //Reasons of all amendments of the Regulation on Holidays of Government Offices (1985-2026)
                "https://www.law.go.kr/LSW/lsInfoP.do?lsiSeq=10406", //Next-day rule only for holidays outside of the holiday periods (1989-1990)
                "https://www.kasi.re.kr/kor/publication/post/newsMaterial/2536", //Seollal holidays from February 7 to 10 including the substitute holiday (2016)
                "https://astro.kasi.re.kr/life/post/calendarData?year=2027", //Substitute holiday for Seollal on February 9 (2027)
                "https://www.law.go.kr/LSW/lsInfoR.do?lsiSeq=75972&efYd=20060906&chrClsCd=010202", //Election days of term-expiration elections are public holidays (2006)
                "https://info.nec.go.kr/main/main_previous_load.xhtml", //Election dates (2006-2025)
                "https://www.korea.kr/news/policyNewsView.do?newsId=148830297", //Presidential Election Day, temporary public holiday (2017)
                "https://www.korea.kr/news/policyNewsView.do?newsId=148941443", //Presidential Election Day, temporary public holiday (2025)
                "https://theme.archives.go.kr/viewer/common/archWebViewer.do?singleData=Y&archiveEventId=0028757373", //Temporary public holiday, constitutional referendum (1980)
                "https://theme.archives.go.kr/viewer/common/archWebViewer.do?singleData=Y&archiveEventId=0028780272", //Temporary public holiday, Seoul Olympics opening (1988)
                "https://theme.archives.go.kr/next/chronology/yearRecord.do?year=2002&month=7&day=1", //Temporary public holiday, FIFA World Cup (2002)
                "https://www.korea.kr/news/policyNewsView.do?newsId=148798912", //Temporary public holiday (2015)
                "https://www.korea.kr/news/policyNewsView.do?newsId=148813106", //Temporary public holiday (2016)
                "https://www.korea.kr/news/policyNewsView.do?newsId=148874895", //Temporary public holiday (2020)
                "https://www.korea.kr/briefing/pressReleaseView.do?newsId=156588468", //Temporary public holiday (2023)
                "https://www.korea.kr/briefing/pressReleaseView.do?newsId=156648653", //Temporary public holiday, Armed Forces Day (2024)
                "https://www.korea.kr/briefing/pressReleaseView.do?newsId=156670135", //Temporary public holiday (2025)
            ];
        }
    }
}
