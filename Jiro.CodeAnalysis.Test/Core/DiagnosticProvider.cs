namespace Jiro.CodeAnalysis.Core
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DiagnosticProvider
    {
        private readonly Compilation compilation;

        internal DiagnosticProvider(Compilation compilation)
        {
            Debug.Assert(compilation != null, "compilation must not be null");

            this.compilation = compilation;
        }

        internal Task<ImmutableArray<Diagnostic>> ListDiagnostics(DiagnosticAnalyzer analyzer)
        {
            Debug.Assert(analyzer != null, "analyzer must not be null");

            return this.ListDiagnostics(ImmutableArray.Create(analyzer));
        }

        internal Task<ImmutableArray<Diagnostic>> ListDiagnostics(ImmutableArray<DiagnosticAnalyzer> analyzers)
        {
            Compilation buffer = null;

            var driver =
                AnalyzerDriver.Create(
                    this.compilation,
                    analyzers,
                    new AnalyzerOptions(ImmutableArray<AdditionalStream>.Empty, ImmutableDictionary<string, string>.Empty),
                    out buffer,
                    CancellationToken.None);

            if (buffer != null)
            {
                buffer.GetDiagnostics(CancellationToken.None);
            }

            return driver.GetDiagnosticsAsync();
        }
    }
}