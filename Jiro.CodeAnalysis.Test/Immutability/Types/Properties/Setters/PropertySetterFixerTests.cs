namespace Jiro.CodeAnalysis.Immutablility.Types.Properties.Setters
{
    using Jiro.CodeAnalysis.Core;
    using Jiro.CodeAnalysis.Immutability.Properties.Setters.Ceremony;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class PropertySetterFixerTests
    {
        [TestMethod]
        public async Task PropertySetterFixer_ForAutmaticProperties_ChangesMutable_ToImmutable()
        {
            var actual = await
                WorkspaceFactory
                    .Create(@"class Class1 { public string Name { get; set; } }")
                    .Setup(new PropertySetterDiagnosticAnalyzer(), new PropertySetterCodeFixProvider())
                    .ApplyFix();

            Assert.AreEqual(@"class Class1 { public string Name { get; } }", actual);
        }

        [TestMethod]
        public async Task PropertySetterFixer_ForAutmaticProperties_LeavesReadonlyIntact()
        {
            var actual = await
                WorkspaceFactory
                    .Create(@"class Class1 { public string Name { get; } }")
                    .Setup(new PropertySetterDiagnosticAnalyzer(), new PropertySetterCodeFixProvider())
                    .ApplyFix();

            Assert.AreEqual(@"class Class1 { public string Name { get; } }", actual);
        }

        [TestMethod]
        public async Task PropertySetterFixer_ForStandardProperties_ChangesMutable_ToImmutable()
        {
            var actual = await
                WorkspaceFactory
                    .Create(@"class Class1 { private string name; public string Name { get { return this.name; } set { this.name = value; } } }")
                    .Setup(new PropertySetterDiagnosticAnalyzer(), new PropertySetterCodeFixProvider())
                    .ApplyFix();

            Assert.AreEqual(@"class Class1 { private string name; public string Name { get { return this.name; } } }", actual);
        }

        [TestMethod]
        public async Task PropertySetterFixer_ForStandardProperties_LeavesReadonlyIntact()
        {
            var expected = @"class Class1 { private string name; public string Name { get { return this.name; } } }";

            var actual = await
                WorkspaceFactory
                    .Create(expected)
                    .Setup(new PropertySetterDiagnosticAnalyzer(), new PropertySetterCodeFixProvider())
                    .ApplyFix();

            Assert.AreEqual(expected, actual);
        }
    }    
}