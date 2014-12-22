namespace Jiro.CodeAnalysis.Immutability.Types.Properties.Setters.Ceremony
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Types.Properties.Setters.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class PropertySetterDiagnosticAnalyzer : BaseDiagnosticAnalyzer<IPropertySymbol>
    {
        public PropertySetterDiagnosticAnalyzer()
            : base(new PropertySetterAnalyzer(), PropertySetterDiagnostic.Descriptors)
        {
            // noop
        }
    }
}