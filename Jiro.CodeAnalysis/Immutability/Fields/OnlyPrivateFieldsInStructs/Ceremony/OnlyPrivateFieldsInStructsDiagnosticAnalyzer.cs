﻿namespace Jiro.CodeAnalysis.Immutability.Types.Fields.OnlyPrivateFieldsInStructs.Ceremony
{
    using Jiro.CodeAnalysis.Analyzing;
    using Jiro.CodeAnalysis.Immutability.Types.Fields.OnlyPrivateFieldsInStructs.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class OnlyPrivateFieldsInStructsDiagnosticAnalyzer : BaseDiagnosticAnalyzer<IFieldSymbol>
    {
        internal OnlyPrivateFieldsInStructsDiagnosticAnalyzer()
            : base(new OnlyPrivateFieldsInStructsAnalyzer(), OnlyPrivateInStructsDiagnostic.Descriptors)
        {
            // noop
        }
    }
}