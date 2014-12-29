namespace Jiro.CodeAnalysis.Analyzing
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    internal static class ReportDiagnosticExtension
    {
        internal static void Report(this Diagnostic diagnostic, SymbolAnalysisContext context)
        {
            if (!diagnostic.IsEmpty())
            {
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}