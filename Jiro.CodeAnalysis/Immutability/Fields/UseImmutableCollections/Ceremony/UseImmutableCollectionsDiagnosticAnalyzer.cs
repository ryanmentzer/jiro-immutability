namespace Jiro.CodeAnalysis.Immutability.Fields.UseImmutableCollections.Ceremony
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Fields.UseImmutableCollections.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class UseImmutableCollectionsDiagnosticAnalyzer : BaseDiagnosticAnalyzer<IFieldSymbol>
    {
        internal UseImmutableCollectionsDiagnosticAnalyzer()
            : base(new UseImmutableCollectionsAnalyzer(), UseImmutableCollectionsDiagnostic.Descriptors)
        {
            // noop
        }
    }
}