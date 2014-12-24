namespace Jiro.CodeAnalysis.Immutability.Fields.OnlyPrivateFieldsInStructs.Diagnostics
{
    using Microsoft.CodeAnalysis;
    using System.Collections.Immutable;

    internal sealed class OnlyPrivateInStructsDiagnostic
    {
        internal const string Id = "JIRO-003";

        internal static readonly DiagnosticDescriptor Descriptor =
            new DiagnosticDescriptor(
                Id,
                "Fields in structs must be private.",
                "The '{0}' field should be modified with the 'private' access modifier.",
                "Immutability",
                DiagnosticSeverity.Warning,
                true,
                helpLink: "http://stackoverflow.com/questions/6063212/does-using-public-readonly-fields-for-immutable-structs-work/6063546#6063546");

        internal static readonly ImmutableArray<DiagnosticDescriptor> Descriptors = ImmutableArray.Create(Descriptor);
    }
}