using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.MSTestV4;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nager.Date.ArchitectureTest.Fixtures;
using Nager.Date.ArchitectureTest.Internals;
using Nager.Date.HolidayProviders;
using Nager.Date.Models;
using System.Linq;
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

        /// <summary>
        /// A HolidayProvider that uses ObservedRuleSet must implement IObservedRuleSetHolidayProvider
        /// </summary>
        private static readonly IArchRule _observedRuleSetRequiresMarkerRule = Classes()
            .That()
            .Are(_holidayProviders)
            .And()
            .DependOnAny(typeof(ObservedRuleSet))
            .Should()
            .ImplementInterface(typeof(IObservedRuleSetHolidayProvider))
            .Because("only holiday providers marked with IObservedRuleSetHolidayProvider are allowed to use ObservedRuleSet");

        /// <summary>
        /// A class that implements IObservedRuleSetHolidayProvider must be a HolidayProvider and must use ObservedRuleSet
        /// </summary>
        private static readonly IArchRule _markerRequiresObservedRuleSetRule = Classes()
            .That()
            .ImplementInterface(typeof(IObservedRuleSetHolidayProvider))
            .Should()
            .Be(_holidayProviders)
            .AndShould()
            .DependOnAny(typeof(ObservedRuleSet))
            .Because("the IObservedRuleSetHolidayProvider marker is only allowed on holiday providers that use ObservedRuleSet");

        [TestMethod]
        public void HolidayProvider_Using_ObservedRuleSet_Should_Implement_IObservedRuleSetHolidayProvider()
        {
            _observedRuleSetRequiresMarkerRule.Check(NagerDateArchitecture.Instance);
        }

        [TestMethod]
        public void IObservedRuleSetHolidayProvider_Should_Only_Be_Implemented_By_HolidayProviders_Using_ObservedRuleSet()
        {
            _markerRequiresObservedRuleSetRule.Check(NagerDateArchitecture.Instance);
        }

        #region Self tests of the rules against fixtures

        private static Architecture LoadFixtureArchitecture()
        {
            return new ArchLoader()
                .LoadAssemblies(typeof(HolidaySystem).Assembly, typeof(MarkedHolidayProviderWithoutObservedRuleSet).Assembly)
                .Build();
        }

        private static string[] GetFixtureViolations(IArchRule rule, Architecture architecture)
        {
            var fixtureNamespace = typeof(MarkedHolidayProviderWithoutObservedRuleSet).Namespace + ".";

            return rule
                .Evaluate(architecture)
                .Where(result => !result.Passed)
                .Select(result => result.EvaluatedObjectIdentifier.Identifier)
                .Where(identifier => identifier.StartsWith(fixtureNamespace))
                .ToArray();
        }

        [TestMethod]
        public void SelfTest_Marker_Without_ObservedRuleSet_Is_Detected()
        {
            var violations = GetFixtureViolations(_markerRequiresObservedRuleSetRule, LoadFixtureArchitecture());

            CollectionAssert.AreEquivalent(new[] { typeof(MarkedHolidayProviderWithoutObservedRuleSet).FullName }, violations);
        }

        [TestMethod]
        public void SelfTest_ObservedRuleSet_Without_Marker_Is_Detected()
        {
            var violations = GetFixtureViolations(_observedRuleSetRequiresMarkerRule, LoadFixtureArchitecture());

            CollectionAssert.AreEquivalent(new[] { typeof(UnmarkedHolidayProviderWithObservedRuleSet).FullName }, violations);
        }

        #endregion
    }
}
