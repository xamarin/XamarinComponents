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
        /// <param name="outputFile"></param>
        public static async Task ExtractDefinitionAsync(string inputFile, string outputFile)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputFile) || !File.Exists(inputFile))
                    throw new Exception("Invalid input file");


                var lines = File.ReadAllText(inputFile);
                var tree = CSharpSyntaxTree.ParseText(lines);

                // Get the root CompilationUnitSyntax.
                var root = await tree.GetRootAsync().ConfigureAwait(false) as CompilationUnitSyntax;

                var serializer = new XmlSerializer(typeof(ApiDefinition));

                var output = new ApiDefinition();

                Process(root, ref output);

                
                using (StreamWriter str = new StreamWriter(outputFile))
                {
                    serializer.Serialize(str, output, output.XmlNamespaces);
                }

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

            return newClass;
        }

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

        private static ApiParameter BuildParameter(ParameterSyntax node)
        {
            //create in instance of the Parameter definition and set the name of the parameter
            var newParam = new ApiParameter()
            {
                Name = node.Identifier.Text,

            };

            // if the node type is 
            if (node.Type is IdentifierNameSyntax)
            {
                newParam.Type = ((IdentifierNameSyntax)node.Type).Identifier.Text;
            }
            else if (node.Type is PredefinedTypeSyntax)
            {
                newParam.Type = ((PredefinedTypeSyntax)node.Type).Keyword.Text;
            }
            else
            {
                throw new Exception($"Unexpected syntax type: {node.Type.ToString()}");
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
    }
}
