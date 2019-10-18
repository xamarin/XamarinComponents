using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xamarin.Binding.IOS.Helpers;

namespace Fileseperator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            filePath.Text = @"C:\ApiDefinition.cs";
            output.Text = @"C:\Export";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath.Text))
                    throw new Exception("You must select a file");

                if (!File.Exists(filePath.Text))
                    throw new Exception("File not found");

                if (string.IsNullOrWhiteSpace(output.Text))
                    throw new Exception("You must select a folder");

                var lines = File.ReadAllText(filePath.Text);

                await BuildApiChangesAsync(lines);

                var results =  Parser.FindTypeDeclarations(lines);

                if (!results.Any())
                    throw new Exception("No types found");

                var linesToRemove = new List<int>();

                var ordered = results.OrderBy(x => x.StartLine).ToList();

                if (!Directory.Exists(output.Text))
                    Directory.CreateDirectory(output.Text);

                foreach (var atypeDef in ordered)
                {
                    //
                    var fileName = atypeDef.TypeName + ".cs";
                    var filePath = System.IO.Path.Combine(output.Text, fileName);

                    if (File.Exists(filePath))
                        File.Delete(filePath);


   
                    File.WriteAllText(filePath, atypeDef.TypeDefinition);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
 

        }

        public async Task BuildApiChangesAsync(string lines)
        {

            var tree = CSharpSyntaxTree.ParseText(lines);

            // Get the root CompilationUnitSyntax.
            var root = await tree.GetRootAsync().ConfigureAwait(false) as CompilationUnitSyntax;

            var children = root.ChildNodes().ToList();

            var names = children.Last();

            var classes = names.ChildNodes().Where(x => x.RawKind == 8857).Cast<InterfaceDeclarationSyntax>().ToList();

            var renamed = classes.Where(x => x.AttributeLists != null && x.AttributeLists.ToString().Contains("Name")).ToList();

            var mapping = new TransformationMap();

            foreach(var aInt in renamed)
            {
                var newClass = new MemberOverride()
                {
                    Name = aInt.Identifier.Text,
                    Type = MemberType.Class,
                };

                foreach (var attribute in aInt.AttributeLists)
                {
                    var attrib = attribute.Attributes.First();

                    //var attribName = ((IdentifierNameSyntax)attrib.Name).Identifier.Text;

                    var result = BuildAttributes(attrib);

                    if (!string.IsNullOrWhiteSpace(result.OriginalName))
                        newClass.OriginalName = result.OriginalName;

                    newClass.Attributes.Add(result.Attribute);
                }

                ///process the members
                ///
                var members = aInt.ChildNodes().ToList();

                if (members.Count > 0)
                {
                    var filteredMembers = members.Where(x => x is MethodDeclarationSyntax || x is PropertyDeclarationSyntax).ToList();

                    if (filteredMembers.Any())
                    {
                       
                        foreach (var aMember in filteredMembers)
                        {
                            SyntaxList<AttributeListSyntax> attribs;
                            MemberOverride newMember = null;

                            if (aMember is MethodDeclarationSyntax)
                            {
                                newMember = new MemberOverride()
                                {
                                    Name = ((MethodDeclarationSyntax)aMember).Identifier.Text,
                                    Type = MemberType.Method,
                                };

                                attribs = ((MethodDeclarationSyntax)aMember).AttributeLists;
                                
                            }
                            else
                            {
                                newMember = new MemberOverride()
                                {
                                    Name = ((PropertyDeclarationSyntax)aMember).Identifier.Text,
                                    Type = MemberType.Property,
                                };

                                attribs = ((PropertyDeclarationSyntax)aMember).AttributeLists;
                            }

                            if (string.IsNullOrWhiteSpace(newMember.Name))
                            {
                                Console.WriteLine("Booo!!");
                            }


                            if (attribs.Count() > 0)
                            {
                                foreach (var attribute in attribs)
                                {
                                    var attrib = attribute.Attributes.First();

                                    try
                                    {

                                        var result = BuildAttributes(attrib);

                                        if (!string.IsNullOrWhiteSpace(result.OriginalName) && result.Attribute.Name.Equals("Export", StringComparison.OrdinalIgnoreCase))
                                        {
                                            if (!result.OriginalName.ToLower().Contains(newMember.Name.ToLower()))
                                                newMember.OriginalName = result.OriginalName;
                                        }


                                        newMember.Attributes.Add(result.Attribute);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }


                                }
                            }

                            

                            


                            newClass.Members.Add(newMember);
                        }
                        
                    }

                }
                mapping.Mappings.Add(newClass);
            }


            mapping.RemoveUnchanged();

            var exported = mapping.Serialize();

            var exporFileName = System.IO.Path.Combine(output.Text, "ClassMappings.json");

            File.WriteAllText(exporFileName, exported);

            var import = TransformationMap.Deserialize(exported);

            MessageBox.Show("Complete");
        }

        public (MemberOverrideAttribute Attribute,string OriginalName) BuildAttributes(AttributeSyntax attrib)
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
