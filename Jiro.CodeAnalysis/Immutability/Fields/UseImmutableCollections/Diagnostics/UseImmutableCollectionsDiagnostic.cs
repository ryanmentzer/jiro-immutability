namespace Jiro.CodeAnalysis.Immutability.Fields.UseImmutableCollections.Diagnostics
{
    using Microsoft.CodeAnalysis;
    using System.Collections.Immutable;

    internal sealed class UseImmutableCollectionsDiagnostic
    {
        internal const string Id = "JIRO-004";
        
        internal static readonly DiagnosticDescriptor Descriptor =
            new DiagnosticDescriptor(
                Id,
                "Fields should use immutable collections instead of arrays or mutable collections.",
                "Change the data type of '{0}' to corresponding type from System.Collections.Immutable.",
                "Immutability",
                DiagnosticSeverity.Warning,
                true,
                helpLink: "http://www.ryanmentzer.com");

        internal static readonly ImmutableArray<DiagnosticDescriptor> Descriptors = ImmutableArray.Create(Descriptor);
    }
}