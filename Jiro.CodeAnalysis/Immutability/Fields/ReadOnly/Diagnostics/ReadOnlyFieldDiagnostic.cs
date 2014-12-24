namespace Jiro.CodeAnalysis.Immutability.Types.Fields.ReadOnly.Diagnostics
{
    using Microsoft.CodeAnalysis;
    using System.Collections.Immutable;

    internal sealed class ReadOnlyFieldDiagnostic
    {
        internal const string Id = "jiro.1";

        internal static readonly DiagnosticDescriptor Descriptor =
            new DiagnosticDescriptor(
                Id, 
                "Fields must be readonly.", 
                "The '{0}' field should be modified with the 'readonly' keyword.", 
                "Immutability", 
                DiagnosticSeverity.Warning, 
                true, 
                helpLink: "http://www.ryanmentzer.com");

        internal static readonly ImmutableArray<DiagnosticDescriptor> Descriptors = ImmutableArray.Create(Descriptor);
    }
}