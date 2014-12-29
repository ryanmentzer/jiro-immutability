namespace Jiro.CodeAnalysis.Immutability.Properties.Diagnostics
{
    using Microsoft.CodeAnalysis;
    using System.Collections.Immutable;

    internal sealed class PropertySetterDiagnostic
    {
        internal const string Id = "JIRO-002";

        private static readonly DiagnosticDescriptor descriptor =
            new DiagnosticDescriptor(
                "RCM002", 
                "Properties should not have setters.", 
                "The '{0}' property setter should be removed.", 
                "Immutability", 
                DiagnosticSeverity.Warning, 
                true, 
                helpLink: "http://www.ryanmentzer.com");

        internal static readonly ImmutableArray<DiagnosticDescriptor> Descriptors = ImmutableArray.Create(descriptor);

        internal static Diagnostic Create(IPropertySymbol property) =>
            Guard.NotNull(
                property,
                nameof(property),
                () => Diagnostic.Create(descriptor, property.Locations[0], property.Name));
    }
}