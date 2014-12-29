namespace Jiro.CodeAnalysis.Fixing
{
    using Microsoft.CodeAnalysis.CodeFixes;
    using System.Linq;

    internal sealed class DocumentRootFixContextProvider : IFixContextProvider
    {
        IFixContext IFixContextProvider.Create(CodeFixContext context) => new DocumentRootFixContext(context, context.Diagnostics.First());
    }
}