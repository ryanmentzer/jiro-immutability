namespace Jiro.CodeAnalysis.Immutability.Fields.UseImmutableCollections.Diagnostics
{
    using Microsoft.CodeAnalysis;
    using System.Collections.Immutable;

    internal sealed class UseImmutableCollectionsDiagnostic
    {
        internal const string Id = "jiro.4";

        internal static readonly DiagnosticDescriptor Descriptor =
            new DiagnosticDescriptor(
                Id,
                "Field types must not be mutable.",
                "The data type of the '{0}' field should be changed to an immutable type.",
                "Immutability",
                DiagnosticSeverity.Warning,
                true,
                helpLink: "http://www.ryanmentzer.com");

        internal static readonly ImmutableArray<DiagnosticDescriptor> Descriptors = ImmutableArray.Create(Descriptor);
    }
}