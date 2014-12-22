namespace Jiro.CodeAnalysis.Analyzing
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    internal static class RegisterSymbolActionExtension
    {
        internal static void RegisterSymbolAction<T>(this AnalysisContext context, IAnalyzer<T> examiner, params SymbolKind[] kinds)
        {
            context.RegisterSymbolAction(
                x =>
                {
                    var diagnosis = examiner.Analyze((T)x.Symbol);

                    if (!diagnosis.IsEmpty())
                    {
                        x.ReportDiagnostic(diagnosis);
                    }
                },
                kinds);
        }
    }
}