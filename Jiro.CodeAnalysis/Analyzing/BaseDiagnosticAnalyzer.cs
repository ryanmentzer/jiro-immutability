namespace Jiro.CodeAnalysis.Analyzing
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Immutable;

    public abstract class BaseDiagnosticAnalyzer<TSymbol> : DiagnosticAnalyzer
    {
        private readonly IAnalyzer<TSymbol> analyzer;
        private readonly ImmutableArray<DiagnosticDescriptor> diagnostics;

        internal BaseDiagnosticAnalyzer(IAnalyzer<TSymbol> analyzer, ImmutableArray<DiagnosticDescriptor> diagnostics)
        {
            this.analyzer = analyzer;
            this.diagnostics = diagnostics;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return this.diagnostics;
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            this.analyzer.Register(context);
        }
    }
}