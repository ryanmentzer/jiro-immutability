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

        public override ImmutableArray<string> GetFixableDiagnosticIds()
        {
            return this.fixableDiagnosticIds;
        }

        public override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public override async Task ComputeFixesAsync(CodeFixContext context)
        {
            await this.contextProvider.Create(context).RegisterFix(this.fixer);
        }
    }
}