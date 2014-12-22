namespace Jiro.CodeAnalysis.Core
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class AnalyzerAndFixerWorkspace
    {
        private readonly Project project;
        private readonly Document document;
        private readonly DiagnosticAnalyzer analyzer;
        private readonly StringFixProvider stringFixProvider;

        internal AnalyzerAndFixerWorkspace(
            Document document,
            Project project,
            DiagnosticAnalyzer analyzer,
            CodeFixProvider codeFixProvider)
        {
            this.project = project;
            this.document = document;
            this.analyzer = analyzer;
            this.stringFixProvider = new StringFixProvider(document, codeFixProvider);
        }

        internal async Task<string> ApplyFix()
        {
            var compilation = await this.project.GetCompilationAsync(CancellationToken.None);

            var diagnostics = await
                new DiagnosticProvider(compilation)
                    .ListDiagnostics(this.analyzer);

            var result = await this.stringFixProvider.List(diagnostics);

            return result.Length > 0 ? result[0] : await ToString(this.document);
        }

        private static async Task<string> ToString(Document document)
        {
            var result = await document.GetTextAsync(CancellationToken.None);

            return result.ToString();
        }
    }
}