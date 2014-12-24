namespace Jiro.CodeAnalysis.Immutability.Fields.OnlyPrivateFieldsInStructs
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Fixing;
    using Jiro.CodeAnalysis.Immutability.Fields.OnlyPrivateFieldsInStructs.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Collections.Immutable;

    [TestClass]
    public class OnlyPrivateFieldsInStructsAnalyzerTests
    {
        private static readonly IAnalyzer<IFieldSymbol> analyzer = new OnlyPrivateFieldsInStructsAnalyzer();

        [TestMethod]
        public void OnlyPrivateFieldsInStructsAnalyzer_Noops_ForAllAccessibilities_InAReferenceType()
        {
            foreach (var accessibility in ListAccessibilities())
            {
                var actual =
                    analyzer.Analyze(
                        CreateField(Structure.Class, accessibility));

                Assert.IsTrue(actual.IsEmpty());
            }
        }

        [TestMethod]
        public void OnlyPrivateFieldsInStructsAnalyzer_Noops_ForAPrivateField_InAValueType()
        {
            var actual =
                analyzer.Analyze(
                    CreateField(Structure.Struct, Accessibility.Private));

            Assert.IsTrue(actual.IsEmpty());
        }

        [TestMethod]
        public void OnlyPrivateFieldsInStructsAnalyzer_Diagnoses_NonPrivateAccessibilities_InAValueType()
        {
            foreach (var accessibility in ListAccessibilities().Remove(Accessibility.Private))
            {
                var field = CreateField(Structure.Struct, accessibility);
                
                var actual = analyzer.Analyze(field);

                Assert.IsFalse(actual.IsEmpty());
                Assert.AreEqual(field.Locations[0], actual.Location);
                Assert.AreEqual(string.Format(OnlyPrivateInStructsDiagnostic.Descriptor.MessageFormat, field.Name), actual.GetMessage());
            }
        }

        private static ImmutableArray<Accessibility> ListAccessibilities()
        {
            var buffer = Enum.GetValues(typeof(Accessibility));

            var result = ImmutableArray.CreateBuilder<Accessibility>(buffer.Length);

            foreach (var value in buffer)
            {
                result.Add((Accessibility)value);
            }

            return result.ToImmutable();
        }

        private static IFieldSymbol CreateField(Structure structure, Accessibility accessibility)
        {
            var result = new Mock<IFieldSymbol>();

            result.SetupGet(x => x.DeclaredAccessibility).Returns(accessibility);

            var containingType = new Mock<INamedTypeSymbol>();
            containingType.Setup(x => x.IsValueType).Returns(structure == Structure.Struct);
            result.SetupGet(x => x.ContainingType).Returns(containingType.Object);

            result.SetupGet(x => x.Locations).Returns(ImmutableArray.Create(Location.None));
            result.SetupGet(x => x.Name).Returns("Name");

            return result.Object;
        }

        private enum Structure
        {
            Class,
            Struct
        }
    }
}