using Nager.Date.HolidayProviders;
using Nager.Date.Models;
using System;
using System.Collections.Generic;

namespace Nager.Date.ArchitectureTest.Fixtures
{
    /// <summary>
    /// Violation: implements the marker interface but does not use ObservedRuleSet
    /// </summary>
    internal sealed class MarkedHolidayProviderWithoutObservedRuleSet : IHolidayProvider, IObservedRuleSetHolidayProvider
    {
        public IEnumerable<Holiday> GetHolidays(int year)
        {
            return [];
        }

        public IEnumerable<string> GetSources()
        {
            return [];
        }
    }

    /// <summary>
    /// Violation: uses ObservedRuleSet but does not implement the marker interface
    /// </summary>
    internal sealed class UnmarkedHolidayProviderWithObservedRuleSet : IHolidayProvider
    {
        public IEnumerable<Holiday> GetHolidays(int year)
        {
            var observedRuleSet = new ObservedRuleSet
            {
                Sunday = date => date.AddDays(1)
            };

            var observedDate = observedRuleSet.GetObservedDate(new DateTime(year, 1, 1));
            return [];
        }

        public IEnumerable<string> GetSources()
        {
            return [];
        }
    }

    /// <summary>
    /// Valid: implements the marker interface and uses ObservedRuleSet
    /// </summary>
    internal sealed class MarkedHolidayProviderWithObservedRuleSet : IHolidayProvider, IObservedRuleSetHolidayProvider
    {
        public IEnumerable<Holiday> GetHolidays(int year)
        {
            var observedRuleSet = new ObservedRuleSet
            {
                Sunday = date => date.AddDays(1)
            };

            var observedDate = observedRuleSet.GetObservedDate(new DateTime(year, 1, 1));
            return [];
        }

        public IEnumerable<string> GetSources()
        {
            return [];
        }
    }
}
