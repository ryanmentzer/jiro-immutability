namespace Jiro.CodeAnalysis.Core
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class AnalyzerAndFixerWorkspace
    {
        private readonly Project project;
        private readonly Document document;
        private readonly DiagnosticAnalyzer analyzer;
        private readonly StringFixProvider stringFixProvider;

        internal AnalyzerAndFixerWorkspace(
            Project project,
            Document document,
            DiagnosticAnalyzer analyzer,
            CodeFixProvider codeFixProvider)
        {
            Debug.Assert(project != null, "project must not be null");
            Debug.Assert(document != null, "document must not be null");
            Debug.Assert(analyzer != null, "analyzer must not be null");

            this.project = project;
            this.document = document;
            this.analyzer = analyzer;
            this.stringFixProvider = new StringFixProvider(document, codeFixProvider);
        }

        internal async Task<ImmutableArray<Diagnostic>> Diagnose()
        {
            var compilation = await this.project.GetCompilationAsync(CancellationToken.None);

            return await
                new DiagnosticProvider(compilation)
                    .ListDiagnostics(this.analyzer);
        }

        internal Task<string> ApplyFix()
        {
            return this.ApplyFix(CancellationToken.None);
        }

        internal async Task<string> ApplyFix(CancellationToken cancellationToken)
        {
            var compilation = await this.project.GetCompilationAsync(cancellationToken);

            var diagnostics = await
                new DiagnosticProvider(compilation)
                    .ListDiagnostics(this.analyzer);

            var result = await this.stringFixProvider.List(diagnostics, cancellationToken);

            return result.Length > 0 ? result[0] : await OriginalSourceCode(this.document, cancellationToken);
        }

        private static async Task<string> OriginalSourceCode(Document document, CancellationToken cancellationToken)
        {
            var result = await document.GetTextAsync(cancellationToken);

            return result.ToString();
        }
    }
}