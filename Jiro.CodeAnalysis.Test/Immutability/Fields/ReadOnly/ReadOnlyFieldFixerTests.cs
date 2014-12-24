namespace Jiro.CodeAnalysis.Immutability.Fields.ReadOnly
{
    using Jiro.CodeAnalysis.Core;
    using Jiro.CodeAnalysis.Immutability.Fields.ReadOnly.Ceremony;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class ReadOnlyFieldFixerTests
    {
        [TestMethod]
        public async Task ReadOnlyFieldFixer_ChangesAMutableReference_ToImmutable()
        {
            var actual = await 
                WorkspaceFactory
                    .Create(@"class Class1 { private int bar; }")
                    .Setup(new ReadOnlyFieldDiagnosticAnalyzer(), new ReadOnlyFieldCodeFixProvider())
                    .ApplyFix();

            Assert.AreEqual(@"class Class1 { private readonly int bar; }", actual);
        }

        [TestMethod]
        public async Task ReadOnlyFieldFixer_LeavesReadonlyFieldsAlone()
        {
            var actual = await
                WorkspaceFactory
                    .Create(@"class Class1 { private readonly int bar; }")
                    .Setup(new ReadOnlyFieldDiagnosticAnalyzer(), new ReadOnlyFieldCodeFixProvider())
                    .ApplyFix();

            Assert.AreEqual(@"class Class1 { private readonly int bar; }", actual);
        }
    }
}