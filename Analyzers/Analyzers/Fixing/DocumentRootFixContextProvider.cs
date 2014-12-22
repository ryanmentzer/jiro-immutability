namespace Jiro.CodeAnalysis.Fixing
{
    using Microsoft.CodeAnalysis.CodeFixes;
    using System.Linq;

    internal sealed class DocumentRootFixContextProvider : IFixContextProvider
    {
        public IFixContext Create(CodeFixContext context)
        {
            return new DocumentRootFixContext(context, context.Diagnostics.First());
        }
    }
}