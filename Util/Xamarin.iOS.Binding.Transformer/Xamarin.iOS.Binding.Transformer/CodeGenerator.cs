using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

//Microsoft.CodeAnalysis.CSharp.Workspaces

namespace Xamarin.iOS.Binding.Transformer
{
    public static class CodeGenerator
    {
        public async static Task GenerateAsync(ApiDefinition api, string outputFilename)
        {
            //create the base complation unit
            var syntaxFactory = SyntaxFactory.CompilationUnit();

            //added the usings
            foreach (var aUsing in api.Usings.Items)
            {
                var node = SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(" " + aUsing.Name));
                node = node.WithTrailingTrivia(SyntaxFactory.LineFeed);

                syntaxFactory = syntaxFactory.AddUsings(node);
            }

            var lastDelegates = new List<string>();

            foreach (var aNamespace in api.Namespaces)
            {
                var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(aNamespace.Name)).NormalizeWhitespace();
                @namespace = @namespace.WithLeadingTrivia(SyntaxFactory.LineFeed);

                //interate through the delegates
                foreach (var aDel in aNamespace.Delegates)
                {
                    // build the delegate syntax declaration
                    var delegateDeclaration = BuildDelegate(aDel);

                    var str = delegateDeclaration.NormalizeWhitespace().ToFullString();

                    //needs to build parameters
                    @namespace = @namespace.AddMembers(delegateDeclaration);

                    if (aNamespace.Delegates.Last() == aDel)
                        lastDelegates.Add(str);
                }

                //interate through the types
                foreach (var aClass in aNamespace.Types)
                {
                    //build the type for the ApiClass object
                    var aType = BuildType(aClass);

                    //add it to the namespace
                    @namespace = @namespace.AddMembers(aType);
                }

                //add the namespace to the compilation unit
                syntaxFactory = syntaxFactory.AddMembers(@namespace);
            }

            //create the namespace root element

            //format and export as a string
            var code = syntaxFactory.NormalizeWhitespace().ToFullString();

            //fix to add gap between delegates and first type delcaration
            if (lastDelegates.Count > 0)
            {
                //loop through all the last delegates from all the namespaces
                foreach (var aDel in lastDelegates)
                {
                    //replace and add a new line
                    code = code.Replace(aDel, aDel + Environment.NewLine);
                }
            }

