namespace Jiro.CodeAnalysis.Immutability.Properties
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Properties.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Immutable;

    [TestClass]
    public class PropertySetterAnalyzerTests
    {
        [TestMethod]
        public void PropertySetterAnalyzer_Analyze_Noops_WhenPropertyIsReadOnly()
        {
            var analyzer = new PropertySetterAnalyzer();

            var actual =
                analyzer.Analyze(
                    CreateReadOnlyProperty());

            Assert.IsTrue(actual.IsEmpty());
        }

        [TestMethod]
        public void PropertySetterAnalyzer_Analyze_HasIssue_WhenPropertyIsMutable()
        {
            var analyzer = new PropertySetterAnalyzer();

            var property = CreateMutableProperty();

            var actual = analyzer.Analyze(property);

            Assert.IsFalse(actual.IsEmpty());
            Assert.AreEqual(property.Locations[0], actual.Location);
            Assert.AreEqual(string.Format(PropertySetterDiagnostic.Descriptor.MessageFormat, property.Name), actual.GetMessage());
        }

        private static IPropertySymbol CreateReadOnlyProperty()
        {
            return CreateProperty(true);
        }

        private static IPropertySymbol CreateMutableProperty()
        {
            return CreateProperty(false);
        }

        private static IPropertySymbol CreateProperty(bool readOnly)
        {
            var result = new Mock<IPropertySymbol>();

            result.SetupGet(x => x.Name).Returns("Name");
            result.SetupGet(x => x.Locations).Returns(ImmutableArray.Create(Location.None));
            result.SetupGet(x => x.IsReadOnly).Returns(readOnly);

            return result.Object;
        }
    }
}