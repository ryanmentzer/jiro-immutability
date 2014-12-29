namespace Jiro.CodeAnalysis.Immutability.Fields.OnlyPrivateFieldsInStructs
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Fields.OnlyPrivateFieldsInStructs.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    internal sealed class OnlyPrivateFieldsInStructsAnalyzer : IAnalyzer<IFieldSymbol>
    {
        void IAnalyzer<IFieldSymbol>.Register(AnalysisContext context) =>
            context.RegisterSymbolAction(this, SymbolKind.Field);

        Diagnostic IAnalyzer<IFieldSymbol>.Analyze(IFieldSymbol field)
        {
            Guard.NotNull(field, nameof(field));
            Guard.NotNull(field.ContainingType, nameof(IFieldSymbol.ContainingType));

            var result = EmptyDiagnostic.Create();

            if (field.ContainingType.IsValueType &&
                field.DeclaredAccessibility != Accessibility.Private)
            {
                result = OnlyPrivateInStructsDiagnostic.Create(field);
            }

            return result;
        }
    }
}