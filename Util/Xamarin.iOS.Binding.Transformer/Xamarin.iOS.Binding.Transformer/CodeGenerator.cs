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
            //var code = syntaxFactory.NormalizeWhitespace().ToFullString();
            var code = syntaxFactory.ToFullString();

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
            var interfaceDeclaration = SyntaxFactory.InterfaceDeclaration(SyntaxFactory.Identifier
                    (
                        SyntaxFactory.TriviaList
                        (
                            SyntaxFactory.Space
                        ),
                        aClass.Name,
                        SyntaxFactory.TriviaList
                        (
                            SyntaxFactory.Space
                        )
                    ));

            interfaceDeclaration = interfaceDeclaration.WithLeadingTrivia(SyntaxFactory.LineFeed);

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

            if (aClass.IsPartial)
            {
                interfaceDeclaration = interfaceDeclaration.WithModifiers(SyntaxFactory.TokenList
                (
                    SyntaxFactory.Token(SyntaxKind.PartialKeyword)
                ));
            }

            ///formatting
            interfaceDeclaration = interfaceDeclaration.WithTrailingTrivia(SyntaxFactory.LineFeed);

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
                    returnMembers = returnMembers.Add(property);
            }

            //work through the methods
            foreach (var aMethod in aClass.Methods)
            {
                MemberDeclarationSyntax method = BuildMethod(aMethod);

                if (method != null)
                    returnMembers = returnMembers.Add(method);
            }

            return returnMembers;
        }

        /// <summary>
        /// Build a method definition
        /// </summary>
        /// <param name="aMethod"></param>
        /// <returns></returns>
        private static MemberDeclarationSyntax BuildMethod(ApiMethod aMethod)
        {
            //create the method defintion instance
            var method = SyntaxFactory.MethodDeclaration
            (
                SyntaxFactory.IdentifierName
                (
                    SyntaxFactory.Identifier
                    (
                        SyntaxFactory.TriviaList(),
                        aMethod.ReturnType,
                        SyntaxFactory.TriviaList
                        (
                            SyntaxFactory.Space
                        )
                    )
                ),
                SyntaxFactory.Identifier(aMethod.Name)
            );

            method = method.WithLeadingTrivia(SyntaxFactory.LineFeed, SyntaxFactory.Tab);

            //container for the attributes
            var attrs = SyntaxFactory.List<AttributeListSyntax>();

            //check and add Abstract attribute if enabled
            if (aMethod.IsAbstract)
            {
                var attribList = BuildSimpleAttribute("Abstract", true);

                attrs = attrs.Add(attribList);
            }

            //check and add Static attribute if enabled
            if (aMethod.IsStatic)
            {
                var attribList = BuildSimpleAttribute("Static", true);

                attrs = attrs.Add(attribList);
            }

            //check and add DesignatedInitializer attribute if enabled
            if (aMethod.DesignatedInitializer)
            {
                var attribList = BuildSimpleAttribute("DesignatedInitializer", true);

                attrs = attrs.Add(attribList);
            }

            //check and add RequiresSuper attribute if enabled
            if (aMethod.RequiresSuper)
            {
                var attribList = BuildSimpleAttribute("RequiresSuper", true);

                attrs = attrs.Add(attribList);
            }

            if (!string.IsNullOrWhiteSpace(aMethod.ExportName))
            {
                var atrib = SyntaxFactory.Attribute
                        (
                            SyntaxFactory.IdentifierName("Export")
                        );

                var nodes = new List<SyntaxNodeOrToken>()
                                    {
                                        SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.LiteralExpression
                                            (
                                                SyntaxKind.StringLiteralExpression,
                                                SyntaxFactory.Literal(aMethod.ExportName)
                                            )
                                        ),
                                    };

                var atrlibs = SyntaxFactory.AttributeArgumentList().WithArguments(SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(nodes));

                atrib = atrib.WithArgumentList(atrlibs);
                attrs = attrs.Add(SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(atrib))
                    .WithOpenBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.LineFeed,
                                    SyntaxFactory.Whitespace("    ")
                                ),
                                SyntaxKind.OpenBracketToken,
                                SyntaxFactory.TriviaList()
                            )
                        ));
            }

            //check and add IsNullAllowed attribute if enabled
            if (aMethod.IsNullAllowed)
            {
                attrs = attrs.Add(SyntaxFactory.AttributeList
                (
                    SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                    (
                        SyntaxFactory.Attribute
                        (
                            SyntaxFactory.IdentifierName("NullAllowed")
                        )
                    )
                )
                .WithTarget
                (
                    SyntaxFactory.AttributeTargetSpecifier
                    (
                        SyntaxFactory.Token(SyntaxKind.ReturnKeyword)
                    )
                )
                .WithOpenBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.Whitespace("    ")
                                ),
                                SyntaxKind.OpenBracketToken,
                                SyntaxFactory.TriviaList()
                            )
                        )
                        .WithCloseBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList(),
                                SyntaxKind.CloseBracketToken,
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.LineFeed
                                )
                            )
                        ));
            }

            //Check for parameters and them to the defintio
            if (aMethod.Parameters?.Count > 0)
            {
                var pars = BuildMethodParameters(aMethod.Parameters);

                method = method.WithParameterList(pars);
            }

            //add the attributes to the definition
            method = method.WithAttributeLists(attrs);

            //
            

            method = method.WithSemicolonToken
            (
                SyntaxFactory.Token(SyntaxKind.SemicolonToken)
            );

            method = method.WithTrailingTrivia(SyntaxFactory.LineFeed);

            return method;

        }

        /// <summary>
        /// Build a property definition
        /// </summary>
        /// <param name="aProperty"></param>
        /// <returns></returns>
        private static MemberDeclarationSyntax BuildProperty(ApiProperty aProperty)
        {
            
            var property = SyntaxFactory.PropertyDeclaration
            (
                SyntaxFactory.IdentifierName
                (
                    SyntaxFactory.Identifier
                    (
                        SyntaxFactory.TriviaList(),
                        aProperty.Type,
                        SyntaxFactory.TriviaList
                        (
                            SyntaxFactory.Space
                        )
                    )
                ),

                SyntaxFactory.Identifier(aProperty.Name)
            );

            property = property.WithLeadingTrivia(SyntaxFactory.LineFeed, SyntaxFactory.Tab);

            var attrs = SyntaxFactory.List<AttributeListSyntax>();

            if (!string.IsNullOrWhiteSpace(aProperty.WrapName))
            {
                attrs = attrs.Add(SyntaxFactory.AttributeList
                        (
                            SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                            (
                                SyntaxFactory.Attribute
                                (
                                    SyntaxFactory.IdentifierName("Wrap")
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
                                                    SyntaxKind.StringLiteralExpression,
                                                    SyntaxFactory.Literal(aProperty.WrapName)
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                        .WithOpenBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.Whitespace("    ")
                                ),
                                SyntaxKind.OpenBracketToken,
                                SyntaxFactory.TriviaList()
                            )
                        )
                        .WithCloseBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList(),
                                SyntaxKind.CloseBracketToken,
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.LineFeed
                                )
                            )
                        ));
            }

            if (aProperty.IsAbstract)
            {
                var attribList = BuildSimpleAttribute("Abstract", true);

                attrs = attrs.Add(attribList);
            }

            if (aProperty.IsNullAllowed)
            {
                attrs = attrs.Add(BuildSimpleAttribute("NullAllowed", true));
            }
            
            if (aProperty.IsObsolete)
            {
                attrs = attrs.Add(SyntaxFactory.AttributeList
                        (
                            SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                            (
                                SyntaxFactory.Attribute
                                (
                                    SyntaxFactory.IdentifierName("Obsolete")
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
                                                    SyntaxKind.StringLiteralExpression,
                                                    SyntaxFactory.Literal("This is obsolete")
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        ).WithOpenBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.Whitespace("    ")
                                ),
                                SyntaxKind.OpenBracketToken,
                                SyntaxFactory.TriviaList()
                            )
                        )
                        .WithCloseBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList(),
                                SyntaxKind.CloseBracketToken,
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.LineFeed
                                )
                            )
                        ));
            }

            if (aProperty.IsStatic)
            {
                var attribList = BuildSimpleAttribute("Static", true);

                attrs = attrs.Add(attribList);
            }

            if (aProperty.Verify != null)
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
                                        SyntaxFactory.IdentifierName(aProperty.Verify.VerifyType)
                                    )
                                )
                            )
                        )
                    )
                ).WithOpenBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.Whitespace("    ")
                                ),
                                SyntaxKind.OpenBracketToken,
                                SyntaxFactory.TriviaList()
                            )
                        )
                        .WithCloseBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList(),
                                SyntaxKind.CloseBracketToken,
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.LineFeed
                                )
                            )
                        );

                attrs = attrs.Add(attribList);
            }

            if (aProperty.FieldParams != null && aProperty.FieldParams.Any())
            {
                var atrib = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Export"));

                var nodes = new List<SyntaxNodeOrToken>(){};

                var pars = aProperty.FieldParams.Split(',');

                foreach (var fieldname in pars)
                {
                    nodes.Add(SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.LiteralExpression
                                            (
                                                SyntaxKind.StringLiteralExpression,
                                                SyntaxFactory.Literal(fieldname)
                                            )
                                        ));

                    //if the field is not the last item then add a comma
                    if (pars.Last() != fieldname)
                        nodes.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
                }

                var atrlibs = SyntaxFactory.AttributeArgumentList().WithArguments(SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(nodes));

                atrib = atrib.WithArgumentList(atrlibs);
                attrs = attrs.Add(SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(atrib)).WithOpenBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.Whitespace("    ")
                                ),
                                SyntaxKind.OpenBracketToken,
                                SyntaxFactory.TriviaList()
                            )
                        )
                        .WithCloseBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList(),
                                SyntaxKind.CloseBracketToken,
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.LineFeed
                                )
                            )
                        ));
            }

            if (!string.IsNullOrWhiteSpace(aProperty.ExportName))
            {
                var atrib = SyntaxFactory.Attribute
                        (
                            SyntaxFactory.IdentifierName("Export")
                        );

                var nodes = new List<SyntaxNodeOrToken>()
                                    {
                                        SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.LiteralExpression
                                            (
                                                SyntaxKind.StringLiteralExpression,
                                                SyntaxFactory.Literal(aProperty.ExportName)
                                            )
                                        ),
                                    };

                if (!string.IsNullOrWhiteSpace(aProperty.SemanticStrength))
                {

                    nodes.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
                    nodes.Add(SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.MemberAccessExpression
                                            (
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.IdentifierName("ArgumentSemantic"),
                                                SyntaxFactory.IdentifierName(aProperty.SemanticStrength)
                                            )
                                        ));
                }

                var atrlibs = SyntaxFactory.AttributeArgumentList().WithArguments(SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(nodes));

                atrib = atrib.WithArgumentList(atrlibs);
                attrs = attrs.Add(SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(atrib))
                     .WithOpenBracketToken
                        (
                            SyntaxFactory.Token
                            (
                                SyntaxFactory.TriviaList
                                (
                                    SyntaxFactory.LineFeed,
                                    SyntaxFactory.Whitespace("    ")
                                ),
                                SyntaxKind.OpenBracketToken,
                                SyntaxFactory.TriviaList()
                            )
                        ));
            }

            var accessors = SyntaxFactory.AccessorList();

            if (aProperty.CanGet)
            {
                SyntaxList<AttributeListSyntax> attrslist;

                if (!string.IsNullOrWhiteSpace(aProperty.GetBindName))
                {
                    attrslist = SyntaxFactory.SingletonList<AttributeListSyntax>
                            (
                                SyntaxFactory.AttributeList
                                (
                                    SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                                    (
                                        SyntaxFactory.Attribute
                                        (
                                            SyntaxFactory.IdentifierName("Bind")
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
                                                            SyntaxKind.StringLiteralExpression,
                                                            SyntaxFactory.Literal(aProperty.GetBindName)
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            );
                }

                accessors = accessors
                    .AddAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithAttributeLists(attrslist)
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            }

            if (aProperty.CanSet)
            {
                SyntaxList<AttributeListSyntax> attrslist;

                if (!string.IsNullOrWhiteSpace(aProperty.SetBindName))
                {
                    attrslist = SyntaxFactory.SingletonList<AttributeListSyntax>
                            (
                                SyntaxFactory.AttributeList
                                (
                                    SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                                    (
                                        SyntaxFactory.Attribute
                                        (
                                            SyntaxFactory.IdentifierName("Bind")
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
                                                            SyntaxKind.StringLiteralExpression,
                                                            SyntaxFactory.Literal(aProperty.SetBindName)
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            );
                }

                accessors = accessors
                    .AddAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .WithAttributeLists(attrslist)
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            }

            property =  property.WithAttributeLists(attrs);
            property =  property.WithAccessorList(accessors);

            
            property = property.WithTrailingTrivia(SyntaxFactory.LineFeed);

            return property;
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
                    SyntaxFactory.IdentifierName
                                (
                                    SyntaxFactory.Identifier
                                    (
                                        SyntaxFactory.TriviaList(SyntaxFactory.Space),
                                        basetype.Name,
                                        SyntaxFactory.TriviaList
                                        (
                                        )
                                    )
                                )
                    ));


                if (basetype != aClass.Implements.Last())
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));

            }
        
            return SyntaxFactory.BaseList(SyntaxFactory.SeparatedList<BaseTypeSyntax>(tokens))
                .WithTrailingTrivia(SyntaxFactory.LineFeed);

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
                var attribList = BuildSimpleAttribute("Protocol", false);


                attribs = attribs.Add(attribList);

            }

            if (apiClass.IsCategory)
            {
                var attribList = BuildSimpleAttribute("Category", false);

                attribs = attribs.Add(attribList);


            }

            if (apiClass.IsStatic)
            {
                var attribList = BuildSimpleAttribute("Static", false);

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
                    )
                    .WithOpenBracketToken
                    (
                        SyntaxFactory.Token
                        (
                            SyntaxFactory.TriviaList
                            (
                                SyntaxFactory.LineFeed
                            ),
                            SyntaxKind.OpenBracketToken,
                            SyntaxFactory.TriviaList()
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
                            )
                            .WithOpenBracketToken
                            (
                                SyntaxFactory.Token
                                (
                                    SyntaxFactory.TriviaList
                                    (
                                        SyntaxFactory.LineFeed
                                    ),
                                    SyntaxKind.OpenBracketToken,
                                    SyntaxFactory.TriviaList()
                                )
                            );

                attribs = attribs.Add(atrs);



            }

            if (apiClass.DisableDefaultCtor)
            {
                var attribList = BuildSimpleAttribute("DisableDefaultCtor", false);

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
                                                    SyntaxFactory.IdentifierName(apiClass.Verify.VerifyType)
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                            .WithCloseBracketToken
                            (
                                SyntaxFactory.Token
                                (
                                    SyntaxFactory.TriviaList(),
                                    SyntaxKind.CloseBracketToken,
                                    SyntaxFactory.TriviaList
                                    (
                                        SyntaxFactory.LineFeed
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
                            SyntaxFactory.IdentifierName
                                (
                                    SyntaxFactory.Identifier
                                    (
                                        SyntaxFactory.TriviaList(),
                                        aParam.Type,
                                        SyntaxFactory.TriviaList
                                        (
                                            SyntaxFactory.Space
                                        )
                                    )
                                )
                        ));

                if (aParam != parameters.Last())
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
            }

            return SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList<ParameterSyntax>(tokens));

        }

        /// <summary>
        /// Build the parameters for the method
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static ParameterListSyntax BuildMethodParameters(List<ApiParameter> parameters)
        {
            var parList = new List<ParameterSyntax>();

            foreach (var aParam in parameters)
            {
                var newPar = SyntaxFactory.Parameter
                     (
                         SyntaxFactory.Identifier(aParam.Name)
                     )
                     .WithType
                     (
                          SyntaxFactory.IdentifierName
                                (
                                    SyntaxFactory.Identifier
                                    (
                                        SyntaxFactory.TriviaList(),
                                        aParam.Type,
                                        SyntaxFactory.TriviaList
                                        (
                                            SyntaxFactory.Space
                                        )
                                    )
                                )
                     );

                if (aParam.IsNullAllowed)
                {
                    newPar = newPar.WithAttributeLists(SyntaxFactory.SingletonList<AttributeListSyntax>
                        (
                            SyntaxFactory.AttributeList
                            (
                                SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                                (
                                    SyntaxFactory.Attribute
                                    (
                                        SyntaxFactory.IdentifierName("NullAllowed")
                                    )
                                )
                            )
                        ));
                }

                parList.Add(newPar);
            }

            var pars = SyntaxFactory.ParameterList();
            pars = pars.AddParameters(parList.ToArray());

            return pars;

        }

        #region Resuable Methods

        /// <summary>
        /// Build a simple attribute
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static AttributeListSyntax BuildSimpleAttribute(string name, bool withTab)
        {
            var attribList = SyntaxFactory.AttributeList
               (
                   SyntaxFactory.SingletonSeparatedList<AttributeSyntax>
                   (
                       SyntaxFactory.Attribute
                       (
                           SyntaxFactory.IdentifierName(name)
                       )
                   )

               );

            if (withTab)
            {
               return attribList.WithOpenBracketToken
                (
                    SyntaxFactory.Token
                    (
                        SyntaxFactory.TriviaList
                        (
                            SyntaxFactory.LineFeed,
                            SyntaxFactory.Whitespace("    ")
                        ),
                        SyntaxKind.OpenBracketToken,
                        SyntaxFactory.TriviaList()
                    )
                );


               
            }
            else
            {
                return attribList.WithOpenBracketToken
                (
                    SyntaxFactory.Token
                    (
                        SyntaxFactory.TriviaList
                        (
                            SyntaxFactory.LineFeed
                        ),
                        SyntaxKind.OpenBracketToken,
                        SyntaxFactory.TriviaList()
                    )
                );
            }


        }


        #endregion
        #endregion
    }
}
