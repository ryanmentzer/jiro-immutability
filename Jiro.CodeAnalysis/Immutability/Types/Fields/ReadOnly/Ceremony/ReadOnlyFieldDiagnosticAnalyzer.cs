namespace Jiro.CodeAnalysis.Immutability.Types.Fields.ReadOnly.Ceremony
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Types.Fields.ReadOnly.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ReadOnlyFieldDiagnosticAnalyzer : BaseDiagnosticAnalyzer<IFieldSymbol>
    {
        public ReadOnlyFieldDiagnosticAnalyzer()
            : base(new ReadOnlyFieldAnalyzer(), ReadOnlyFieldDiagnostic.Descriptors)
        {
            // noop
        }
    }
}