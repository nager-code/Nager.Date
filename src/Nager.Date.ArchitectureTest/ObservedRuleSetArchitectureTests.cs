using ArchUnitNET.Domain;
using ArchUnitNET.MSTestV4;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nager.Date.ArchitectureTest.Internals;
using Nager.Date.HolidayProviders;
using Nager.Date.Models;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Nager.Date.ArchitectureTest
{
    [TestClass]
    public class ObservedRuleSetArchitectureTests
    {
        private static readonly IObjectProvider<Class> _holidayProviders = Classes()
            .That()
            .AreAssignableTo(typeof(IHolidayProvider))
            .As("Nager.Date HolidayProvider");

        [TestMethod]
        public void HolidayProvider_Using_ObservedRuleSet_Should_Implement_IObservedRuleSetHolidayProvider()
        {
            var rule = Classes()
                .That()
                .Are(_holidayProviders)
                .And()
                .DependOnAny(typeof(ObservedRuleSet))
                .Should()
                .ImplementInterface(typeof(IObservedRuleSetHolidayProvider))
                .Because("only holiday providers marked with IObservedRuleSetHolidayProvider are allowed to use ObservedRuleSet");

            rule.Check(NagerDateArchitecture.Instance);
        }

        [TestMethod]
        public void IObservedRuleSetHolidayProvider_Should_Only_Be_Implemented_By_HolidayProviders_Using_ObservedRuleSet()
        {
            var rule = Classes()
                .That()
                .ImplementInterface(typeof(IObservedRuleSetHolidayProvider))
                .Should()
                .Be(_holidayProviders)
                .AndShould()
                .DependOnAny(typeof(ObservedRuleSet))
                .Because("the IObservedRuleSetHolidayProvider marker should be removed when a holiday provider no longer uses ObservedRuleSet");

            rule.Check(NagerDateArchitecture.Instance);
        }
    }
}
