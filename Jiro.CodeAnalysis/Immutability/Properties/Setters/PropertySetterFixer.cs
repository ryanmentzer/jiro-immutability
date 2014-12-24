namespace Jiro.CodeAnalysis.Immutability.Types.Properties.Setters
{
    using Jiro.CodeAnalysis.Fixing;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    internal sealed class PropertySetterFixer : IFixer<PropertyDeclarationSyntax>
    {
        public FixResult ApplyFix(PropertyDeclarationSyntax property)
        {
            Debug.Assert(property != null, "property must not be null.");

            var result = FixResult.Empty;

            var setter =
                property
                    .DescendantNodes()
                    .Where(x => x.IsKind(SyntaxKind.SetAccessorDeclaration))
                    .FirstOrDefault();

            if (setter != null)
            {
                result =
                    new FixResult(
                        property.Parent.RemoveNode(setter, SyntaxRemoveOptions.KeepNoTrivia),
                        string.Format(CultureInfo.CurrentUICulture, "Remove the '{0}' property setter.", property.Identifier.Text));
            }

            return result;
        }
    }
}