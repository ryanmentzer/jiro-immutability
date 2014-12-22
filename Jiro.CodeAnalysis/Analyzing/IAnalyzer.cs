namespace Jiro.CodeAnalysis.Analyzing
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    internal interface IAnalyzer<T>
    {
        Diagnostic Analyze(T symbol);

        void Register(AnalysisContext context);
    }
}