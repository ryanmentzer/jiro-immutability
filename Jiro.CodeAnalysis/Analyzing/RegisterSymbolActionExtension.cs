namespace Jiro.CodeAnalysis.Analyzing
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    internal static class RegisterSymbolActionExtension
    {
        internal static void RegisterSymbolAction<T>(this AnalysisContext context, IAnalyzer<T> examiner, params SymbolKind[] kinds) =>
            context.RegisterSymbolAction(examiner.AnalyzeSymbol, kinds);
    }
}