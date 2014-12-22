namespace Jiro.CodeAnalysis.Fixing
{
    internal interface IFixer<T>
    {
        FixResult ApplyFix(T syntax);
    }
}