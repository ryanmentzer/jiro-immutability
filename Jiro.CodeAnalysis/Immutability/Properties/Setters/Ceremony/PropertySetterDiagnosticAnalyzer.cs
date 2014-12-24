namespace Jiro.CodeAnalysis.Immutability.Types.Properties.Setters.Ceremony
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Types.Properties.Setters.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class PropertySetterDiagnosticAnalyzer : BaseDiagnosticAnalyzer<IPropertySymbol>
    {
        internal PropertySetterDiagnosticAnalyzer()
            : base(new PropertySetterAnalyzer(), PropertySetterDiagnostic.Descriptors)
        {
            // noop
        }
    }
}