namespace Jiro.CodeAnalysis.Immutability.Fields.ReadOnly
{
    using Jiro.CodeAnalysis.Fixing;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    internal sealed class ReadOnlyFieldFixer : IFixer<FieldDeclarationSyntax>
    {
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.CodeAnalysis.CSharp.SyntaxFactory.Whitespace(System.String,System.Boolean)")]
        public FixResult ApplyFix(FieldDeclarationSyntax field)
        {
            Guard.NotNull(field, nameof(field));
            Guard.NotNull(field.Parent, nameof(FieldDeclarationSyntax.Parent));
             
            var readOnly =
                SyntaxFactory.Token(
                    SyntaxTriviaList.Empty,
                    SyntaxKind.ReadOnlyKeyword,
                    SyntaxTriviaList.Create(SyntaxFactory.Whitespace(" ")));

            return new FixResult(
                field.Parent.ReplaceNode(field, field.AddModifiers(readOnly)),
                string.Format(CultureInfo.CurrentUICulture, "Make the '{0}' field readonly.", field.Declaration.Variables[0].Identifier.Text));
        }
    }
}