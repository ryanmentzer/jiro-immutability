namespace Jiro.CodeAnalysis.Fixing
{
    using Microsoft.CodeAnalysis;
    using System.Diagnostics;

    internal struct FixResult
    {
        private readonly SyntaxNode node;
        private readonly string description;

        public FixResult()
        {
            this.node = null;
            this.description = string.Empty;
        }

        internal FixResult(SyntaxNode node, string description)
        {
            Debug.Assert(node != null, "node must not be null");
            Debug.Assert(!string.IsNullOrWhiteSpace(description), "description must not be null, empty, or whitespace");

            this.node = node;
            this.description = description;
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
                return this.node == null;
            }
        }

        internal SyntaxNode Node
        {
            get
            {
                return this.node;
            }
        }

        internal string Description
        {
            get
            {
                return this.description;
            }
        }
    }
}