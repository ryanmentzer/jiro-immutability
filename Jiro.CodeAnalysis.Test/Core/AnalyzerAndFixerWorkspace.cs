namespace Jiro.CodeAnalysis.Core
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeActions;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class AnalyzerAndFixerWorkspace
    {
        private readonly Project project;
        private readonly Document document;
        private readonly DiagnosticAnalyzer analyzer;
        private readonly CodeFixProvider codeFixProvider;

        internal AnalyzerAndFixerWorkspace(
            Document document,
            Project project,
            DiagnosticAnalyzer analyzer,
            CodeFixProvider codeFixProvider)
        {
            this.project = project;
            this.document = document;
            this.analyzer = analyzer;
            this.codeFixProvider = codeFixProvider;
        }

        internal async Task<string> ApplyFix()
        {
            var compilation = await this.project.GetCompilationAsync(CancellationToken.None);

            var diagnostics = await
                new DiagnosticProvider(compilation)
                    .ListDiagnostics(this.analyzer);

            var result = await GetFixes(this.codeFixProvider, this.document, diagnostics);

            return result.Length > 0 ? result[0] : await GetText(this.document);
        }

        private static async Task<string> GetText(Document document)
        {
            var result = await document.GetTextAsync(CancellationToken.None);

            return result.ToString();
        }

        private static async Task<ImmutableArray<string>> GetFixes(
            CodeFixProvider codeFixProvider, 
            Document document, 
            ImmutableArray<Diagnostic> diagnostics)
        {
            var codeActions = await ListCodeActions(codeFixProvider, document, diagnostics);

            var codeChangeTasks = codeActions.Select(a => GetChangedText(a, document.Id));

            var codeChanges = await Task.WhenAll(codeChangeTasks);

            return codeChanges.ToImmutableArray();
        }

        private static async Task<IEnumerable<CodeAction>> ListCodeActions(
            CodeFixProvider codeFixProvider, 
            Document document, 
            ImmutableArray<Diagnostic> diagnostics)
        {
            var codeActions = new ConcurrentBag<CodeAction>();
            var getTasks = ImmutableArray.CreateBuilder<Task>();

            foreach (var diagnostic in diagnostics)
            {
                var context = 
                    new CodeFixContext(
                        document, 
                        diagnostic,
                        (action, matchedDiags) => codeActions.Add(action), 
                        CancellationToken.None);

                getTasks.Add(
                    codeFixProvider.ComputeFixesAsync(context));
            }

            await Task.WhenAll(getTasks.ToImmutable());

            return codeActions;
        }

        private static async Task<string> GetChangedText(CodeAction codeAction, DocumentId documentId)
        {
            var operations = await codeAction.GetOperationsAsync(CancellationToken.None);

            var operation = (ApplyChangesOperation)operations.Single();

            var newDoc = operation.ChangedSolution.GetDocument(documentId);

            var text = await newDoc.GetTextAsync();

            return text.ToString();
        }
    }
}