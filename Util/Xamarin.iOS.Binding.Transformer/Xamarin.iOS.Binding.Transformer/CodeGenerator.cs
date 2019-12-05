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
        private const string _whitespace = "    ";

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

            if (aClass.IsPartial)
            {
                interfaceDeclaration = interfaceDeclaration.WithModifiers(SyntaxFactory.TokenList
                (
                    SyntaxFactory.Token
                    (
                        SyntaxFactory.TriviaList(),
                        SyntaxKind.PartialKeyword,
                        SyntaxFactory.TriviaList
                        (
                            SyntaxFactory.Space
                        )
                    )

                ));
            }

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

            

            ///formatting
            interfaceDeclaration = interfaceDeclaration.AddTrailingTrivia(true);

            interfaceDeclaration = interfaceDeclaration.ApplyOpenBraceToken();

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

            if (!string.IsNullOrWhiteSpace(aMethod.Advice))
            {
                var attrib = BuildStringLiteralAttribute("Advice", aMethod.Advice);

                attrs = attrs.Add(attrib.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace));
            }

            if (!string.IsNullOrWhiteSpace(aMethod.EventArgs))
            {
                var attrib = BuildStringLiteralAttribute("EventArgs", aMethod.EventArgs);

                attrs = attrs.Add(attrib.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace));
            }

            if (!string.IsNullOrWhiteSpace(aMethod.EventName))
            {
                var attrib = BuildStringLiteralAttribute("EventName", aMethod.EventName);

                attrs = attrs.Add(attrib.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace));
            }

            if (!string.IsNullOrWhiteSpace(aMethod.DefaultValue))
            {
                var attrib = BuildDefaultValueAttrib(aMethod.DefaultValue);

                attrs = attrs.Add(attrib);
            }

            if (!string.IsNullOrWhiteSpace(aMethod.WrapName))
            {
                var attrib = BuildStringLiteralAttribute("Wrap", aMethod.WrapName);

                attrs = attrs.Add(attrib.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace));
            }

            if (!string.IsNullOrWhiteSpace(aMethod.Obsolete))
            {
                var attribs = BuildStringLiteralAttribute("Obsolete", aMethod.Obsolete);

                attrs = attrs.Add(attribs.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace));
            }

            if (!string.IsNullOrWhiteSpace(aMethod.DelegateName))
            {
                var attrib = BuildStringLiteralAttribute("DelegateName", aMethod.DelegateName);

                attrs = attrs.Add(attrib.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace));
            }

            //check and add Static attribute if enabled
            if (aMethod.IsStatic)
            {
                var attribList = BuildSimpleAttribute("Static", true);

                attrs = attrs.Add(attribList);
            }

            if (aMethod.IsNew)
            {
                var attribList = BuildSimpleAttribute("New", true);

                attrs = attrs.Add(attribList);
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
                .AddOpenBracketToken(true, _whitespace));
                        
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

            //check and add Abstract attribute if enabled
            if (aMethod.IsAbstract)
            {
                var attribList = BuildSimpleAttribute("Abstract", true);

                attrs = attrs.Add(attribList);
            }

            if (!string.IsNullOrWhiteSpace(aMethod.IosVersion))
            {
                var attribList = BuildXamarinAttribute(aMethod.IosVersion, aMethod.Introduced);

                attrs = attrs.Add(attribList);
            }

            if (!string.IsNullOrWhiteSpace(aMethod.TVVersion))
            {
                var attribList = BuildXamarinAttribute(aMethod.TVVersion, aMethod.Introduced, true);

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

                SyntaxFactory.Identifier
                    (
                        SyntaxFactory.TriviaList(),
                        aProperty.Name,
                        SyntaxFactory.TriviaList
                        (
                            SyntaxFactory.Space
                        )
                    )
            );

            property = property.WithLeadingTrivia(SyntaxFactory.LineFeed, SyntaxFactory.Tab);

            var attrs = SyntaxFactory.List<AttributeListSyntax>();

            if (!string.IsNullOrWhiteSpace(aProperty.WrapName))
            {
                attrs = attrs.Add(BuildStringLiteralAttribute("Wrap", aProperty.WrapName)
                    .AsAtrributeListSyntax()
                    .AddOpenBracketToken(whitespace: _whitespace));
            }

            if (!string.IsNullOrWhiteSpace(aProperty.Obsolete))
            {
                var attribs = BuildStringLiteralAttribute("Obsolete", aProperty.Obsolete);

                attrs = attrs.Add(attribs.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace));
            }

            if (aProperty.IsNew)
            {
                var attribList = BuildSimpleAttribute("New", true);

                attrs = attrs.Add(attribList);
            }

            if (aProperty.IsInternal)
            {
                var attribList = BuildSimpleAttribute("Internal", true);

                attrs = attrs.Add(attribList);
            }

            if (aProperty.ShouldNotify)
            {
                var attribList = BuildSimpleAttribute("Notification", true);

                attrs = attrs.Add(attribList);
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
            
            

            if (aProperty.IsStatic)
            {
                var attribList = BuildSimpleAttribute("Static", true);

                attrs = attrs.Add(attribList);
            }

            if (aProperty.Verify != null && !string.IsNullOrWhiteSpace(aProperty.VerifyType))
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
                                    SyntaxFactory.LineFeed,
                                    SyntaxFactory.Whitespace("    ")
                                ),
                                SyntaxKind.OpenBracketToken,
                                SyntaxFactory.TriviaList()
                            )
                        );

                attrs = attrs.Add(attribList);
            }

            if (aProperty.FieldParams != null && aProperty.FieldParams.Any())
            {
                var atrib = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Field"));

                var nodes = new List<SyntaxNodeOrToken>(){};

                var pars = aProperty.FieldParams.Split(',');

                foreach (var fieldname in pars)
                {
                    nodes.Add(SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.LiteralExpression
                                            (
                                                SyntaxKind.StringLiteralExpression,
                                                SyntaxFactory.Literal(fieldname.Trim())
                                            )
                                        ));

                    //if the field is not the last item then add a comma
                    if (pars.Last() != fieldname)
                        nodes.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
                }

                var atrlibs = SyntaxFactory.AttributeArgumentList().WithArguments(SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(nodes));

                atrib = atrib.WithArgumentList(atrlibs);

                attrs = attrs.Add(atrib.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace));
            }

            if (!string.IsNullOrWhiteSpace(aProperty.IosVersion))
            {
                var attribList = BuildXamarinAttribute(aProperty.IosVersion, aProperty.Introduced);

                attrs = attrs.Add(attribList);
            }

            if (!string.IsNullOrWhiteSpace(aProperty.TVVersion))
            {
                var attribList = BuildXamarinAttribute(aProperty.TVVersion, aProperty.Introduced, true);

                attrs = attrs.Add(attribList);
            }

            

            if (!string.IsNullOrWhiteSpace(aProperty.ExportName))
            {
                var nodes = new List<SyntaxNodeOrToken>();

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

                var atrib = BuildStringLiteralAttribute("Export", aProperty.ExportName, nodes);

                //var atrlibs = SyntaxFactory.AttributeArgumentList().WithArguments(SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(nodes));

                //atrib = atrib.WithArgumentList(atrlibs);

                attrs = attrs.Add(atrib.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace));
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
                    .AddLeadingTrivia(space: true)
                    .WithAttributeLists(attrslist)
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                    .AddTrailingTrivia(space: true));
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
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                    .AddTrailingTrivia(space: true));
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

            if (!string.IsNullOrWhiteSpace(apiClass.Obsolete))
            {
                var attribList = BuildStringLiteralAttribute("Obsolete", apiClass.Obsolete);

                attribs = attribs.Add(attribList.AsAtrributeListSyntax().AddOpenBracketToken(true));
            }

            if (!string.IsNullOrWhiteSpace(apiClass.Advice))
            {
                var attribList = BuildStringLiteralAttribute("Advice", apiClass.Advice);

                attribs = attribs.Add(attribList.AsAtrributeListSyntax().AddOpenBracketToken(true));
            }


            if (apiClass.IsProtocol)
            {

                var btypeDef = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Protocol"));

                var tokens = new List<SyntaxNodeOrToken>();

                
                if (!string.IsNullOrWhiteSpace(apiClass.ProtocolName))
                {
                    //add the return type
                    tokens.Add(BuildStringLiteralArgument("Name", apiClass.ProtocolName));

                    btypeDef = btypeDef.WithArgumentTokens(tokens);
                }
 

                attribs = attribs.Add(btypeDef.AsAtrributeListSyntax().AddOpenBracketToken());

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

            if (apiClass.DisableDefaultCtor)
            {
                var attribList = BuildSimpleAttribute("DisableDefaultCtor", false);

                attribs = attribs.Add(attribList);
            }

            if (apiClass.Verify != null && !string.IsNullOrWhiteSpace(apiClass.VerifyType))
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
                var baseTypeAtrribs = BuildClassBaseTypeAttribs(apiClass);

                attribs = attribs.Add(baseTypeAtrribs);
            }

            return attribs;
        }

        private static AttributeListSyntax BuildClassBaseTypeAttribs(ApiClass apiClass)
        {
            var btypeDef = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("BaseType"));

            var tokens = new List<SyntaxNodeOrToken>();

            if (!string.IsNullOrWhiteSpace(apiClass.BaseType.TypeName))
            {
                //add the return type
                tokens.Add(SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.TypeOfExpression
                                            (
                                                SyntaxFactory.IdentifierName(apiClass.BaseType.TypeName)
                                            )
                                        ));
            }

            if (!string.IsNullOrWhiteSpace(apiClass.BaseType.Name))
            {
                //add a comma if the Name is not the first attribute argument
                if (tokens.Count > 0)
                {
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));

                }

                //now add the native name
                tokens.Add(BuildStringLiteralArgument("Name", apiClass.BaseType.Name, true, true));
            }

            if (!string.IsNullOrWhiteSpace(apiClass.BaseType.DelegateName))
            {
                //add a comma if the Delegate is not the first attribute argument
                if (tokens.Count > 0)
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));

                tokens.Add(SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.ImplicitArrayCreationExpression
                                            (
                                                SyntaxFactory.InitializerExpression
                                                (
                                                    SyntaxKind.ArrayInitializerExpression,
                                                    SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>
                                                    (
                                                        SyntaxFactory.LiteralExpression
                                                        (
                                                            SyntaxKind.StringLiteralExpression,
                                                            SyntaxFactory.Literal(apiClass.BaseType.DelegateName)
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                        .WithNameEquals
                                        (
                                            SyntaxFactory.NameEquals
                                            (
                                                SyntaxFactory.IdentifierName("Delegates")
                                            )
                                        ).AddLeadingTrivia(true, true));
            }

            if (!string.IsNullOrWhiteSpace(apiClass.BaseType.EventsType))
            {
                //add a comma if the Delegate is not the first attribute argument
                if (tokens.Count > 0)
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));

                tokens.Add(SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.ImplicitArrayCreationExpression
                                            (
                                                SyntaxFactory.InitializerExpression
                                                (
                                                    SyntaxKind.ArrayInitializerExpression,
                                                    SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>
                                                    (
                                                        SyntaxFactory.TypeOfExpression
                                                        (
                                                            SyntaxFactory.IdentifierName(apiClass.BaseType.EventsType)
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                        .WithNameEquals
                                        (
                                            SyntaxFactory.NameEquals
                                            (
                                                SyntaxFactory.IdentifierName("Events")
                                            )
                                        ).AddLeadingTrivia(true, true));
            }

            var btype = btypeDef.WithArgumentTokens(tokens);

            return btype.AsAtrributeListSyntax().AddOpenBracketToken();
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
                var paramType = (aParam.IsReference) ? $"ref {aParam.Type}" : aParam.Type;

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
                                        paramType,
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
                var firstParTriv = (aParam == parameters.First()) ? SyntaxFactory.TriviaList() : SyntaxFactory.TriviaList(SyntaxFactory.Space);

                var paramType = (aParam.IsReference) ? $"ref {aParam.Type}" : aParam.Type;

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
                                        firstParTriv,
                                        paramType,
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
                            .WithCloseBracketToken
                            (
                                SyntaxFactory.Token
                                (
                                    SyntaxFactory.TriviaList(),
                                    SyntaxKind.CloseBracketToken,
                                    SyntaxFactory.TriviaList
                                    (
                                        SyntaxFactory.Space
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

        /// <summary>
        /// Builds the xamarin platform attributes.
        /// </summary>
        /// <param name="TvNotiOs">if set to <c>true</c> TvOs not iOS</param>
        /// <param name="versionNumber">The version number.</param>
        /// <param name="introduced">if set to <c>true</c> [introduced].</param>
        /// <returns></returns>
        private static AttributeListSyntax BuildXamarinAttribute(string versionNumber, bool introduced, bool TvNotiOs = false)
        {
            if (!introduced)
            {
                var platform = (TvNotiOs) ? "TV" : "iOS";

                var attrib = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(platform));

                var nodes = new List<SyntaxNodeOrToken>();

                var attrs = versionNumber.Split(",");

                foreach (var anumber in attrs)
                {
                    nodes.Add(SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.LiteralExpression
                                            (
                                                SyntaxKind.NumericLiteralExpression,
                                                SyntaxFactory.Literal(int.Parse(anumber))
                                            )
                                        ));

                    if (anumber != attrs.Last())
                        nodes.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
                }

                attrib = attrib.WithArgumentTokens(nodes);

                return attrib.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace);
            }
            else
            {
                var platform = (TvNotiOs) ? "TvOS" : "iOS";

                var attrib = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Introduced"));

                var nodes = new List<SyntaxNodeOrToken>();

                nodes.Add(SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.MemberAccessExpression
                                            (
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.IdentifierName("PlatformName"),
                                                SyntaxFactory.IdentifierName(platform)
                                            )
                                        ));

                nodes.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));

                var attrs = versionNumber.Split(",");

                foreach (var anumber in attrs)
                {
                    nodes.Add(SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.LiteralExpression
                                            (
                                                SyntaxKind.NumericLiteralExpression,
                                                SyntaxFactory.Literal(int.Parse(anumber))
                                            )
                                        ));

                    if (anumber != attrs.Last())
                        nodes.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
                }

                attrib = attrib.WithArgumentTokens(nodes);

                return attrib.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace);
            }
        }

        private static AttributeListSyntax BuildDefaultValueAttrib(string defaultValue)
        {
            if (defaultValue.Equals("none", StringComparison.OrdinalIgnoreCase))
            {
                var attr = BuildSimpleAttribute("NoDefaultValue", true);

                return attr;
            }
            else
            {
                var nodes = new List<SyntaxNodeOrToken>();

                switch (defaultValue.ToLower())
                {
                    case "true":
                        {
                            nodes.Add(SyntaxFactory.AttributeArgument
                                    (
                                        SyntaxFactory.LiteralExpression
                                        (
                                            SyntaxKind.TrueLiteralExpression
                                        )
                                    ));
                        }
                        break;
                    case "false":
                        {
                            nodes.Add(SyntaxFactory.AttributeArgument
                                    (
                                        SyntaxFactory.LiteralExpression
                                        (
                                            SyntaxKind.FalseLiteralExpression
                                        )
                                    ));
                        }
                        break;
                    case "null":
                        {
                            nodes.Add(SyntaxFactory.AttributeArgument
                                    (
                                        SyntaxFactory.LiteralExpression
                                        (
                                            SyntaxKind.NullLiteralExpression
                                        )
                                    ));
                        }
                        break;
                }

                var attrib = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("DefaultValue"));
                attrib = attrib.WithArgumentTokens(nodes);

                return attrib.AsAtrributeListSyntax().AddOpenBracketToken(true, _whitespace);

            }
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
                            SyntaxFactory.Whitespace(_whitespace)
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

        private static AttributeSyntax BuildStringLiteralAttribute(string name, string literalValue, IEnumerable<SyntaxNodeOrToken> additionalNodes = null)
        {
            var atrib = SyntaxFactory.Attribute
                        (
                            SyntaxFactory.IdentifierName(name)

                        );

            var nodes = new List<SyntaxNodeOrToken>();

            nodes.Add(SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.LiteralExpression
                                            (
                                                SyntaxKind.StringLiteralExpression,
                                                SyntaxFactory.Literal(literalValue)
                                            )
                                        ));


            if (additionalNodes != null && additionalNodes.Count() > 0)
                nodes.AddRange(additionalNodes);

            var atrlibs = SyntaxFactory.AttributeArgumentList().WithArguments(SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(nodes));

            atrib = atrib.WithArgumentList(atrlibs);

            return atrib;
        }

        private static AttributeArgumentSyntax BuildStringLiteralArgument(string name, string literalValue, bool newLine = false, bool tab = false)
        {
            var attrib = SyntaxFactory.AttributeArgument
                                        (
                                            SyntaxFactory.LiteralExpression
                                            (
                                                SyntaxKind.StringLiteralExpression,
                                                SyntaxFactory.Literal(literalValue)
                                            )
                                        )
                                        .WithNameEquals
                                        (
                                            SyntaxFactory.NameEquals
                                            (
                                                SyntaxFactory.IdentifierName(name)
                                            )
                                        );

            return attrib.AddLeadingTrivia(newLine, tab);
        }
        #endregion
        #endregion
    }
}
