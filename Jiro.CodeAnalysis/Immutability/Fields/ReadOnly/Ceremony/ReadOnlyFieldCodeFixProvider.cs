namespace Jiro.CodeAnalysis.Immutability.Fields.ReadOnly.Ceremony
{
    using Jiro.CodeAnalysis.Fixing;
    using Jiro.CodeAnalysis.Immutability.Fields.ReadOnly.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    [ExportCodeFixProvider(ReadOnlyFieldDiagnostic.Id, LanguageNames.CSharp)]
    internal class ReadOnlyFieldCodeFixProvider : BaseCodeFixProvider<FieldDeclarationSyntax>
    {
        internal ReadOnlyFieldCodeFixProvider()
            : base(new DocumentRootFixContextProvider(), new ReadOnlyFieldFixer(), ReadOnlyFieldDiagnostic.Descriptors)
        {
            // noop
        }
    }
}