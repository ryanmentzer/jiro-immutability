namespace Jiro.CodeAnalysis.Immutability.Fields.ReadOnly.Diagnostics
{
    using Microsoft.CodeAnalysis;
    using System.Collections.Immutable;

    internal sealed class ReadOnlyFieldDiagnostic
    {
        internal const string Id = "JIRO-001";

        internal static readonly DiagnosticDescriptor Descriptor =
            new DiagnosticDescriptor(
                Id, 
                "Fields should be readonly.", 
                "The '{0}' field should be modified with the 'readonly' keyword.", 
                "Immutability", 
                DiagnosticSeverity.Warning, 
                true, 
                helpLink: "https://github.com/ryanmentzer/Jiro/blob/master/Jiro.CodeAnalysis/Immutability/Fields/ReadOnly/readme.md");

        internal static readonly ImmutableArray<DiagnosticDescriptor> Descriptors = ImmutableArray.Create(Descriptor);
    }
}