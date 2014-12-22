namespace Jiro.CodeAnalysis.Fixing
{
    using Microsoft.CodeAnalysis;

    internal static class DiagnosticIsEmptyExtension
    {
        internal static bool IsEmpty(this Diagnostic diagnostic)
        {
            return string.IsNullOrEmpty(diagnostic.Id);
        }
    }
}