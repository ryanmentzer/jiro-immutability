using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Jiro.CodeAnalysis.Core
{
    internal interface IDiagnosticProvider
    {
        Task<ImmutableArray<Diagnostic>> ListDiagnostics(params DiagnosticAnalyzer[] analyzers);
    }
}