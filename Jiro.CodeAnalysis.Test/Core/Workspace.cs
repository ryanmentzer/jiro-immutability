namespace Jiro.CodeAnalysis.Core
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.Diagnostics;

    internal class Workspace
    {
        private readonly Solution solution;
        private readonly Project project;
        private readonly Document document;

        internal Workspace(Solution solution, Project project, Document document)
        {
            this.solution = solution;
            this.project = project;
            this.document = document;
        }

        internal AnalyzerAndFixerWorkspace Setup(DiagnosticAnalyzer analyzer, CodeFixProvider fixProvider)
        {
            return 
                new AnalyzerAndFixerWorkspace(
                    this.document, 
                    this.project, 
                    analyzer, 
                    fixProvider);
        }

        internal AnalyzerWorkspace Setup(DiagnosticAnalyzer analyzer)
        {
            return new AnalyzerWorkspace(this.project, analyzer);
        }
    }
}