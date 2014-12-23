namespace Jiro.CodeAnalysis.Immutability.Types.Fields.Type
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Core;
    using Jiro.CodeAnalysis.Immutability.Types.Fields.Type.Ceremony;
    using Microsoft.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Immutable;
    using System.Threading.Tasks;

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

        [TestMethod]
        public async Task ImmutableFieldTypeAnalyzer_Diagnoses_GenericLists()
        {
            var actual = await
                WorkspaceFactory
                    .Create(@"namespace Tests { using System.Collections.Generic; class Class1 { private List<int> foo; } }")
                    .Setup(new ImmutableFieldTypeDiagnosticAnalyzer())
                    .Diagnose();
            
            Assert.IsTrue(actual.Length > 0);
        }

        [TestMethod]
        public async Task ImmutableFieldTypeAnalyzer_Noops_ForAKnownImmutableType()
        {
            var actual = await
                WorkspaceFactory
                    .Create(@"namespace Tests { using class Class1 { private in foo; } }")
                    .Setup(new ImmutableFieldTypeDiagnosticAnalyzer())
                    .Diagnose();

            Assert.IsTrue(actual.Length == 0);
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