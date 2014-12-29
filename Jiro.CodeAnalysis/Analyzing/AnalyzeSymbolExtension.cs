namespace Jiro.CodeAnalysis.Analyzing
{
    using Microsoft.CodeAnalysis.Diagnostics;

    internal static class AnalyzeSymbolExtension
    {
        internal static void AnalyzeSymbol<T>(this IAnalyzer<T> analyzer, SymbolAnalysisContext context) =>
            analyzer.Analyze((T)context.Symbol).Report(context);
    }
}