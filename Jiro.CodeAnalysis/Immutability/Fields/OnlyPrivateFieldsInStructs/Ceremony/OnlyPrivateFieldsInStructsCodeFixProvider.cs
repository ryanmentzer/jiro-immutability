namespace Jiro.CodeAnalysis.Immutability.Fields.OnlyPrivateFieldsInStructs.Ceremony
{
    using Jiro.CodeAnalysis.Fixing;
    using Jiro.CodeAnalysis.Immutability.Fields.OnlyPrivateFieldsInStructs.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    [ExportCodeFixProvider(OnlyPrivateInStructsDiagnostic.Id, LanguageNames.CSharp)]
    internal class OnlyPrivateFieldsInStructsCodeFixProvider : BaseCodeFixProvider<FieldDeclarationSyntax>
    {
        internal OnlyPrivateFieldsInStructsCodeFixProvider()
            : base(new DocumentRootFixContextProvider(), new OnlyPrivateFieldsInStructsFixer(), OnlyPrivateInStructsDiagnostic.Descriptors)
        {
            // noop
        }
    }
}