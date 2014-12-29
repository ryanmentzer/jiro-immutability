namespace Jiro.CodeAnalysis.Analyzing
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Immutable;

    internal abstract class BaseDiagnosticAnalyzer<TSymbol> : DiagnosticAnalyzer
    {
        private readonly IAnalyzer<TSymbol> analyzer;

        internal BaseDiagnosticAnalyzer(IAnalyzer<TSymbol> analyzer, ImmutableArray<DiagnosticDescriptor> diagnostics)
        {
            this.analyzer = Guard.NotNull(analyzer, nameof(analyzer));
            this.SupportedDiagnostics = diagnostics;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get;
        }

        public override void Initialize(AnalysisContext context) => this.analyzer.Register(context);
    }
}