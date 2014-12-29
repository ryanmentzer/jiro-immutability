namespace Jiro.CodeAnalysis.Immutability.Properties
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Properties.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    internal sealed class PropertySetterAnalyzer : IAnalyzer<IPropertySymbol>
    {
        public void Register(AnalysisContext context) => context.RegisterSymbolAction(this, SymbolKind.Property);

        public Diagnostic Analyze(IPropertySymbol property)
        {
            Guard.NotNull(property, nameof(property));

            return
                property.IsReadOnly ?
                EmptyDiagnostic.Create() :
                PropertySetterDiagnostic.Create(property);
        }
    }
}