            //write to the output file
            using (var writeFile = new StreamWriter(outputFilename))
            {
                await writeFile.WriteAsync(code);
            }

        }

        #region Private Methods

        /// <summary>
        /// Build the type syntax node
        /// </summary>
        /// <param name="aClass"></param>
        /// <returns></returns>
        private static TypeDeclarationSyntax BuildType(ApiClass aClass)
        {
            //build the attributes for the class
            var attributeList = BuildClassAttributes(aClass);

            //create the interface
            var interfaceDeclaration = SyntaxFactory.InterfaceDeclaration(aClass.Name);

            //does the class inherit from anything
            if (aClass.Implements.Any())
            {
                //build the baselistsyntax object
                var baseClasses = BuildClassBaseTypes(aClass);

                //if it returns items add it to the declaration
                if (baseClasses.Types.Any())
                    interfaceDeclaration = interfaceDeclaration.WithBaseList(baseClasses);
            }
            
            //if there are attributes then add them to the class definition
            if (attributeList.Any())
                interfaceDeclaration = interfaceDeclaration.WithAttributeLists(attributeList);

            var members = BuildClassMembers(aClass);

            //if there are any members add them
            if (members.Any())
                interfaceDeclaration = interfaceDeclaration.WithMembers(members);

            ///formatting
            interfaceDeclaration = interfaceDeclaration.WithLeadingTrivia(SyntaxFactory.LineFeed, SyntaxFactory.Tab);
            interfaceDeclaration = interfaceDeclaration.WithTrailingTrivia(SyntaxFactory.LineFeed);

            if (aClass.IsPartial)
            {
                interfaceDeclaration = interfaceDeclaration.WithModifiers(SyntaxFactory.TokenList
                (
                    SyntaxFactory.Token(SyntaxKind.PartialKeyword)
                ));
            }
            return interfaceDeclaration;
        }

        private static SyntaxList<MemberDeclarationSyntax> BuildClassMembers(ApiClass aClass)
        {
            var returnMembers = SyntaxFactory.List<MemberDeclarationSyntax>();

            //work through the properties 
            foreach (var aProperty in aClass.Properties)
            {
                MemberDeclarationSyntax property = BuildProperty(aProperty);

                if (property != null)
                    returnMembers.Add(property);
            }

            //work through the methods
            foreach (var aMethod in aClass.Methods)
            {
                MemberDeclarationSyntax method = BuildMethod(aMethod);

                if (method != null)
                    returnMembers.Add(method);
            }

            return returnMembers;
        }

        private static MemberDeclarationSyntax BuildMethod(ApiMethod aMethod)
        {
            return null;

        }

        private static MemberDeclarationSyntax BuildProperty(ApiProperty aProperty)
        {
            
            return null;
        }

        /// <summary>
        /// Build the delegate syntax node
        /// </summary>
        /// <param name="apiDelegate"></param>
        /// <returns></returns>
        private static DelegateDeclarationSyntax BuildDelegate(ApiDelegate apiDelegate)
        {
            var delegateDeclaration = SyntaxFactory.DelegateDeclaration(SyntaxFactory.ParseTypeName(" " + apiDelegate.ReturnType), SyntaxFactory.ParseToken(" " + apiDelegate.Name));


            //add the paramters if there are any
            if (apiDelegate.Parameters.Any())
            {
                var parsList = BuildParameters(apiDelegate.Parameters);

                if (parsList.Parameters.Any())
                    delegateDeclaration = delegateDeclaration.WithParameterList(parsList);
            }

            delegateDeclaration = delegateDeclaration.WithLeadingTrivia(SyntaxFactory.LineFeed, SyntaxFactory.Tab);
            delegateDeclaration = delegateDeclaration.WithTrailingTrivia(SyntaxFactory.LineFeed);

            return delegateDeclaration;
        }

        /// <summary>
        /// Build the base classes/interfaces
        /// </summary>
        /// <param name="aClass"></param>
        /// <returns></returns>
        private static BaseListSyntax BuildClassBaseTypes(ApiClass aClass)
        {

            var tokens = new List<SyntaxNodeOrToken>();

            foreach (var basetype in aClass.Implements)
            {
                tokens.Add(SyntaxFactory.SimpleBaseType
                    (
                        SyntaxFactory.IdentifierName(basetype.Name)
                    ));


                if (basetype != aClass.Implements.Last())
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));

            }
        
            return SyntaxFactory.BaseList(SyntaxFactory.SeparatedList<BaseTypeSyntax>(tokens));

        }

        /// <summary>
        /// Build the class attributes
        /// </summary>
        /// <param name="apiClass"></param>
        /// <returns></returns>
        private static SyntaxList<AttributeListSyntax> BuildClassAttributes(ApiClass apiClass)
        {

            var attribs = SyntaxFactory.List<AttributeListSyntax>();

            if (apiClass.IsProtocol)
            {
                var attribList = SyntaxFactory.AttributeList
                            (
                                SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                                (
                                    SyntaxFactory.Attribute
                                    (
                                        SyntaxFactory.IdentifierName("Protocol")
                                    )
                                )
                            );

                attribs = attribs.Add(attribList);

            }

            if (apiClass.IsCategory)
            {
                var attribList = SyntaxFactory.AttributeList
                           (
                               SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                               (
                                   SyntaxFactory.Attribute
                                   (
                                       SyntaxFactory.IdentifierName("Category")
                                   )
                               )
                           );

                attribs = attribs.Add(attribList);


            }

            if (apiClass.IsStatic)
            {
                var attribList = SyntaxFactory.AttributeList
                           (
                               SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                               (
                                   SyntaxFactory.Attribute
                                   (
                                       SyntaxFactory.IdentifierName("Static")
                                   )
                               )
                           );

                attribs = attribs.Add(attribList);


            }

            if (apiClass.Model != null)
            {
                var sep = SyntaxFactory.AttributeList
                    (
                        SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                        (
                            SyntaxFactory.Attribute
                            (
                                SyntaxFactory.IdentifierName("Model")
                            )
                            .WithArgumentList
                            (
                                SyntaxFactory.AttributeArgumentList
                                (
                                    SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>
                                    (
                                        SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.LiteralExpression
                                            (
                                                SyntaxKind.TrueLiteralExpression
                                            )
                                        )
                                        .WithNameEquals
                                        (
                                            SyntaxFactory.NameEquals
                                            (
                                                SyntaxFactory.IdentifierName("AutoGeneratedName")
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    );

                attribs = attribs.Add(sep);
            }

            if (apiClass.BaseType != null)
            {
                var atrs = SyntaxFactory.AttributeList
                            (
                                SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                                (
                                    SyntaxFactory.Attribute
                                    (
                                        SyntaxFactory.IdentifierName("BaseType")
                                    )
                                    .WithArgumentList
                                    (
                                        SyntaxFactory.AttributeArgumentList
                                        (
                                            SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>
                                            (
                                                SyntaxFactory.AttributeArgument
                                                (
                                                    SyntaxFactory.TypeOfExpression
                                                    (
                                                        SyntaxFactory.IdentifierName(apiClass.BaseType.TypeName)
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            );

                attribs = attribs.Add(atrs);



            }

            if (apiClass.DisableDefaultCtor)
            {
                var attribList = SyntaxFactory.AttributeList
                           (
                               SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                               (
                                   SyntaxFactory.Attribute
                                   (
                                       SyntaxFactory.IdentifierName("DisableDefaultCtor")
                                   )
                               )
                           );

                attribs = attribs.Add(attribList);


            }

            if (apiClass.Verify != null)
            {
                var attribList = SyntaxFactory.AttributeList
                (
                    SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                    (
                        SyntaxFactory.Attribute
                        (
                            SyntaxFactory.IdentifierName("Verify")
                        )
                        .WithArgumentList
                        (
                            SyntaxFactory.AttributeArgumentList
                            (
                                SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>
                                (
                                    SyntaxFactory.AttributeArgument
                                    (
                                        SyntaxFactory.IdentifierName("ConstantsInterfaceAssociation")
                                    )
                                )
                            )
                        )
                    )
                );

                attribs = attribs.Add(attribList);
            }

            return attribs;
        }

        /// <summary>
        /// Build the paramter list syntax
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static ParameterListSyntax BuildParameters(IEnumerable<ApiParameter> parameters)
        {
            var tokens = new List<SyntaxNodeOrToken>();

            foreach (var aParam in parameters)
            {
                tokens.Add(SyntaxFactory.Parameter
                        (
                            SyntaxFactory.Identifier(aParam.Name)
                        )
                        .WithType
                        (
                            SyntaxFactory.IdentifierName(aParam.Type)
                        ));

                if (aParam != parameters.Last())
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
            }

            return SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList<ParameterSyntax>(tokens));

        }
        #endregion
    }
}
