namespace Jiro.CodeAnalysis.Fixing
{
    using Microsoft.CodeAnalysis;

    internal struct FixResult
    {
        public FixResult()
        {
            this.Node = null;
            this.Description = string.Empty;
        }

        internal FixResult(SyntaxNode node, string description)
        {
            this.Node = Guard.NotNull(node, nameof(node));
            this.Description = Guard.NotEmpty(description, nameof(description));
        }

        internal static FixResult Empty
        {
            get
            {
                return new FixResult();
            }
        }

        internal bool IsEmpty
        {
            get
            {
                return this.Node == null;
            }
        }

        internal SyntaxNode Node
        {
            get;
        }

        internal string Description
        {
            get;
        }
    }
}