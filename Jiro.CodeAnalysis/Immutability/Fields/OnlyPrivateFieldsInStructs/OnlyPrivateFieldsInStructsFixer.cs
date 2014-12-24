namespace Jiro.CodeAnalysis.Immutability.Fields.OnlyPrivateFieldsInStructs
{
    using Jiro.CodeAnalysis.Fixing;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;

    internal sealed class OnlyPrivateFieldsInStructsFixer : IFixer<FieldDeclarationSyntax>
    {
        private static ImmutableHashSet<SyntaxKind> AccessModifiers =
            ImmutableHashSet.Create<SyntaxKind>(SyntaxKind.PublicKeyword, SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword, SyntaxKind.PrivateKeyword);

        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.CodeAnalysis.CSharp.SyntaxFactory.Whitespace(System.String,System.Boolean)")]
        public FixResult ApplyFix(FieldDeclarationSyntax field)
        {
            Debug.Assert(field != null, "field must not be null.");
            Debug.Assert(field.Parent != null, "field.Parent must not be null.");

            var @private =
                SyntaxFactory.Token(
                    SyntaxTriviaList.Empty,
                    SyntaxKind.PrivateKeyword,
                    SyntaxTriviaList.Create(SyntaxFactory.Whitespace(" ")));

            var modifiers =
                SyntaxTokenList
                    .Create(@private)
                    .AddRange(field.Modifiers.Where(x => !AccessModifiers.Contains(x.CSharpKind())));

            return new FixResult(
                field.Parent.ReplaceNode(field, field.WithModifiers(modifiers)),
                string.Format(CultureInfo.CurrentUICulture, "Make the '{0}' field private.", field.Declaration.Variables[0].Identifier.Text));
        }
    }
}