namespace Jiro.CodeAnalysis.Core
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeActions;
    using Microsoft.CodeAnalysis.CodeFixes;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class StringFixProvider
    {
        private readonly CodeFixProvider codeFixProvider;
        private readonly Document document;

        public StringFixProvider(Document document, CodeFixProvider codeFixProvider)
        {
            Debug.Assert(document != null, "document must not be null");
            Debug.Assert(codeFixProvider != null, "codeFixProvider must not be null");

            this.document = document;
            this.codeFixProvider = codeFixProvider;
        }

        internal async Task<ImmutableArray<string>> List(ImmutableArray<Diagnostic> diagnostics, CancellationToken cancellationToken)
        {
            var actions = await 
                ListCodeActions(
                    this.codeFixProvider, 
                    this.document, 
                    diagnostics, 
                    cancellationToken);

            var changes = await
                Task.WhenAll(actions.Select(x => ToString(x, this.document.Id)));

            return changes.ToImmutableArray();
        }

        private static async Task<IEnumerable<CodeAction>> ListCodeActions(
            CodeFixProvider codeFixProvider,
            Document document,
            ImmutableArray<Diagnostic> diagnostics,
            CancellationToken cancellationToken)
        {
            var result = new ConcurrentBag<CodeAction>();

            var tasks =
                diagnostics
                    .Select(x => ToCodeFixContext(x, document, result.Add, cancellationToken))
                    .Select(codeFixProvider.ComputeFixesAsync)
                    .ToImmutableArray();

            await Task.WhenAll(tasks);

            return result;
        }

        private static CodeFixContext ToCodeFixContext(
            Diagnostic diagnostic,
            Document document,
            Action<CodeAction> registration,
            CancellationToken cancellationToken)
        {
            return new
                CodeFixContext(
                    document,
                    diagnostic,
                    (action, _) => registration(action),
                    cancellationToken);
        }

        private static async Task<string> ToString(CodeAction codeAction, DocumentId documentId)
        {
            var text = await
                (await GetApplyChangesOperation(codeAction))
                    .ChangedSolution
                    .GetDocument(documentId)
                    .GetTextAsync();

            return text.ToString();
        }

        private static async Task<ApplyChangesOperation> GetApplyChangesOperation(CodeAction codeAction)
        {
            var operations = await codeAction.GetOperationsAsync(CancellationToken.None);

            return (ApplyChangesOperation)operations.Single();
        }
    }
}