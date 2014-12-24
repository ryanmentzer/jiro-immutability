namespace Jiro.CodeAnalysis.Immutability.Properties.Setters
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Properties.Setters.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Diagnostics;

    internal sealed class PropertySetterAnalyzer : IAnalyzer<IPropertySymbol>
    {
        public void Register(AnalysisContext context)
        {
            context.RegisterSymbolAction(this, SymbolKind.Property);
        }

        public Diagnostic Analyze(IPropertySymbol property)
        {
            Debug.Assert(property != null, "property must not be null.");

            return
                property.IsReadOnly ?
                EmptyDiagnostic.Create() :
                Diagnostic.Create(PropertySetterDiagnostic.Descriptor, property.Locations[0], property.Name);
        }
    }
}