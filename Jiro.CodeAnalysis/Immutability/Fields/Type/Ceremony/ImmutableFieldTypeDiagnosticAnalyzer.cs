namespace Jiro.CodeAnalysis.Immutability.Types.Fields.Type.Ceremony
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Types.Fields.Type.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class ImmutableFieldTypeDiagnosticAnalyzer : BaseDiagnosticAnalyzer<IFieldSymbol>
    {
        internal ImmutableFieldTypeDiagnosticAnalyzer()
            : base(new ImmutableFieldTypeAnalyzer(), ImmutableFieldTypeDiagnostic.Descriptors)
        {
            // noop
        }
    }
}