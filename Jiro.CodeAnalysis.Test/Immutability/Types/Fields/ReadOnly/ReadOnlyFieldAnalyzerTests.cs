namespace Jiro.CodeAnalysis.Immutability.Fields.ReadOnly
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Fields.ReadOnly.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Immutable;

    [TestClass]
    public class ReadOnlyFieldAnalyzerTests
    {
        [TestMethod]
        public void ReadOnlyFieldAnalyzer_Analyze_Noops_WhenFieldIsReadOnly()
        {
            var analyzer = new ReadOnlyFieldAnalyzer();

            var actual = 
                analyzer.Analyze(
                    CreateFieldWithReadOnlyModifier());

            Assert.IsTrue(actual.IsEmpty());
        }

        [TestMethod]
        public void ReadOnlyFieldAnalyzer_Analyze_HasIssue_WhenFieldHasMutableReference()
        {
            var analyzer = new ReadOnlyFieldAnalyzer();

            var field = CreateFieldWithoutReadOnlyModifier();

            var actual = 
                analyzer.Analyze(
                    CreateFieldWithoutReadOnlyModifier());

            Assert.IsFalse(actual.IsEmpty());
            Assert.AreEqual(field.Locations[0], actual.Location);
            Assert.AreEqual(string.Format(ReadOnlyFieldDiagnostic.Descriptor.MessageFormat, field.Name), actual.GetMessage());
        }

        private static IFieldSymbol CreateFieldWithoutReadOnlyModifier()
        {
            return CreateField(false);
        }

        private static IFieldSymbol CreateFieldWithReadOnlyModifier()
        {
            return CreateField(true);
        }
    
        private static IFieldSymbol CreateField(bool readOnly)
        {
            var result = new Mock<IFieldSymbol>();

            result.SetupGet(x => x.Name).Returns("Name");
            result.SetupGet(x => x.Locations).Returns(ImmutableArray.Create(Location.None));
            result.SetupGet(x => x.IsReadOnly).Returns(readOnly);

            return result.Object;
        }
    }
}