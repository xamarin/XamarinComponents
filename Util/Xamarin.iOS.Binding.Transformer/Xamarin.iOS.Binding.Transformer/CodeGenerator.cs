using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Xamarin.iOS.Binding.Transformer
{
    public static class CodeGenerator
    {
        public async static Task GenerateAsync(ApiDefinition api, string outputFilename)
        {
            var syntaxFactory = SyntaxFactory.CompilationUnit();

            foreach (var aUsing in api.Usings)
            {
                var node = SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(" " + aUsing.Name));
                node = node.WithTrailingTrivia(SyntaxFactory.LineFeed);

                syntaxFactory = syntaxFactory.AddUsings(node);
            }

            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(api.Namespace)).NormalizeWhitespace();
            @namespace = @namespace.WithLeadingTrivia(SyntaxFactory.LineFeed);

            foreach (var aDel in api.Delegates)
            {
                var delegateDeclaration = SyntaxFactory.DelegateDeclaration(SyntaxFactory.ParseTypeName(aDel.ReturnType), SyntaxFactory.ParseToken(" " + aDel.Name));
                delegateDeclaration = delegateDeclaration.WithLeadingTrivia(SyntaxFactory.Tab);
                delegateDeclaration = delegateDeclaration.WithTrailingTrivia(SyntaxFactory.LineFeed);

                //needs to build parameters
                @namespace = @namespace.AddMembers(delegateDeclaration);
            }

            foreach (var aClass in api.Types)
            {
                //build the attributes for the class
                var attributeList = BuildClassAttributes(aClass);

                //create the interface
                var interfaceDeclaration = SyntaxFactory.InterfaceDeclaration(" " + aClass.Name);


                 //if there are attributes then add them to the class definition
                if (attributeList.Count > 0)
                {
                    var atsyn = SyntaxFactory.AttributeList(attributeList);
                    interfaceDeclaration = interfaceDeclaration.AddAttributeLists(atsyn.WithTrailingTrivia(SyntaxFactory.LineFeed));
                }

                interfaceDeclaration = interfaceDeclaration.WithLeadingTrivia(SyntaxFactory.LineFeed, SyntaxFactory.Tab);
                interfaceDeclaration = interfaceDeclaration.WithTrailingTrivia(SyntaxFactory.LineFeed);

                @namespace = @namespace.AddMembers(interfaceDeclaration);
            }

            syntaxFactory = syntaxFactory.AddMembers(@namespace);

            
            var code = syntaxFactory.ToFullString();

            using (var writeFile = new StreamWriter(outputFilename))
            {
                await writeFile.WriteAsync(code);
            }

            //Console.WriteLine(code);    


        }

        private static SeparatedSyntaxList<AttributeSyntax> BuildClassAttributes(ApiClass apiClass)
        {
            var attributeList = new SeparatedSyntaxList<AttributeSyntax>();

            if (apiClass.IsProtocol)
            {
                var name = SyntaxFactory.ParseName("Protocol");
                var attribute = SyntaxFactory.Attribute(name);
                attributeList = attributeList.Add(attribute);
                

            }

            if (apiClass.IsCategory)
            {
                var name = SyntaxFactory.ParseName("Category");
                var attribute = SyntaxFactory.Attribute(name);
                attributeList = attributeList.Add(attribute);


            }

            

            if (apiClass.Model != null)
            {

            }

            if (apiClass.BaseType != null)
            {

            }

            if (apiClass.DisableDefaultCtor)
            {
                var name = SyntaxFactory.ParseName("DisableDefaultCtor");
                var attribute = SyntaxFactory.Attribute(name);
                attributeList = attributeList.Add(attribute);


            }

            return attributeList;
        }
    }
}
