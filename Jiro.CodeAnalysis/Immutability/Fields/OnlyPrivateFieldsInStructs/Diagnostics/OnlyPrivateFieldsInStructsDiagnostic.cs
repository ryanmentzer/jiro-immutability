namespace Jiro.CodeAnalysis.Immutability.Fields.OnlyPrivateFieldsInStructs.Diagnostics
{
    using Microsoft.CodeAnalysis;
    using System.Collections.Immutable;

    internal sealed class OnlyPrivateInStructsDiagnostic
    {
        internal const string Id = "JIRO-003";

        private static readonly DiagnosticDescriptor descriptor =
            new DiagnosticDescriptor(
                Id,
                "Fields in structs must be private.",
                "The '{0}' field should be modified with the 'private' access modifier.",
                "Immutability",
                DiagnosticSeverity.Warning,
                true,
                helpLink: "http://stackoverflow.com/questions/6063212/does-using-public-readonly-fields-for-immutable-structs-work/6063546#6063546");

        internal static readonly ImmutableArray<DiagnosticDescriptor> Descriptors = ImmutableArray.Create(descriptor);

        internal static Diagnostic Create(IFieldSymbol field) =>
            Guard.NotNull(
                field,
                nameof(field),
                () => Diagnostic.Create(descriptor, field.Locations[0], field.Name));
    }
}