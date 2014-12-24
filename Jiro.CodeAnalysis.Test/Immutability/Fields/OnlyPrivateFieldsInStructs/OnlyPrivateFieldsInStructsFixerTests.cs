namespace Jiro.CodeAnalysis.Immutability.Fields.OnlyPrivateFieldsInStructs
{
    using Jiro.CodeAnalysis.Core;
    using Jiro.CodeAnalysis.Immutability.Fields.OnlyPrivateFieldsInStructs.Ceremony;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class OnlyPrivateFieldsInStructsFixerTests
    {
        [TestMethod]
        public async Task OnlyPrivateFieldsInStructsFixer_Converts_NonPrivateFields_ToPrivate()
        {
            var actual = await
                WorkspaceFactory
                    .Create(@"struct Struct1 { public int bar; }")
                    .Setup(new OnlyPrivateFieldsInStructsDiagnosticAnalyzer(), new OnlyPrivateFieldsInStructsCodeFixProvider())
                    .ApplyFix();

            Assert.AreEqual(@"struct Struct1 { private int bar; }", actual);
        }

        [TestMethod]
        public async Task OnlyPrivateFieldsInStructsFixer_DoesNotProduce_MultipleAccessModifiers()
        {
            var actual = await
                WorkspaceFactory
                    .Create(@"struct Struct1 { public private int bar; }")
                    .Setup(new OnlyPrivateFieldsInStructsDiagnosticAnalyzer(), new OnlyPrivateFieldsInStructsCodeFixProvider())
                    .ApplyFix();

            Assert.AreEqual(@"struct Struct1 { private int bar; }", actual);
        }

        [TestMethod]
        public async Task OnlyPrivateFieldsInStructsFixer_Preserves_NonAccessModifiers()
        {
            var actual = await
                WorkspaceFactory
                    .Create(@"struct Struct1 { public readonly int bar; }")
                    .Setup(new OnlyPrivateFieldsInStructsDiagnosticAnalyzer(), new OnlyPrivateFieldsInStructsCodeFixProvider())
                    .ApplyFix();

            Assert.AreEqual(@"struct Struct1 { private readonly int bar; }", actual);
        }
    }
}