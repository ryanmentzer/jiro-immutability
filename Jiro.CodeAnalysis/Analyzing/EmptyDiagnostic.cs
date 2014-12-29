namespace Jiro.CodeAnalysis.Analyzing
{
    using Microsoft.CodeAnalysis;

    internal static class EmptyDiagnostic
    {
        private static readonly Diagnostic empty =
            Diagnostic.Create(string.Empty, string.Empty, string.Empty, DiagnosticSeverity.Hidden, DiagnosticSeverity.Hidden, false, 0);

        internal static Diagnostic Create() => empty;

        internal static bool IsEmpty(this Diagnostic diagnostic) => string.IsNullOrEmpty(diagnostic.Id);
    }
}