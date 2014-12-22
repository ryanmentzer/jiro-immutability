namespace Jiro.CodeAnalysis.Immutability.Types.Fields.OnlyPrivateFieldsInStructs.Ceremony
{
    using Jiro.CodeAnalysis.Fixing;
    using Jiro.CodeAnalysis.Immutability.Types.Fields.OnlyPrivateFieldsInStructs.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    [ExportCodeFixProvider(OnlyPrivateInStructsDiagnostic.Id, LanguageNames.CSharp)]
    public class OnlyPrivateFieldsInStructsCodeFixProvider : BaseCodeFixProvider<FieldDeclarationSyntax>
    {
        public OnlyPrivateFieldsInStructsCodeFixProvider()
            : base(new DocumentRootFixContextProvider(), new OnlyPrivateFieldsInStructsFixer(), OnlyPrivateInStructsDiagnostic.Descriptors)
        {
            // noop
        }
    }
}