namespace Jiro.CodeAnalysis.Immutability.Types.Fields.ReadOnly
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Types.Fields.ReadOnly.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Diagnostics;

    internal sealed class ReadOnlyFieldAnalyzer : IAnalyzer<IFieldSymbol>
    {
        public void Register(AnalysisContext context)
        {
            context.RegisterSymbolAction(this, SymbolKind.Field);
        }
    
        public Diagnostic Analyze(IFieldSymbol field)
        {
            Debug.Assert(field != null, "field must not be null.");

            return
                field.IsReadOnly ?
                EmptyDiagnostic.Create() :
                Diagnostic.Create(ReadOnlyFieldDiagnostic.Descriptor, field.Locations[0], field.Name);
        }
    }
}