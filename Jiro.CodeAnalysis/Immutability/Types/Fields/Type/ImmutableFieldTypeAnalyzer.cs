namespace Jiro.CodeAnalysis.Immutability.Types.Fields.Type
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Types.Fields.Type.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Diagnostics;

    internal sealed class ImmutableFieldTypeAnalyzer : IAnalyzer<IFieldSymbol>
    {
        public void Register(AnalysisContext context)
        {
            context.RegisterSymbolAction(this, SymbolKind.Field);
        }

        public Diagnostic Analyze(IFieldSymbol field)
        {
            Debug.Assert(field != null, "field must not be null.");

            return
                field.Type.TypeKind == TypeKind.Array ?
                Diagnostic.Create(ImmutableFieldTypeDiagnostic.Descriptor, field.Locations[0], field.Name) :
                EmptyDiagnostic.Create();
        }
    }
}