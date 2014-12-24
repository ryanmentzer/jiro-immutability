namespace Jiro.CodeAnalysis.Immutability.Types.Fields.OnlyPrivateFieldsInStructs
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Types.Fields.OnlyPrivateFieldsInStructs.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System;
    using System.Diagnostics;

    internal sealed class OnlyPrivateFieldsInStructsAnalyzer : IAnalyzer<IFieldSymbol>
    {
        void IAnalyzer<IFieldSymbol>.Register(AnalysisContext context)
        {
            context.RegisterSymbolAction(this, SymbolKind.Field);
        }

        Diagnostic IAnalyzer<IFieldSymbol>.Analyze(IFieldSymbol field)
        {
            Debug.Assert(field != null, "field must not be null.");
            Debug.Assert(field.ContainingType != null, "field.ContainingType must not be null.");

            var result = EmptyDiagnostic.Create();

            if (field.ContainingType.IsValueType &&
                field.DeclaredAccessibility != Accessibility.Private)
            {
                result = Diagnostic.Create(OnlyPrivateInStructsDiagnostic.Descriptor, field.Locations[0], field.Name);
            }

            return result;
        }
    }
}