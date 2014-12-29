namespace Jiro.CodeAnalysis.Immutability.Fields.UseImmutableCollections
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Fields.UseImmutableCollections.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System;

    internal sealed class UseImmutableCollectionsAnalyzer : IAnalyzer<IFieldSymbol>
    {
        public void Register(AnalysisContext context) => context.RegisterSymbolAction(this, SymbolKind.Field);

        public Diagnostic Analyze(IFieldSymbol field)
        {
            Guard.NotNull(field, nameof(field));
            Guard.NotNull(field.Type, nameof(IFieldSymbol.Type));

            var type = field.Type;

            return
                type.TypeKind == TypeKind.Array || FromSystemCollectionsGeneric(type) ?
                Diagnostic.Create(UseImmutableCollectionsDiagnostic.Descriptor, field.Locations[0], field.Name) :
                EmptyDiagnostic.Create();
        }

        private static bool FromSystemCollectionsGeneric(ITypeSymbol type) =>
            string.Equals(
                "System.Collections.Generic",
                type.OriginalDefinition.ContainingNamespace.ToDisplayString(),
                StringComparison.Ordinal);
    }
}