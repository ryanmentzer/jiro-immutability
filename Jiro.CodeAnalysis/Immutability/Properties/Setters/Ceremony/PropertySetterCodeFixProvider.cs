namespace Jiro.CodeAnalysis.Immutability.Properties.Setters.Ceremony
{
    using Jiro.CodeAnalysis.Fixing;
    using Jiro.CodeAnalysis.Immutability.Properties.Setters.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    [ExportCodeFixProvider(PropertySetterDiagnostic.Id, LanguageNames.CSharp)]
    internal class PropertySetterCodeFixProvider : BaseCodeFixProvider<PropertyDeclarationSyntax>
    {
        internal PropertySetterCodeFixProvider()
            : base(new DocumentRootFixContextProvider(), new PropertySetterFixer(), PropertySetterDiagnostic.Descriptors)
        {
            // noop
        }
    }
}