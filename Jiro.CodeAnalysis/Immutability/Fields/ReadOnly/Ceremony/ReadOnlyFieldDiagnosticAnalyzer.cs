namespace Jiro.CodeAnalysis.Immutability.Types.Fields.ReadOnly.Ceremony
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Types.Fields.ReadOnly.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class ReadOnlyFieldDiagnosticAnalyzer : BaseDiagnosticAnalyzer<IFieldSymbol>
    {
        internal ReadOnlyFieldDiagnosticAnalyzer()
            : base(new ReadOnlyFieldAnalyzer(), ReadOnlyFieldDiagnostic.Descriptors)
        {
            // noop
        }
    }
}