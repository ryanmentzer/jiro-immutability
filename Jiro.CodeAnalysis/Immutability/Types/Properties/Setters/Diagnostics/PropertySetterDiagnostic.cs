namespace Jiro.CodeAnalysis.Immutability.Types.Properties.Setters.Diagnostics
{
    using Microsoft.CodeAnalysis;
    using System.Collections.Immutable;

    internal sealed class PropertySetterDiagnostic
    {
        internal const string Id = "RCM002";

        internal static readonly DiagnosticDescriptor Descriptor =
            new DiagnosticDescriptor(
                "RCM002", 
                "Properties must not have setters.", 
                "The '{0}' property setter should be removed.", 
                "Immutability", 
                DiagnosticSeverity.Warning, 
                true, 
                helpLink: "http://www.ryanmentzer.com");

        internal static readonly ImmutableArray<DiagnosticDescriptor> Descriptors = ImmutableArray.Create(Descriptor);
    }
}