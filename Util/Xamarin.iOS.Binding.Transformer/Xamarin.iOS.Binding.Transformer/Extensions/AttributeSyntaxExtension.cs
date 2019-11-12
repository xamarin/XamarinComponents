using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
    public static class AttributeSyntaxExtension
    {
        public static AttributeSyntax WithArgumentTokens(this AttributeSyntax target, IEnumerable<SyntaxNodeOrToken> nodes)
        {
            return target.WithArgumentList(SyntaxFactory.AttributeArgumentList(SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(nodes)));
        }

        public static AttributeListSyntax AsAtrributeListSyntax(this AttributeSyntax target)
        {
            return SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(target)); ;

        }

        public static AttributeListSyntax AddOpenBracketToken(this AttributeListSyntax target, bool newLine = true, string whitespace = null)
        {
            var newNode = target;

            var ltrivlist = new List<SyntaxTrivia>();

            if (newLine)
                ltrivlist.Add(SyntaxFactory.LineFeed);

            if (whitespace != null && whitespace.Length > 0)
                ltrivlist.Add(SyntaxFactory.Whitespace(whitespace));

            var ltriv = SyntaxFactory.TriviaList(ltrivlist);

            newNode = newNode.WithOpenBracketToken(SyntaxFactory.Token(ltriv, SyntaxKind.OpenBracketToken, SyntaxFactory.TriviaList()));

            return newNode;

        }
    }
}
