namespace Jiro.CodeAnalysis.Fixing
{
    using Microsoft.CodeAnalysis.CodeFixes;

    internal interface IFixContextProvider
    {
        IFixContext Create(CodeFixContext context);
    }
}