namespace Jiro.CodeAnalysis.Fixing
{
    using System.Threading.Tasks;

    internal interface IFixContext
    {
        Task RegisterFix<T>(IFixer<T> fixer);
    }
}