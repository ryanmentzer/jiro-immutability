namespace Jiro.CodeAnalysis.Core
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Immutable;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DiagnosticProvider : IDiagnosticProvider
    {
        private readonly Compilation compilation;

        public DiagnosticProvider(Compilation compilation)
        {
            this.compilation = compilation;
        }

        public async Task<ImmutableArray<Diagnostic>> ListDiagnostics(params DiagnosticAnalyzer[] analyzers)
        {
            Compilation buffer = null;

            var driver =
                AnalyzerDriver.Create(
                    this.compilation,
                    analyzers.ToImmutableArray(),
                    new AnalyzerOptions(ImmutableArray<AdditionalStream>.Empty, ImmutableDictionary<string, string>.Empty),
                    out buffer,
                    CancellationToken.None);

            buffer.GetDiagnostics(CancellationToken.None);

            return await driver.GetDiagnosticsAsync();
        }
    }
}