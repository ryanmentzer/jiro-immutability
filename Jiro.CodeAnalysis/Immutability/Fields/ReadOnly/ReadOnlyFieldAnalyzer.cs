namespace Jiro.CodeAnalysis.Immutability.Fields.ReadOnly
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Fields.ReadOnly.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    internal sealed class ReadOnlyFieldAnalyzer : IAnalyzer<IFieldSymbol>
    {
        public void Register(AnalysisContext context) => context.RegisterSymbolAction(this, SymbolKind.Field);
    
        public Diagnostic Analyze(IFieldSymbol field)
        {
            Guard.NotNull(field, nameof(field));

            return
                field.IsReadOnly ?
                EmptyDiagnostic.Create() :
                ReadOnlyFieldDiagnostic.Create(field);
        }
    }
}