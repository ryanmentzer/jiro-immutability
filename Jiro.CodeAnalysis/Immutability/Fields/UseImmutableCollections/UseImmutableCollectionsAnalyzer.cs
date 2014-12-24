namespace Jiro.CodeAnalysis.Immutability.Fields.UseImmutableCollections
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Fields.UseImmutableCollections.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Immutable;
    using System.Diagnostics;

    internal sealed class UseImmutableCollectionsAnalyzer : IAnalyzer<IFieldSymbol>
    {
        private static readonly ImmutableHashSet<string> NamespaceBlacklist =
            ImmutableHashSet.Create("System.Collections.Generic");

        public void Register(AnalysisContext context)
        {
            context.RegisterSymbolAction(this, SymbolKind.Field);
        }

        public Diagnostic Analyze(IFieldSymbol field)
        {
            Debug.Assert(field != null, "field must not be null.");
                       
            return
                field.Type.TypeKind == TypeKind.Array || NamespaceBlacklist.Contains(field.Type.OriginalDefinition.ContainingNamespace.ToDisplayString()) ?
                Diagnostic.Create(UseImmutableCollectionsDiagnostic.Descriptor, field.Locations[0], field.Name) :
                EmptyDiagnostic.Create();
        }
    }
}