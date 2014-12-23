namespace Jiro.CodeAnalysis.Immutability.Types.Fields.Type
{
    using Jiro.CodeAnalysis.Analyzing;
    using Microsoft.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Immutable;

    [TestClass]
    public class ImmutableFieldTypeAnalyzerTests
    {
        private static readonly IAnalyzer<IFieldSymbol> analyzer = new ImmutableFieldTypeAnalyzer();

        [TestMethod]
        public void ImmutableFieldTypeAnalyzer_Diagnoses_Arrays()
        {
            var actual =
                analyzer.Analyze(
                    CreateField(TypeKind.Array));

            Assert.IsFalse(actual.IsEmpty());
        }

        private static IFieldSymbol CreateField(TypeKind typeKind)
        {
            var result = new Mock<IFieldSymbol>();

            result.SetupGet(x => x.Name).Returns("Name");
            result.SetupGet(x => x.Locations).Returns(ImmutableArray.Create(Location.None));

            var type = new Mock<ITypeSymbol>();

            type.Setup(x => x.TypeKind).Returns(typeKind);

            result.SetupGet(x => x.Type).Returns(type.Object);

            return result.Object;
        }
    }
}