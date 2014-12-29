namespace Jiro.CodeAnalysis.Fixing
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeFixes;
    using System.Collections.Immutable;
    using System.Threading.Tasks;

    public abstract class BaseCodeFixProvider<TSyntax> : CodeFixProvider
    {
        private readonly IFixContextProvider contextProvider;
        private readonly IFixer<TSyntax> fixer;
        private readonly ImmutableArray<string> fixableDiagnosticIds;

        internal BaseCodeFixProvider(
            IFixContextProvider contextProvider, 
            IFixer<TSyntax> fixer, 
            ImmutableArray<DiagnosticDescriptor> diagnostics)
        {
            this.contextProvider = contextProvider;
            this.fixer = fixer;
            this.fixableDiagnosticIds = diagnostics.SelectAsArray(x => x.Id);
        }

        public override ImmutableArray<string> GetFixableDiagnosticIds() => this.fixableDiagnosticIds;

        public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

        public override Task ComputeFixesAsync(CodeFixContext context) => 
            this.contextProvider.Create(context).RegisterFix(this.fixer);
    }
}