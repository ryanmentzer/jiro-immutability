namespace Jiro.CodeAnalysis.Fixing
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeActions;
    using Microsoft.CodeAnalysis.CodeFixes;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class DocumentRootFixContext : IFixContext
    {
        private readonly CodeFixContext context;
        private readonly Diagnostic diagnostic;

        internal DocumentRootFixContext(CodeFixContext context, Diagnostic diagnostic)
        {
            this.context = context;
            this.diagnostic = diagnostic;
        }

        public async Task RegisterFix<T>(IFixer<T> fixer)
        {
            Debug.Assert(fixer != null, "fixer must not be null");

            var document = this.context.Document;

            var root = await
                document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var syntax =
                  root.FindToken(diagnostic.Location.SourceSpan.Start)
                      .Parent
                      .AncestorsAndSelf()
                      .OfType<T>()
                      .First();

            var fix = fixer.ApplyFix(syntax);

            if (!fix.IsEmpty)
            {
                this.context.RegisterFix(
                    CodeAction.Create(fix.Description, document.WithSyntaxRoot(fix.Node)),
                    this.diagnostic);
            }
        }
    }
}