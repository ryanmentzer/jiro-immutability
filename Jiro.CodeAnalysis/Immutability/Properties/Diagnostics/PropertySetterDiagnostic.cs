namespace Jiro.CodeAnalysis.Immutability.Properties.Diagnostics
{
    using Microsoft.CodeAnalysis;
    using System.Collections.Immutable;

    internal sealed class PropertySetterDiagnostic
    {
        internal const string Id = "JIRO-002";

        internal static readonly DiagnosticDescriptor Descriptor =
            new DiagnosticDescriptor(
                "RCM002", 
                "Properties should not have setters.", 
                "The '{0}' property setter should be removed.", 
                "Immutability", 
                DiagnosticSeverity.Warning, 
                true, 
                helpLink: "http://www.ryanmentzer.com");

        internal static readonly ImmutableArray<DiagnosticDescriptor> Descriptors = ImmutableArray.Create(Descriptor);
    }
}