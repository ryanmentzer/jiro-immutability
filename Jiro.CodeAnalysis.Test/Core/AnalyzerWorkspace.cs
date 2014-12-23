namespace Jiro.CodeAnalysis.Core
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Immutable;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class AnalyzerWorkspace
    {
        private readonly Project project;
        private readonly DiagnosticAnalyzer analyzer;

        internal AnalyzerWorkspace(
            Project project,
            DiagnosticAnalyzer analyzer)
        {
            this.project = project;
            this.analyzer = analyzer;
        }

        internal async Task<ImmutableArray<Diagnostic>> Diagnose()
        {
            var compilation = await this.project.GetCompilationAsync(CancellationToken.None);

            return await
                new DiagnosticProvider(compilation)
                    .ListDiagnostics(this.analyzer);
        }
    }
}