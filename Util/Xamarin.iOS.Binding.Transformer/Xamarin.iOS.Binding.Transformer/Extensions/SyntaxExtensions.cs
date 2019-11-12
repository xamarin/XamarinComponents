using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CodeAnalysis
{
    public static class SyntaxExtensions
    {
        public static TSyntax AddTrailingTrivia<TSyntax>(this TSyntax node, bool linebreak = false, bool space = false) where TSyntax : SyntaxNode
        {
            var newNode = node;

            var trivia = new List<SyntaxTrivia>();

            if (space)
                trivia.Add(SyntaxFactory.Space);

            if (linebreak == true)
                trivia.Add(SyntaxFactory.LineFeed);

            newNode = newNode.WithTrailingTrivia(trivia);

            return newNode;
        }

        public static TSyntax AddLeadingTrivia<TSyntax>(this TSyntax node, bool newLine = false, bool tab = false, bool space = false) where TSyntax : SyntaxNode
        {
            var newNode = node;

            var trivia = new List<SyntaxTrivia>();

            if (space)
                trivia.Add(SyntaxFactory.Space);

            if (newLine)
                trivia.Add(SyntaxFactory.LineFeed);

            if (tab)
                trivia.Add(SyntaxFactory.Tab);

            newNode = newNode.WithLeadingTrivia(trivia);

            return newNode;
        }

        public static InterfaceDeclarationSyntax ApplyOpenBraceToken(this InterfaceDeclarationSyntax node, bool linebreak = true)
        {
            var newNode = node;

            if (linebreak == true)
                newNode = node.WithOpenBraceToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.LineFeed
                                ),
                                SyntaxKind.OpenBraceToken,
                                SyntaxFactory.TriviaList()
                            )
                        );


            return newNode;
        }
    }
}
