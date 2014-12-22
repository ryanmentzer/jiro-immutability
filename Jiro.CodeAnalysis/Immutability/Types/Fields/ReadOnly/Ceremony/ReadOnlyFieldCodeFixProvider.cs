namespace Jiro.CodeAnalysis.Immutability.Types.Fields.ReadOnly.Ceremony
{
    using Jiro.CodeAnalysis.Fixing;
    using Jiro.CodeAnalysis.Immutability.Types.Fields.ReadOnly.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    [ExportCodeFixProvider(ReadOnlyFieldDiagnostic.Id, LanguageNames.CSharp)]
    public class ReadOnlyFieldCodeFixProvider : BaseCodeFixProvider<FieldDeclarationSyntax>
    {
        public ReadOnlyFieldCodeFixProvider()
            : base(new DocumentRootFixContextProvider(), new ReadOnlyFieldFixer(), ReadOnlyFieldDiagnostic.Descriptors)
        {
            // noop
        }
    }
}