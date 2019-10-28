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

        private static void ProcessTree(List<SyntaxNode> tree, ref ApiDefinition output)
        {
            foreach (var aChild in tree)
            {
                if (aChild is NamespaceDeclarationSyntax)
                {
                    var clnamespace = ((IdentifierNameSyntax)((NamespaceDeclarationSyntax)aChild).Name).Identifier.Text;

                    output.Namespace = clnamespace;

                    if (tree.Last() == aChild)
                    {
                        var classes = aChild.ChildNodes().ToList();

                        ProcessTree(classes, ref output);
                    }
                }
                else if (aChild is UsingDirectiveSyntax)
                {
                    var usingVal = ((IdentifierNameSyntax)((UsingDirectiveSyntax)aChild).Name).Identifier.Text;

                    output.Usings.Items.Add(new ApiUsing() { Name = usingVal });
                }
                else if (aChild is DelegateDeclarationSyntax)
                {
                    ApiDelegate delegateEntry = BuildDelegate((DelegateDeclarationSyntax)aChild);

                    output.Delegates.Add(delegateEntry);

                }
                else if (aChild is InterfaceDeclarationSyntax)
                {
                    var classentry = BuildClass((InterfaceDeclarationSyntax)aChild);

                    output.Types.Add(classentry);
                }

            }
        }

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
                                        newClass.IsProtocol = true;
                                    }
                                    break;
                                case "static":
                                    {
                                        newClass.IsStatic = true;
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
                        //else if (aArg.Expression is InitializerExpressionSyntax)
                        //{
                        //    var tyep = aArg.Expression as InitializerExpressionSyntax;

                        //    Console.WriteLine(tyep.ToString());

                        //}
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

                                                        Console.WriteLine(typvalue);

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
                        else
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

                        }


                    }
                }
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
                                    newProperty.NeedsVerify = "true";
                                }
                                break;
                            case "static":
                                {
                                    newProperty.IsStatic = true;
                                }
                                break;
                            case "field":
                                {
                                    
                                }
                                break;
                            default:
                                {
                                    Console.WriteLine(name);
                                }
                                break;

                        }


                    }
                }
            }

            var accessors = GetAccessors(node.AccessorList);

            newProperty.CanGet = accessors.canGet;
            newProperty.CanSet = accessors.canSet;

            return newProperty;
        }




        #region Attribute Methods

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

        private static void ProcessBaseTypeAttrib(AttributeSyntax attrib, ref ApiClass newClass)
        {
            var result = BuildAttributes(attrib);
            var baseType = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.TypeOf);

            if (baseType == null)
            {
                throw new Exception("No basetype specified");
            }

            var newBaseType = new ApiBaseType()
            {
                TypeName = baseType.Value,
            };

            newClass.BaseType = newBaseType;
        }

        private static void ProcessPropertyExportAttrib(AttributeSyntax attrib, ref ApiProperty newProperty)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var exportNameAttrib = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

            if (exportNameAttrib == null)
            {
                throw new Exception("No Export attrib specified");
            }

            newProperty.ExportName = exportNameAttrib.Value;

            //get the export member access if its set
            var memberAccess = result.Attribute.Arguments.FirstOrDefault(x => x.DataType == AttributeDataType.MemberAccess);

            if (memberAccess != null)
            {
                newProperty.SemanticStrength = memberAccess.Value;
            }
        }

        private static void ProcessPropertyWrapAttrib(AttributeSyntax attrib, ref ApiProperty newProperty)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var wrapNameAttrib = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

            if (wrapNameAttrib == null)
            {
                throw new Exception("No Wrap attrib specified");
            }

            newProperty.WrapName = wrapNameAttrib.Value;
        }

        private static void ProcessMethodExportAttrib(AttributeSyntax attrib, ref ApiMethod newProperty)
        {
            var result = BuildAttributes(attrib);

            //get the exported name
            var exportNameAttrib = result.Attribute.Arguments.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Name) && x.DataType == AttributeDataType.String);

            if (exportNameAttrib == null)
            {
                throw new Exception("No Export attrib specified");
            }

            newProperty.ExportName = exportNameAttrib.Value;

        }

        private static void ProcessPropertyTVAttrib(AttributeSyntax attrib, ref ApiProperty newProperty)
        {
            var result = BuildAttributes(attrib);
            var tvAttrib = result.Attribute.Arguments.Where(x => x.DataType == AttributeDataType.Number);

            if (tvAttrib.Any())
            {
                var values = tvAttrib.Select(x => x.Value);

                var versionNumber = CombineString(values);

                newProperty.TVVersion = versionNumber;
            }
        }

        private static void ProcessPropertyiOSAttrib(AttributeSyntax attrib, ref ApiProperty newProperty)
        {
            var result = BuildAttributes(attrib);
            var iosAttrib = result.Attribute.Arguments.Where(x => x.DataType == AttributeDataType.Number);

            if (iosAttrib.Any())
            {
                var values = iosAttrib.Select(x => x.Value);

                var versionNumber = CombineString(values);

                newProperty.IosVersion = versionNumber;
            }
        }

        private static void ProcessPropertyVerifyAttrib(AttributeSyntax attrib, ref ApiProperty newProperty)
        {
            var result = BuildAttributes(attrib);
            var tvAttrib = result.Attribute.Arguments.Where(x => x.DataType == AttributeDataType.Number);

        }
        #endregion

        #region Helper Methods

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
        private static (bool canGet, bool canSet) GetAccessors(AccessorListSyntax node)
        {
            if (node.Accessors.Count() == 0)
                return (false, false);

            var thing = node.Accessors.Where(x => x is AccessorDeclarationSyntax);

            var canGet = false;
            var canSet = false;

            var hasGet = thing.FirstOrDefault(x => x.Keyword.Text.Equals("get", StringComparison.OrdinalIgnoreCase));
            var hasSet = thing.FirstOrDefault(x => x.Keyword.Text.Equals("set", StringComparison.OrdinalIgnoreCase));

            canGet = (hasGet != null);
            canSet = (hasSet != null);

            return (canGet, canSet);

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
