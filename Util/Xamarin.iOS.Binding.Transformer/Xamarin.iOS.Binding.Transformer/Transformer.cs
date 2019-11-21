using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Xamarin.iOS.Binding.Transformer
{
    public class Transformer
    {

        /// <summary>
        /// Extract the CSharp file into a xml file
        /// </summary>
        /// <param name="inputFile"></param>
        public static async Task<ApiDefinition> ExtractDefinitionAsync(string inputFile)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputFile) || !File.Exists(inputFile))
                    throw new Exception("Invalid input file");


                var lines = File.ReadAllText(inputFile);
                var tree = CSharpSyntaxTree.ParseText(lines);

                // Get the root CompilationUnitSyntax.
                var root = await tree.GetRootAsync().ConfigureAwait(false) as CompilationUnitSyntax;

                

                var output = new ApiDefinition();

                Process(root, ref output);

                output.UpdateHierachy();

                return output;

            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public static ApiDefinition Load(string inputFile)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputFile) || !File.Exists(inputFile))
                    throw new Exception("Invalid input file");

                var serializer = new XmlSerializer(typeof(ApiDefinition));

                var output = new ApiDefinition();

                using (StreamReader str = new StreamReader(inputFile))
                {
                    
                    output = (ApiDefinition)serializer.Deserialize(str);
                }

                return output;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Private Methods

        private static void Process(CompilationUnitSyntax unit, ref ApiDefinition output)
        {

            var children = unit.ChildNodes().ToList();

            ProcessTree(children, ref output);
        }

        private static void ProcessTree(List<SyntaxNode> tree, ref ApiDefinition output, ApiNamespace apiNamespace = null)
        {
            var @namespace = (apiNamespace != null) ? apiNamespace : new ApiNamespace();

            foreach (var aChild in tree)
            {
                if (aChild is NamespaceDeclarationSyntax)
                {
                    var clnamespace = ((IdentifierNameSyntax)((NamespaceDeclarationSyntax)aChild).Name).Identifier.Text;

                    @namespace.Name = clnamespace;

                    if (tree.Last() == aChild)
                    {
                        var classes = aChild.ChildNodes().ToList();

                        ProcessTree(classes, ref output, @namespace);
                    }
                }
                else if (aChild is UsingDirectiveSyntax)
                {
                    try
                    {
                        var usiDir = (UsingDirectiveSyntax)aChild;

                        if (usiDir.Name is IdentifierNameSyntax)
                        {
                            var usingVal = ((IdentifierNameSyntax)usiDir.Name).Identifier.Text;

                            output.Usings.Items.Add(new ApiUsing() { Name = usingVal });
                        }
                        else if (usiDir.Name is QualifiedNameSyntax)
                        {
                            var usingVal = usiDir.GetText().ToString().Replace("using", "").Replace(";","").Trim();

                            output.Usings.Items.Add(new ApiUsing() { Name = usingVal });

                        }
                        else
                        {
                            throw new Exception("unexpected using type");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    

                }
                else if (aChild is DelegateDeclarationSyntax)
                {
                    ApiDelegate delegateEntry = BuildDelegate((DelegateDeclarationSyntax)aChild);

                    @namespace.Delegates.Add(delegateEntry);

                }
                else if (aChild is InterfaceDeclarationSyntax)
                {
                    var classentry = BuildClass((InterfaceDeclarationSyntax)aChild);

                    var existing = @namespace.Types.FirstOrDefault(x => x.Name.Equals(classentry.Name));

                    if (existing != null)
                    {
                        //Console.WriteLine($"Found duplicate class: {classentry.Name}, Merging");

                        existing.Merge(classentry);
                        
                    }
                    else
                    {
                        @namespace.Types.Add(classentry);
                    }

                   
                }
            }

            if (!output.Namespaces.Contains(@namespace))
                output.Namespaces.Add(@namespace);
        }

        #region Build Methods

        private static ApiDelegate BuildDelegate(DelegateDeclarationSyntax node)
        {
            //build new delegete definition
            var newDelegate = new ApiDelegate()
            {
                Name = node.Identifier.Text,
                ReturnType = ((PredefinedTypeSyntax)node.ReturnType).Keyword.Text,
            };

            // check if there are parameters
            if (node.ParameterList.Parameters.Any())
            {
                //iterate through each parameter
                foreach (var aParam in node.ParameterList.Parameters)
                {
                    //build the parameter
                    var newParam = BuildParameter(aParam);

                    // add it to the list
                    newDelegate.Parameters.Add(newParam);
                }
            }

            //return delegate definition
            return newDelegate;
        }

        private static ApiClass BuildClass(InterfaceDeclarationSyntax node)
        {
            var newClass = new ApiClass()
            {
                Type = ApiTypeType.Interface,
                Name = node.Identifier.Text,
            };

            //check for modifiers
            if (node.Modifiers.Any())
            {
                foreach (var aMod in node.Modifiers)
                {
                    var modName = aMod.Text;

                    switch (modName.ToLower())
                    {
                        case "partial":
                            {
                                newClass.IsPartial = true;
                            }
                            break;
                        default:
                            {
                                Console.WriteLine($"Unexpected class modifier {modName}");
                            }
                            break;
                    }
                }
            }

            if (node.BaseList != null && node.BaseList.Types.Any())
            {
                foreach (var aImpl in node.BaseList.Types)
                {

                    var newImp = new ApiImplements()
                    {
                        Name = ((IdentifierNameSyntax)aImpl.Type).Identifier.Text,
                    };

                    newClass.Implements.Add(newImp);

                }
            }

            if (node.AttributeLists.Any())
            {
                foreach (var aAtrrib in node.AttributeLists)
                {
                    foreach (var attrib in aAtrrib.Attributes)
                    {
                        if (attrib != null)
                        {
                            var name = ((IdentifierNameSyntax)attrib.Name).Identifier.Text;

                            switch (name.ToLower())
                            {
                                case "basetype":
                                    {
                                        ProcessBaseTypeAttrib(attrib, ref newClass);
                                    }
                                    break;
                                case "model":
                                    {
                                        ProcessModelAttrib(attrib, ref newClass);
                                    }
                                    break;
                                case "disabledefaultctor":
                                    {
                                        newClass.DisableDefaultCtor = true;
                                    }
                                    break;
                                case "category":
                                    {
                                        newClass.IsCategory = true;
                                    }
                                    break;
                                case "protocol":
                                    {
                                        ProcessClassProtocolAttrib(attrib, ref newClass);
                                    }
                                    break;
                                case "static":
                                    {
                                        newClass.IsStatic = true;
                                    }
                                    break;
                                case "verify":
                                    {
                                        ProcessClassVerifyAttrib(attrib, ref newClass);
                                    }
                                    break;
                                case "advice":
                                    {
                                        ProcessClassAdviceAttrib(attrib, ref newClass);
                                    }
                                    break;
                                case "obsolete":
                                    {
                                        ProcessClassObsoleteAttrib(attrib, ref newClass);
                                    }
                                    break;
                                default:
                                    {
                                        Console.WriteLine($"Class attrib not found {name}");
                                    }
                                    break;

                            }


                        }
                    }
                }
            }

            var members = node.ChildNodes();

            if (members.Any())
            {
                foreach (var aMember in members)
                {
                    BuildMembers(aMember, ref newClass);
                }
            }
            return newClass;
        }

        private static void BuildMembers(SyntaxNode node, ref ApiClass apiClass)
        {
            if (node is MethodDeclarationSyntax)
            {
                //build the method definition
                var newMethod = BuildMethod((MethodDeclarationSyntax)node);

                apiClass.Methods.Add(newMethod);
               
            }
            else if (node is PropertyDeclarationSyntax)
            {
                var newProperty = BuildProperty((PropertyDeclarationSyntax)node);

                apiClass.Properties.Add(newProperty);
            }
        }

        private static ApiParameter BuildParameter(ParameterSyntax node)
        {
            //create in instance of the Parameter definition and set the name of the parameter
            var newParam = new ApiParameter()
            {
                Name = node.Identifier.Text,

            };

            newParam.Type = GetType(node.Type);

            if (newParam.Type.StartsWith("*") || node.Modifiers.ToString().Contains("ref"))
            {
                newParam.IsReference = true;
                newParam.Type = newParam.Type.Replace("*", "");

            }

            if (node.AttributeLists.Count > 0)
            {
                ProcessParameterAttributes(node.AttributeLists, ref newParam);
            }
            
            return newParam;
        }

        private static (MemberOverrideAttribute Attribute, string OriginalName) BuildAttributes(AttributeSyntax attrib)
        {
            string originalName = null;

            var attribName = ((IdentifierNameSyntax)attrib.Name).Identifier.Text;

            var newAttrib = new MemberOverrideAttribute()
            {
                Name = attribName ?? string.Empty,
            };

            if (attrib.ArgumentList != null)
            {

                foreach (AttributeArgumentSyntax aArg in attrib.ArgumentList.Arguments)
                {
                    try
                    {
                        if (aArg.Expression is LiteralExpressionSyntax)
                        {
                            var typea = aArg.Expression as LiteralExpressionSyntax;

                            try
                            {
                                var avalue = typea.Kind().ToString();
                                var name = aArg.NameEquals?.Name.Identifier.Text;

                                if (name == null)
                                    name = string.Empty;

                                switch (typea.Kind().ToString())
                                {
                                    case "StringLiteralExpression":
                                        {
                                            var value = typea.Token.Text;

                                            var newArgs = new MemberOverrideArguments()
                                            {
                                                Name = name,
                                                DataType = AttributeDataType.String,
                                                Value = value,
                                            };

                                            if (name.Equals("name", StringComparison.OrdinalIgnoreCase) && (newAttrib.Name.Equals("basetype", StringComparison.OrdinalIgnoreCase)
                                                || (newAttrib.Name.Equals("protocol", StringComparison.OrdinalIgnoreCase))))
                                            {
                                                originalName = value.Replace("\"", "");
                                            }

                                            if (newAttrib.Name.Equals("Export", StringComparison.OrdinalIgnoreCase))
                                            {
                                                originalName = value;
                                            }

                                            newAttrib.Arguments.Add(newArgs);
                                        }
                                        break;
                                    case "TrueLiteralExpression":
                                        {

                                            var value = typea.Token.Text;

                                            var newArgs = new MemberOverrideArguments()
                                            {
                                                Name = name,
                                                DataType = AttributeDataType.Boolean,
                                                Value = value,
                                            };

                                            newAttrib.Arguments.Add(newArgs);
                                        }
                                        break;
                                    case "NullLiteralExpression":
                                        {
                                            var value = typea.Token.Text;

                                            var newArgs = new MemberOverrideArguments()
                                            {
                                                Name = name,
                                                DataType = AttributeDataType.Null,
                                                Value = value,
                                            };

                                            newAttrib.Arguments.Add(newArgs);
                                        }
                                        break;
                                    case "NumericLiteralExpression":
                                        {
                                            var value = typea.Token.Text;

                                            var newArgs = new MemberOverrideArguments()
                                            {
                                                Name = name,
                                                DataType = AttributeDataType.Number,
                                                Value = value,
                                            };

                                            newAttrib.Arguments.Add(newArgs);
                                        }
                                        break;
                                    default:
                                        throw new Exception($"Unhandled literal type: {avalue}");
                                }
                            }
                            catch (Exception ex)
                            {

                                throw;
                            }


                        }
                        else if (aArg.Expression is InitializerExpressionSyntax)
                        {
                            var tyep = aArg.Expression as InitializerExpressionSyntax;

                            Console.WriteLine(tyep.ToString());

                        }
                        else if (aArg.Expression is ImplicitArrayCreationExpressionSyntax)
                        {
                            var tyep = aArg.Expression as ImplicitArrayCreationExpressionSyntax;
                            var name = aArg.NameEquals?.Name.Identifier.Text;

                            var tyepKind = tyep.Kind().ToString();

                            switch (tyepKind)
                            {
                                case "ImplicitArrayCreationExpression":
                                    {
                                        var expression = tyep.Initializer.Expressions.FirstOrDefault();

                                        if (expression != null)
                                        {
                                            var typvalue = expression.Kind().ToString();


                                            switch (expression.Kind().ToString())
                                            {
                                                case "StringLiteralExpression":
                                                    {

                                                        var typea = expression as LiteralExpressionSyntax;
                                                        var value = typea.Token.Text;

                                                        var newArgs = new MemberOverrideArguments()
                                                        {
                                                            Name = name,
                                                            DataType = AttributeDataType.StringArray,
                                                            Value = value,
                                                        };

                                                        newAttrib.Arguments.Add(newArgs);
                                                    }
                                                    break;
                                                case "TypeOfExpression":
                                                    {

                                                        var value = (TypeOfExpressionSyntax)tyep.Initializer.Expressions.First();
                                                        var type = (IdentifierNameSyntax)value.Type;
                                                        var vval = type.Identifier.Text;

                                                        var newArgs = new MemberOverrideArguments()
                                                        {
                                                            Name = name,
                                                            DataType = AttributeDataType.TypeOfArray,
                                                            Value = vval,
                                                        };

                                                        newAttrib.Arguments.Add(newArgs);
                                                    }
                                                    break;

                                            }
                                        }

                                    }
                                    break;
                            }

                        }
                        else if (aArg.Expression is MemberAccessExpressionSyntax)
                        {
                            var tyep = aArg.Expression as MemberAccessExpressionSyntax;
                            var typeValue = tyep.Name.Identifier.Text;

                            var newArgs = new MemberOverrideArguments()
                            {
                                Name = string.Empty,
                                DataType = AttributeDataType.MemberAccess,
                                Value = typeValue,
                            };

                            newAttrib.Arguments.Add(newArgs);
                        }
                        else if (aArg.Expression is TypeOfExpressionSyntax)
                        {
                            var tyep = aArg.Expression as TypeOfExpressionSyntax;
                            var typeValue = ((IdentifierNameSyntax)tyep.Type).Identifier.Text;

                            var newArgs = new MemberOverrideArguments()
                            {
                                Name = string.Empty,
                                DataType = AttributeDataType.TypeOf,
                                Value = typeValue,
                            };

                            newAttrib.Arguments.Add(newArgs);
                        }
                        else if (aArg.Expression is IdentifierNameSyntax)
                        {
                            var tyep = aArg.Expression as IdentifierNameSyntax;
                            var typeValue = tyep.Identifier.Text;

                            var newArgs = new MemberOverrideArguments()
                            {
                                Name = string.Empty,
                                DataType = AttributeDataType.String,
                                Value = typeValue,
                            };

                            newAttrib.Arguments.Add(newArgs);
                        }
                        else
                        {
                            throw new Exception("Unexpected syntax");
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }



                }
            }

            return (newAttrib, originalName);
        }

        private static ApiMethod BuildMethod(MethodDeclarationSyntax node)
        {
            var newMethod = new ApiMethod()
            {
                Name = node.Identifier.Text,
            };

            newMethod.ReturnType = GetType(node.ReturnType);

            //find the attributes
            if (node.AttributeLists.Any())
            {
                var atrribs = node.AttributeLists.SelectMany(x => x.Attributes);

                foreach (var attrib in atrribs)
                {
                    if (attrib != null)
                    {
                        var name = ((IdentifierNameSyntax)attrib.Name).Identifier.Text;

                        switch (name.ToLower())
                        {
                            case "static":
                                {
                                    newMethod.IsStatic = true;
                                }
                                break;
                            case "export":
                                {
                                    ProcessMethodExportAttrib(attrib, ref newMethod);
                                }
                                break;
                            case "designatedinitializer":
                                {
                                    newMethod.DesignatedInitializer = true;
                                }
                                break;
                            case "nullallowed":
                                {
                                    newMethod.IsNullAllowed = true;
                                }
                                break;
                            case "abstract":
                                {
                                    newMethod.IsAbstract = true;
                                }
                                break;
                            case "requiressuper":
                                {
                                    newMethod.RequiresSuper = true;
                                }
                                break;
                            case "eventargs":
                                {
                                    ProcessMethodEventArgsAttrib(attrib, ref newMethod);
                                }
                                break;
                            case "eventname":
                                {
                                    ProcessMethodEventNameAttrib(attrib, ref newMethod);
                                }
                                break;
                            case "obsolete":
                                {
                                    newMethod.Obsolete = GetStringLiteralValue(attrib);
                                }
                                break;
                            case "wrap":
                                {
                                    ProcessMethodWrapAttrib(attrib, ref newMethod);
                                }
                                break;
                            case "defaultvalue":
                                {
                                    ProcessMethodDefaultValueAttrib(attrib, ref newMethod);
                                }
                                break;
                            case "delegatename":
                                {
                                    ProcessMethodDelegateNameAttrib(attrib, ref newMethod);
                                }
                                break;
                            case "new":
                                {
                                    newMethod.IsNew = true;
                                }
                                break;
                            case "advice":
                                {
                                    ProcessMethodAdviceAttrib(attrib, ref newMethod);
                                }
                                break;
                            case "nodefaultvalue":
                                {
                                    newMethod.DefaultValue = "None";
                                }
                                break;
                            case "ios":
                                {
                                    ProcessMethodiOSAttrib(attrib, ref newMethod);
                                }
                                break;
                            case "tv":
                                {
                                    ProcessMethodTVAttrib(attrib, ref newMethod);
                                }
                                break;
                            default:
                                {
                                    Console.WriteLine($"Unexpected Method Attribute {name} ");
                                }
                                break;


                        }


                    }
                }
            }

            //node parameters
            foreach (var aParam in node.ParameterList.Parameters)
            {
                var newParam = BuildParameter(aParam);

                newMethod.Parameters.Add(newParam);
            }

            return newMethod;
        }

        private static ApiProperty BuildProperty(PropertyDeclarationSyntax node)
        {
            var newProperty = new ApiProperty()
            {
                Name = node.Identifier.Text,
               //ReturnType = ((IdentifierNameSyntax)node.Type).Identifier.Text,
            };

            //determine the property type
            newProperty.Type = GetType(node.Type);

            if (node.AttributeLists.Any())
            {
                var atrribs = node.AttributeLists.SelectMany(x => x.Attributes);

                foreach (var attrib in atrribs)
                {
                    if (attrib != null)
                    {
                        var name = ((IdentifierNameSyntax)attrib.Name).Identifier.Text;

                        switch (name.ToLower())
                        {
                            case "nullallowed":
                                {
                                    newProperty.IsNullAllowed = true;
                                }
                                break;
                            case "abstract":
                                {
                                    newProperty.IsAbstract = true;
                                }
                                break;
                            case "wrap":
                                {
                                    //handle wrap attribute
                                    ProcessPropertyWrapAttrib(attrib, ref newProperty);
                                }
                                break;
                            case "export":
                                {
                                    ProcessPropertyExportAttrib(attrib, ref newProperty);
                                }
                                break;
                            case "ios":
                                {
                                    ProcessPropertyiOSAttrib(attrib, ref newProperty);
                                }
                                break;
                            case "tv":
                                {
                                    ProcessPropertyTVAttrib(attrib, ref newProperty);
                                }
                                break;
                            case "verify":
                                {
                                    ProcessPropertyVerifyAttrib(attrib, ref newProperty);
                                }
                                break;
                            case "static":
                                {
                                    newProperty.IsStatic = true;
                                }
                                break;
                            case "field":
                                {
                                    ProcessPropertyFieldAttrib(attrib, ref newProperty);
                                }
                                break;
                            case "internal":
                                {
                                    newProperty.IsInternal = true;
                                }
                                break;
                            case "obsolete":
                                {
                                    newProperty.Obsolete = GetStringLiteralValue(attrib);
                                }
                                break;
                            case "new":
                                {
                                    newProperty.IsNew = true;
                                }
                                break;
                            case "introduced":
                                {
                                    ProcessPropertyIntroducedAttrib(attrib, ref newProperty);
                                }
                                break;
                            case "notification":
                                {
                                    newProperty.ShouldNotify = true;
                                }
                                break;
                            default:
                                {
                                    Console.WriteLine($"Unexpected property atrribute {name}");
                                }
                                break;

                        }


                    }
                }
            }

            var accessors = GetAccessors(node.AccessorList);

            newProperty.CanGet = accessors.canGet;
            newProperty.CanSet = accessors.canSet;
            newProperty.GetBindName = accessors.getbindName;
            newProperty.SetBindName = accessors.setbindName;

            return newProperty;
        }

        #endregion

        #region Attribute Methods

        #region Class

        private static void ProcessModelAttrib(AttributeSyntax attrib, ref ApiClass newClass)
        {
            var result = BuildAttributes(attrib);
            var autogenname = result.Attribute.Arguments.FirstOrDefault(x => x.Name.Equals("autogeneratedname", StringComparison.OrdinalIgnoreCase) && x.DataType == AttributeDataType.Boolean);

            var newModel = new ApiTypeModel();

            if (autogenname != null)
            {
                newModel.AutoGeneratedName = bool.Parse(autogenname.Value);
            }

            newClass.Model = newModel;
        }

        private static void ProcessClassProtocolAttrib(AttributeSyntax attrib, ref ApiClass newClass)
        {
            newClass.IsProtocol = true;

            var result = BuildAttributes(attrib);

            if (result.Attribute.Arguments.Count > 0)
            {
                var nativeName = result.Attribute.Arguments.FirstOrDefault(x => x.Name.Equals("name", StringComparison.OrdinalIgnoreCase));

                if (nativeName != null)
                    newClass.ProtocolName = nativeName.Value.Replace("\"", "");
            }

        }

        private static void ProcessBaseTypeAttrib(AttributeSyntax attrib, ref ApiClass newClass)
        {
            var result = BuildAttributes(attrib);

            //get the native data type

            var baseType = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.TypeOf);

            if (baseType == null)
            {
                throw new Exception("No basetype specified");
            }

            //create a new basetype object
            var newBaseType = new ApiBaseType()
            {
                TypeName = baseType.Value.Replace("\"", ""),
            };

            //get the original name
            var nativeName = result.Attribute.Arguments.FirstOrDefault(x => x.Name.Equals("name", StringComparison.OrdinalIgnoreCase));

            if (nativeName != null)
                newBaseType.Name = nativeName.Value.Replace("\"", "");

            //get the name of the delegate property
            var delegateName = result.Attribute.Arguments.FirstOrDefault(x => x.Name.Equals("delegates", StringComparison.OrdinalIgnoreCase));

            if (delegateName != null)
                newBaseType.DelegateName = delegateName.Value.Replace("\"", "");

            //get the type of the events 
            var eventsType = result.Attribute.Arguments.FirstOrDefault(x => x.Name.Equals("events", StringComparison.OrdinalIgnoreCase));

            if (eventsType != null)
                newBaseType.EventsType = eventsType.Value.Replace("\"", "");

            newClass.BaseType = newBaseType;
        }

        private static void ProcessClassVerifyAttrib(AttributeSyntax attrib, ref ApiClass newClass)
        {
            var result = BuildAttributes(attrib);

            var newVerify = new ApiVerify();

            if (result.Attribute.Arguments.Any())
            {
                var verifytype = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

                if (verifytype != null)
                    newVerify.VerifyType = verifytype.Value;
            }

            newClass.Verify = newVerify;

        }

        private static void ProcessClassAdviceAttrib(AttributeSyntax attrib, ref ApiClass method)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var stringValue = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name));

            if (stringValue != null)
                method.Advice = stringValue.Value.Replace("\"", "");
        }

        private static void ProcessClassObsoleteAttrib(AttributeSyntax attrib, ref ApiClass method)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var stringValue = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name));

            if (stringValue != null)
                method.Obsolete = stringValue.Value.Replace("\"", "");
        }
        #endregion

        #region Property

        private static void ProcessPropertyTVAttrib(AttributeSyntax attrib, ref ApiProperty property)
        {
            var result = BuildAttributes(attrib);
            var tvAttrib = result.Attribute.Arguments.Where(x => x.DataType == AttributeDataType.Number);

            if (tvAttrib.Any())
            {
                var values = tvAttrib.Select(x => x.Value.Replace("\"", ""));

                var versionNumber = CombineString(values);

                property.TVVersion = versionNumber;
            }
        }

        private static void ProcessPropertyiOSAttrib(AttributeSyntax attrib, ref ApiProperty property)
        {
            var result = BuildAttributes(attrib);
            var iosAttrib = result.Attribute.Arguments.Where(x => x.DataType == AttributeDataType.Number);

            if (iosAttrib.Any())
            {
                var values = iosAttrib.Select(x => x.Value.Replace("\"", ""));

                var versionNumber = CombineString(values);

                property.IosVersion = versionNumber;
            }
        }

        private static void ProcessPropertyIntroducedAttrib(AttributeSyntax attrib, ref ApiProperty property)
        {
            var result = BuildAttributes(attrib);
            var accessAttrib = result.Attribute.Arguments.FirstOrDefault(x => x.DataType == AttributeDataType.MemberAccess);

            if (accessAttrib != null)
            {
                if (accessAttrib.Value.Contains("ios", StringComparison.OrdinalIgnoreCase))
                {
                    var versionAttribs = result.Attribute.Arguments.Where(x => x.DataType == AttributeDataType.Number);
                    var values = versionAttribs.Select(x => x.Value.Replace("\"", ""));
                    var versionNumber = CombineString(values);

                    property.IosVersion = versionNumber;
                    property.Introduced = true;

                }
                else if (accessAttrib.Value.Contains("tvos", StringComparison.OrdinalIgnoreCase))
                {
                    var versionAttribs = result.Attribute.Arguments.Where(x => x.DataType == AttributeDataType.Number);
                    var values = versionAttribs.Select(x => x.Value.Replace("\"", ""));
                    var versionNumber = CombineString(values);

                    property.TVVersion = versionNumber;
                    property.Introduced = true;
                }
            }
        }

        /// <summary>
        /// Process the Verify Atrribute on a property
        /// </summary>
        /// <param name="attrib">attribute</param>
        /// <param name="property"></param>
        private static void ProcessPropertyVerifyAttrib(AttributeSyntax attrib, ref ApiProperty property)
        {
            var result = BuildAttributes(attrib);

            var newVerify = new ApiVerify();

            if (result.Attribute.Arguments.Any())
            {
                var verifytype = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

                if (verifytype != null)
                    newVerify.VerifyType = verifytype.Value;
            }

            property.Verify = newVerify;

        }

        private static void ProcessPropertyExportAttrib(AttributeSyntax attrib, ref ApiProperty property)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var exportNameAttrib = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

            if (exportNameAttrib == null)
            {
                throw new Exception("No Export attrib specified");
            }

            property.ExportName = exportNameAttrib.Value.Replace("\"", "");

            //get the export member access if its set
            var memberAccess = result.Attribute.Arguments.FirstOrDefault(x => x.DataType == AttributeDataType.MemberAccess);

            if (memberAccess != null)
            {
                property.SemanticStrength = memberAccess.Value;
            }
        }

        private static void ProcessPropertyWrapAttrib(AttributeSyntax attrib, ref ApiProperty property)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var wrapNameAttrib = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

            if (wrapNameAttrib == null)
            {
                throw new Exception("No Wrap attrib specified");
            }

            property.WrapName = wrapNameAttrib.Value.Replace("\"", "");
        }

        private static void ProcessPropertyFieldAttrib(AttributeSyntax attrib, ref ApiProperty property)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var fieldParams = result.Attribute.Arguments.Where(x => x.DataType == AttributeDataType.String);

            if (fieldParams.Any())
            {
                var values = fieldParams.Select(x => x.Value.Replace("\"", ""));

                var fieldParVal = CombineString(values);

                property.FieldParams = fieldParVal;
            }
        }

        private static void ProcessParameterAttributes(SyntaxList<AttributeListSyntax> attributes, ref ApiParameter parameter)
        {
            var attribs = attributes.SelectMany(x => x.Attributes);

            foreach (var attrib in attribs)
            {
                var name = ((IdentifierNameSyntax)attrib.Name).Identifier.Text;

                switch (name.ToLower())
                {
                    case "nullallowed":
                        {
                            parameter.IsNullAllowed = true;
                        }
                        break;
                    default:
                        {
                            Console.WriteLine($"Invalid parameter atrrib: {name}");
                        }
                        break;
                }
            }
            
        }
        #endregion

        #region Method

        private static void ProcessMethodExportAttrib(AttributeSyntax attrib, ref ApiMethod method)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var exportNameAttrib = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

            if (exportNameAttrib != null)
                method.ExportName = exportNameAttrib.Value.Replace("\"", "");
        }

        private static void ProcessMethodEventArgsAttrib(AttributeSyntax attrib, ref ApiMethod method)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var eventArgTypeAtrrib = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

            if (eventArgTypeAtrrib != null)
                method.EventArgs = eventArgTypeAtrrib.Value.Replace("\"", "");
        }

        private static void ProcessMethodEventNameAttrib(AttributeSyntax attrib, ref ApiMethod method)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var eventArgTypeAtrrib = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

            if (eventArgTypeAtrrib != null)
                method.EventName = eventArgTypeAtrrib.Value.Replace("\"", "");
        }

        private static void ProcessMethodWrapAttrib(AttributeSyntax attrib, ref ApiMethod method)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var wrapNameAttrib = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

            if (wrapNameAttrib != null)
                method.WrapName = wrapNameAttrib.Value.Replace("\"", "");
        }

        private static void ProcessMethodDefaultValueAttrib(AttributeSyntax attrib, ref ApiMethod method)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var defaultValueAttrib = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name));

            if (defaultValueAttrib != null)
                method.DefaultValue = defaultValueAttrib.Value.Replace("\"", "");
        }

        private static void ProcessMethodDelegateNameAttrib(AttributeSyntax attrib, ref ApiMethod method)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var stringValue = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name));

            if (stringValue != null)
                method.DelegateName = stringValue.Value.Replace("\"", "");
        }

        private static void ProcessMethodAdviceAttrib(AttributeSyntax attrib, ref ApiMethod method)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var stringValue = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name));

            if (stringValue != null)
                method.Advice = stringValue.Value.Replace("\"", "");
        }

        private static void ProcessMethodTVAttrib(AttributeSyntax attrib, ref ApiMethod property)
        {
            var result = BuildAttributes(attrib);
            var tvAttrib = result.Attribute.Arguments.Where(x => x.DataType == AttributeDataType.Number);

            if (tvAttrib.Any())
            {
                var values = tvAttrib.Select(x => x.Value.Replace("\"", ""));

                var versionNumber = CombineString(values);

                property.TVVersion = versionNumber;
            }
        }

        private static void ProcessMethodiOSAttrib(AttributeSyntax attrib, ref ApiMethod property)
        {
            var result = BuildAttributes(attrib);
            var iosAttrib = result.Attribute.Arguments.Where(x => x.DataType == AttributeDataType.Number);

            if (iosAttrib.Any())
            {
                var values = iosAttrib.Select(x => x.Value.Replace("\"", ""));

                var versionNumber = CombineString(values);

                property.IosVersion = versionNumber;
            }
        }
        #endregion

        #endregion

        #region Helper Methods

        private static string GetStringLiteralValue(AttributeSyntax attrib)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var stringValue = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name));

            return stringValue.Value.Replace("\"", "");
        }

        /// <summary>
        /// Get the .net type of the node
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetType(TypeSyntax type)
        {
            if (type is PredefinedTypeSyntax)
            {
                return ((PredefinedTypeSyntax)type).Keyword.Text;
            }
            else if (type is IdentifierNameSyntax)
            {
                return ((IdentifierNameSyntax)type).Identifier.Text;
            }
            else if (type is ArrayTypeSyntax)
            {
                var elementType = ((ArrayTypeSyntax)type).ElementType;

                return GetType(elementType);
            }
            else if (type is GenericNameSyntax)
            {
                var nodeType = (GenericNameSyntax)type;

                var typeName = nodeType.Identifier.Text;

                var argsList = new List<string>();

                foreach (var aRg in nodeType.TypeArgumentList.Arguments)
                {
                    var newType = GetType(aRg);

                    argsList.Add(newType);
                }

                var args = CombineString(argsList);
                //
                var returnType = $"{typeName}<{args}>";

                return returnType;
            }
            else if (type is PointerTypeSyntax)
            {
                var elementType = ((PointerTypeSyntax)type).ElementType;

                var acType = GetType(elementType);

                return "*" + acType;
            }
            else
            {
                throw new Exception($"Unexpected syntax type: {type.ToString()}");

            }
        }

        /// <summary>
        /// Get the accessors for the property
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static (bool canGet, bool canSet, string getbindName, string setbindName) GetAccessors(AccessorListSyntax node)
        {
            var canGet = false;
            var canSet = false;
            string getbindName = null;
            string setbindName = null;

            if (node.Accessors.Count() == 0)
                return (canGet, canSet, getbindName, setbindName);

            var thing = node.Accessors.Where(x => x is AccessorDeclarationSyntax);

            
            var hasGet = thing.FirstOrDefault(x => x.Keyword.Text.Equals("get", StringComparison.OrdinalIgnoreCase));
            var hasSet = thing.FirstOrDefault(x => x.Keyword.Text.Equals("set", StringComparison.OrdinalIgnoreCase));

            if (hasGet != null)
            {
                canGet = true;

                if (hasGet.AttributeLists.Count() > 0)
                {
                    getbindName = GetAccessorBindName(hasGet.AttributeLists.First());

                }
            }

            if (hasSet != null)
            {
                canSet = true;

                if (hasSet.AttributeLists.Count() > 0)
                {
                    setbindName = GetAccessorBindName(hasSet.AttributeLists.First());

                }
            }


            return (canGet, canSet, getbindName, setbindName);

        }

        private static string GetAccessorBindName(AttributeListSyntax attributeLists)
        {
            foreach (var attrib in attributeLists.Attributes)
            {
                var name = ((IdentifierNameSyntax)attrib.Name).Identifier.Text;

                switch (name.ToLower())
                {
                    case "bind":
                        {
                            return GetBindName(attrib);
                        }
                    default:
                        {
                            Console.WriteLine($"Invalid get accessor atribute {name}");
                        }
                        break;
                }
            }

            return null;
        }

        private static string GetBindName(AttributeSyntax attribute)
        {
            var result = BuildAttributes(attribute);
            var wrapNameAttrib = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

            return (wrapNameAttrib != null) ? wrapNameAttrib.Value.Replace("\"", "") : null;
        }

        /// <summary>
        /// Combine a list of strings
        /// </summary>
        /// <param name="list"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        private static string CombineString(IEnumerable<string> list, string seperator = ", ")
        {
            return string.Join(seperator, list);
        }
        #endregion
        #endregion
    }
}
