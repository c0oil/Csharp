using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;

namespace empty
{
    public class Generator
    {
        public static CodeNamespace Generate(string namespaceName)
        {
            XmlSchemaSet set = PrepareSchemaSet();
            IEnumerable<TypeModel> types = BuildTypeModels(set);
            CodeNamespace codeNamespace = NamespaceModel.Generate(namespaceName, types);
            return codeNamespace;
        }

        private static XmlSchemaSet PrepareSchemaSet()
        {
            string xsdFileName = "open-protocol-schema.xsd";
            string path = @"I:\temp\empty\empty";
            string xsdPath = Path.Combine(path, xsdFileName);

            XmlSchema xsd;
            using (FileStream stream = new FileStream(xsdPath, FileMode.Open, FileAccess.Read))
            {
                xsd = XmlSchema.Read(stream, null);
            }

            var set = new XmlSchemaSet();
            set.Add(xsd);
            set.Compile();
            return set;
        }

        private static List<TypeModel> BuildTypeModels(XmlSchemaSet set)
        {
            var exporter = new XsdExporter();
            exporter.ExportTypeModels(set);
            var result = exporter.Types.Values.ToList();
            result.AddRange(exporter.CreateClassesForInterfaces());
            return result;
        }
    }

    public class XsdExporter
    {
        private static readonly XmlQualifiedName AnyType = new XmlQualifiedName("anyType", XmlSchema.Namespace);

        public readonly Dictionary<XmlQualifiedName, TypeModel> Types = new Dictionary<XmlQualifiedName, TypeModel>();
        private Dictionary<XmlQualifiedName, XmlSchemaAttributeGroup> AttributeGroups = new Dictionary<XmlQualifiedName, XmlSchemaAttributeGroup>();
        private Dictionary<XmlQualifiedName, XmlSchemaGroup> Groups = new Dictionary<XmlQualifiedName, XmlSchemaGroup>();
        private Dictionary<XmlQualifiedName, string> Labels = new Dictionary<XmlQualifiedName, string>();

        public void ExportTypeModels(XmlSchemaSet set)
        {
            var objectModel = new SimpleModel
            {
                Name = "AnyType",
                ValueType = typeof(object),
            };

            Types[AnyType] = objectModel;

            AttributeGroups = set.Schemas().Cast<XmlSchema>().SelectMany(s => s.AttributeGroups.Values.Cast<XmlSchemaAttributeGroup>()).ToDictionary(g => g.QualifiedName);
            Groups = set.Schemas().Cast<XmlSchema>().SelectMany(s => s.Groups.Values.Cast<XmlSchemaGroup>()).ToDictionary(g => g.QualifiedName);

            foreach (XmlSchemaType globalType in set.GlobalTypes.Values.Cast<XmlSchemaType>())
            {
                CreateTypeModel(globalType, globalType.QualifiedName);
            }

            foreach (XmlSchemaAttributeGroup rootElement in AttributeGroups.Values)
            {
                CreateTypeModel(rootElement, rootElement.QualifiedName);
            }
        }

        private int GroupCount = 0;
        private int AttributeCount = 0;
        public IEnumerable<TypeModel> CreateClassesForInterfaces()
        {
            foreach (KeyValuePair<XmlQualifiedName, TypeModel> model in Types)
            {
                InterfaceModel interfaceModel = model.Value as InterfaceModel;
                if (interfaceModel == null)
                {
                    continue;
                }

                yield return new ClassModel
                {
                    Name = TypeNameHelper.ToTitleCase(model.Key.Name),
                    Properties = interfaceModel.AllProperties(),
                    Interfaces = new List<InterfaceModel> { interfaceModel },
                };
            }
        }


        private TypeModel CreateTypeModel(XmlSchemaAnnotated type, XmlQualifiedName qualifiedName)
        {
            TypeModel typeModel;
            if (!qualifiedName.IsEmpty && Types.TryGetValue(qualifiedName, out typeModel))
            {
                return typeModel;
            }
            if (qualifiedName.Name == "fundName")
            {

            }

            typeModel = TryCreateEnumModel(type, qualifiedName)
                ?? TryCreateInterfaceModel(type, qualifiedName)
                ?? TryCreateSimpleModel(type, qualifiedName);
            if (typeModel != null && !qualifiedName.IsEmpty)
            {
                Types[qualifiedName] = typeModel;
            }

            return typeModel;
        }


        // see http://msdn.microsoft.com/en-us/library/z2w0sxhf.aspx
        private static readonly HashSet<string> EnumTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "string", "normalizedString", "token", "Name", "NCName", "ID", "ENTITY", "NMTOKEN" };

        private static TypeModel TryCreateEnumModel(XmlSchemaAnnotated type, XmlQualifiedName qualifiedName)
        {
            var simpleType = type as XmlSchemaSimpleType;
            if (simpleType != null)
            {
                var typeRestriction = simpleType.Content as XmlSchemaSimpleTypeRestriction;
                if (typeRestriction != null)
                {
                    var enumFacets = typeRestriction.Facets.OfType<XmlSchemaEnumerationFacet>().ToList();
                    var isEnum = (enumFacets.Count == typeRestriction.Facets.Count && enumFacets.Count != 0) ||
                                (EnumTypes.Contains(typeRestriction.BaseTypeName.Name) && enumFacets.Any());
                    if (isEnum)
                    {
                        // we got an enum
                        var name = TypeNameHelper.ToTitleCase(qualifiedName.Name);

                        var enumModel = new EnumModel
                        {
                            Name = name,
                            Values = enumFacets.Select(facet => new EnumValueModel
                            {
                                Name = TypeNameHelper.ToNormalizedEnumName(TypeNameHelper.ToTitleCase(facet.Value)),
                                Value = facet.Value
                            }).ToList()
                        };

                        return enumModel;
                    }
                }
            }
            return null;
        }

        private TypeModel TryCreateSimpleModel(XmlSchemaAnnotated type, XmlQualifiedName qualifiedName)
        {
            var simpleType = type as XmlSchemaSimpleType;
            if (simpleType != null)
            {
                return new SimpleModel
                {
                    Name = TypeNameHelper.ToTitleCase(qualifiedName.Name),
                    ValueType = GetEffectiveType(simpleType.Datatype.TypeCode, simpleType.Datatype.Variety),
                }; ;
            }

            return null;
        }

        private TypeModel TryCreateInterfaceModel(XmlSchemaAnnotated type, XmlQualifiedName qualifiedName)
        {
            var group = type as XmlSchemaGroup;
            if (group != null)
            {
                var interfaceModel = new InterfaceModel
                {
                    Name = "I" + TypeNameHelper.ToTitleCase(qualifiedName.Name),
                };

                var particle = group.Particle;
                IEnumerable<Particle> items = GetElements(particle);
                IEnumerable<PropertyModel> properties = CreatePropertiesForElements(interfaceModel, particle, items.Where(i => !(i.XmlParticle is XmlSchemaGroupRef)));
                interfaceModel.Properties = properties.ToList();

                GroupCount++;
                return interfaceModel;
            }


            var attributeGroup = type as XmlSchemaAttributeGroup;
            if (attributeGroup != null)
            {
                var interfaceModel = new InterfaceModel
                {
                    Name = "I" + TypeNameHelper.ToTitleCase(qualifiedName.Name),
                };

                XmlSchemaObjectCollection items = attributeGroup.Attributes;
                IEnumerable<PropertyModel> properties = CreatePropertiesForAttributes(interfaceModel, items.OfType<XmlSchemaAttribute>());
                IEnumerable<InterfaceModel> interfaces = items.OfType<XmlSchemaAttributeGroupRef>().Select(a => (InterfaceModel)CreateTypeModel(AttributeGroups[a.RefName], a.RefName));
                interfaceModel.Properties.AddRange(properties);
                interfaceModel.Interfaces.AddRange(interfaces);

                AttributeCount++;
                return interfaceModel;
            }

            return null;
        }

        private IEnumerable<PropertyModel> CreatePropertiesForAttributes(TypeModel typeModel, IEnumerable<XmlSchemaObject> items)
        {
            var properties = new List<PropertyModel>();

            foreach (var item in items)
            {
                var attribute = item as XmlSchemaAttribute;
                if (attribute == null)
                {
                    var attributeGroupRef = item as XmlSchemaAttributeGroupRef;
                    if (attributeGroupRef != null)
                    {
                        CreateTypeModel(AttributeGroups[attributeGroupRef.RefName], attributeGroupRef.RefName);

                        var groupItems = AttributeGroups[attributeGroupRef.RefName].Attributes;
                        var groupProperties = CreatePropertiesForAttributes(typeModel,
                            groupItems.Cast<XmlSchemaObject>());
                        properties.AddRange(groupProperties);
                    }
                }
                else
                {
                    if (attribute.Use != XmlSchemaUse.Prohibited)
                    {
                        var attributeQualifiedName = attribute.AttributeSchemaType.QualifiedName;

                        if (attributeQualifiedName.IsEmpty)
                        {
                            attributeQualifiedName = attribute.QualifiedName;

                            if (attributeQualifiedName.IsEmpty || attributeQualifiedName.Namespace == "")
                            {
                                // inner type, have to generate a type name
                                var typeName = TypeNameHelper.ToTitleCase(typeModel.Name) +
                                               TypeNameHelper.ToTitleCase(attribute.QualifiedName.Name);
                                attributeQualifiedName = new XmlQualifiedName(typeName);
                            }
                        }

                        var attributeName = TypeNameHelper.ToTitleCase(attribute.QualifiedName.Name);
                        if (attributeName == typeModel.Name)
                        {
                            attributeName += "Property"; // member names cannot be the same as their enclosing type
                        }

                        var property = new PropertyModel
                        {
                            Name = attributeName,
                            Type = CreateTypeModel(attribute.AttributeSchemaType, attributeQualifiedName),
                        };

                        properties.Add(property);
                    }
                }
            }

            return properties;
        }

        private IEnumerable<PropertyModel> CreatePropertiesForElements(TypeModel typeModel, XmlSchemaParticle particle, IEnumerable<Particle> items)
        {
            var properties = new List<PropertyModel>();
            foreach (var item in items)
            {
                PropertyModel property = null;

                var element = item.XmlParticle as XmlSchemaElement;
                // ElementSchemaType must be non-null. This is not the case when maxOccurs="0".
                if (element?.ElementSchemaType == null)
                {
                    var any = item.XmlParticle as XmlSchemaAny;
                    if (any != null)
                    {
                        property = new PropertyModel
                        {
                            Name = "Any",
                            Type = new SimpleModel
                            {
                                ValueType = typeof(XmlElement),
                            },
                        };
                    }
                    else
                    {
                        var groupRef = item.XmlParticle as XmlSchemaGroupRef;
                        if (groupRef != null)
                        {
                            CreateTypeModel(Groups[groupRef.RefName], groupRef.RefName);

                            var groupItems = GetElements(groupRef.Particle);
                            var groupProperties = CreatePropertiesForElements(typeModel, item.XmlParticle, groupItems);
                            properties.AddRange(groupProperties);
                        }
                    }
                }

                if (property != null)
                {
                    properties.Add(property);
                }
            }

            return properties;
        }

        public static IEnumerable<Particle> GetElements(XmlSchemaGroupBase groupBase)
        {
            foreach (var item in groupBase.Items)
            {
                foreach (var element in GetElements(item))
                {
                    element.MaxOccurs = Math.Max(element.MaxOccurs, groupBase.MaxOccurs);
                    element.MinOccurs = Math.Min(element.MinOccurs, groupBase.MinOccurs);
                    yield return element;
                }
            }
        }

        public static IEnumerable<Particle> GetElements(XmlSchemaObject item)
        {
            if (item == null) { yield break; }

            var element = item as XmlSchemaElement;
            if (element != null)
            {
                yield return new Particle(element);
            }

            var any = item as XmlSchemaAny;
            if (any != null) { yield return new Particle(any); }

            var groupRef = item as XmlSchemaGroupRef;
            if (groupRef != null) { yield return new Particle(groupRef); }

            var itemGroupBase = item as XmlSchemaGroupBase;
            if (itemGroupBase != null)
            {
                foreach (var groupBaseElement in GetElements(itemGroupBase))
                    yield return groupBaseElement;
            }
        }

        private static Type GetEffectiveType(XmlTypeCode typeCode, XmlSchemaDatatypeVariety variety)
        {
            Type result;
            switch (typeCode)
            {
                case XmlTypeCode.AnyAtomicType:
                    // union
                    result = typeof(string);
                    break;
                case XmlTypeCode.AnyUri:
                case XmlTypeCode.Date:
                case XmlTypeCode.Duration:
                case XmlTypeCode.GDay:
                case XmlTypeCode.GMonth:
                case XmlTypeCode.GMonthDay:
                case XmlTypeCode.GYear:
                case XmlTypeCode.GYearMonth:
                case XmlTypeCode.Time:
                    result = variety == XmlSchemaDatatypeVariety.List ? typeof(string[]) : typeof(string);
                    break;
                case XmlTypeCode.Decimal:
                case XmlTypeCode.Integer:
                case XmlTypeCode.NegativeInteger:
                case XmlTypeCode.NonNegativeInteger:
                case XmlTypeCode.NonPositiveInteger:
                case XmlTypeCode.PositiveInteger:
                    result = typeof(string);
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }

        public class Particle
        {
            public Particle(XmlSchemaParticle particle)
            {
                XmlParticle = particle;
                MinOccurs = particle.MinOccurs;
                MaxOccurs = particle.MaxOccurs;
            }

            public XmlSchemaParticle XmlParticle { get; set; }
            public decimal MaxOccurs { get; set; }
            public decimal MinOccurs { get; set; }
        }
    }

    public static class TypeNameHelper
    {
        private static readonly Dictionary<char, string> InvalidChars = CreateInvalidChars();
        private static readonly Regex InvalidCharsRegex = CreateInvalidCharsRegex();
        private static readonly CodeDomProvider Provider = new Microsoft.CSharp.CSharpCodeProvider();

        private static Dictionary<char, string> CreateInvalidChars()
        {
            var invalidChars = new Dictionary<char, string>();

            invalidChars['\x00'] = "Null";
            invalidChars['\x01'] = "StartOfHeading";
            invalidChars['\x02'] = "StartOfText";
            invalidChars['\x03'] = "EndOfText";
            invalidChars['\x04'] = "EndOfTransmission";
            invalidChars['\x05'] = "Enquiry";
            invalidChars['\x06'] = "Acknowledge";
            invalidChars['\x07'] = "Bell";
            invalidChars['\x08'] = "Backspace";
            invalidChars['\x09'] = "HorizontalTab";
            invalidChars['\x0A'] = "LineFeed";
            invalidChars['\x0B'] = "VerticalTab";
            invalidChars['\x0C'] = "FormFeed";
            invalidChars['\x0D'] = "CarriageReturn";
            invalidChars['\x0E'] = "ShiftOut";
            invalidChars['\x0F'] = "ShiftIn";
            invalidChars['\x10'] = "DataLinkEscape";
            invalidChars['\x11'] = "DeviceControl1";
            invalidChars['\x12'] = "DeviceControl2";
            invalidChars['\x13'] = "DeviceControl3";
            invalidChars['\x14'] = "DeviceControl4";
            invalidChars['\x15'] = "NegativeAcknowledge";
            invalidChars['\x16'] = "SynchronousIdle";
            invalidChars['\x17'] = "EndOfTransmissionBlock";
            invalidChars['\x18'] = "Cancel";
            invalidChars['\x19'] = "EndOfMedium";
            invalidChars['\x1A'] = "Substitute";
            invalidChars['\x1B'] = "Escape";
            invalidChars['\x1C'] = "FileSeparator";
            invalidChars['\x1D'] = "GroupSeparator";
            invalidChars['\x1E'] = "RecordSeparator";
            invalidChars['\x1F'] = "UnitSeparator";
            //invalidChars['\x20'] = "Space";
            invalidChars['\x21'] = "ExclamationMark";
            invalidChars['\x22'] = "Quote";
            invalidChars['\x23'] = "Hash";
            invalidChars['\x24'] = "Dollar";
            invalidChars['\x25'] = "Percent";
            invalidChars['\x26'] = "Ampersand";
            invalidChars['\x27'] = "SingleQuote";
            invalidChars['\x28'] = "LeftParenthesis";
            invalidChars['\x29'] = "RightParenthesis";
            invalidChars['\x2A'] = "Asterisk";
            invalidChars['\x2B'] = "Plus";
            invalidChars['\x2C'] = "Comma";
            //invalidChars['\x2D'] = "Minus";
            invalidChars['\x2E'] = "Period";
            invalidChars['\x2F'] = "Slash";
            invalidChars['\x3A'] = "Colon";
            invalidChars['\x3B'] = "Semicolon";
            invalidChars['\x3C'] = "LessThan";
            invalidChars['\x3D'] = "Equal";
            invalidChars['\x3E'] = "GreaterThan";
            invalidChars['\x3F'] = "QuestionMark";
            invalidChars['\x40'] = "At";
            invalidChars['\x5B'] = "LeftSquareBracket";
            invalidChars['\x5C'] = "Backslash";
            invalidChars['\x5D'] = "RightSquareBracket";
            invalidChars['\x5E'] = "Caret";
            //invalidChars['\x5F'] = "Underscore";
            invalidChars['\x60'] = "Backquote";
            invalidChars['\x7B'] = "LeftCurlyBrace";
            invalidChars['\x7C'] = "Pipe";
            invalidChars['\x7D'] = "RightCurlyBrace";
            invalidChars['\x7E'] = "Tilde";
            invalidChars['\x7F'] = "Delete";

            return invalidChars;
        }

        private static string MakeValidIdentifier(string s)
        {
            var id = InvalidCharsRegex.Replace(s, m => InvalidChars[m.Value[0]]);
            return Provider.CreateValidIdentifier(Regex.Replace(id, @"\W+", "_"));
        }

        public static string ToTitleCase(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            return MakeValidIdentifier(ToPascalCase(s));
        }

        // Match non-letter followed by letter
        private static Regex PascalCaseRegex = new Regex(@"[^\p{L}]\p{L}", RegexOptions.Compiled);

        // Uppercases first letter and all letters following non-letters.
        // Examples: testcase -> Testcase, html5element -> Html5Element, test_case -> Test_Case
        public static string ToPascalCase(string s)
        {
            if (string.IsNullOrEmpty(s)) { return s; }
            return char.ToUpperInvariant(s[0])
                + PascalCaseRegex.Replace(s.Substring(1), m => m.Value[0] + char.ToUpperInvariant(m.Value[1]).ToString());
        }

        public static string ToNormalizedEnumName(string name)
        {
            name = name.Trim().Replace(' ', '_').Replace('\t', '_');
            if (string.IsNullOrEmpty(name))
            {
                return "Item";
            }
            if (!char.IsLetter(name[0]))
            {
                return $"Item{name}";
            }
            return name;
        }

        private static Regex CreateInvalidCharsRegex()
        {
            var r = string.Join("", InvalidChars.Keys.Select(c => $@"\x{(int)c:x2}").ToArray());
            return new Regex("[" + r + "]", RegexOptions.Compiled);
        }
    }

    public abstract class TypeModel
    {
        public string Name { get; set; }

        public virtual CodeTypeDeclaration Generate()
        {
            CodeTypeDeclaration typeDeclaration = new CodeTypeDeclaration { Name = Name };

            return typeDeclaration;
        }

        public virtual CodeTypeReference GetReferenceFor()
        {
            return new CodeTypeReference(Name);
        }
    }

    public class SimpleModel : TypeModel
    {
        public Type ValueType { get; set; }

        public override CodeTypeDeclaration Generate()
        {
            return null;
        }

        public override CodeTypeReference GetReferenceFor()
        {
            return new CodeTypeReference(ValueType);
        }
    }

    public class PropertyModel : TypeModel
    {
        public TypeModel Type { get; set; }
        private CodeTypeReference TypeReference => Type.GetReferenceFor();

        public void AddMembersTo(CodeTypeDeclaration typeDeclaration)
        {
            var typeReference = TypeReference;

            CodeTypeMember member = new CodeMemberField
            {
                Name = Name,
                Type = typeReference,
            };
            member.Name += " { get; set; }";
            member.Attributes = MemberAttributes.Public;
            typeDeclaration.Members.Add(member);
        }

        public void AddInterfaceMembersTo(CodeTypeDeclaration typeDeclaration)
        {
            var typeReference = TypeReference;
            CodeTypeMember member = new CodeMemberProperty
            {
                Name = Name,
                Type = typeReference,
                HasGet = true,
                HasSet = true,
            };
            typeDeclaration.Members.Add(member);
        }
    }

    public class InterfaceModel : TypeModel
    {
        public List<PropertyModel> Properties { get; set; }
        public List<InterfaceModel> Interfaces { get; set; }

        public InterfaceModel()
        {
            Properties = new List<PropertyModel>();
            Interfaces = new List<InterfaceModel>();
        }

        public override CodeTypeDeclaration Generate()
        {
            var interfaceDeclaration = base.Generate();
            interfaceDeclaration.IsInterface = true;

            interfaceDeclaration.BaseTypes.AddRange(Interfaces.Select(i => i.GetReferenceFor()).ToArray());

            foreach (PropertyModel property in Properties)
            {
                property.AddInterfaceMembersTo(interfaceDeclaration);
            }

            return interfaceDeclaration;
        }

        public List<PropertyModel> AllProperties()
        {
            var result = new List<PropertyModel>();
            if (Properties != null)
            {
                result.AddRange(Properties);
            }
            if (Interfaces != null)
            {
                result.AddRange(Interfaces.SelectMany(x => x.AllProperties()));
            }
            return result;
        }
    }

    public class ClassModel : TypeModel
    {
        public List<InterfaceModel> Interfaces { get; set; }
        public List<PropertyModel> Properties { get; set; }
        public TypeModel BaseClass { get; set; }

        public override CodeTypeDeclaration Generate()
        {
            CodeTypeDeclaration classDeclaration = base.Generate();
            classDeclaration.IsClass = true;

            var baseClass = BaseClass as ClassModel;
            if (baseClass != null)
            {
                classDeclaration.BaseTypes.Add(baseClass.GetReferenceFor());
            }

            foreach (PropertyModel property in Properties)
            {
                property.AddMembersTo(classDeclaration);
            }

            classDeclaration.BaseTypes.AddRange(Interfaces.Select(i => i.GetReferenceFor()).ToArray());

            return classDeclaration;
        }
    }


    public struct EnumValueModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class EnumModel : TypeModel
    {
        public List<EnumValueModel> Values { get; set; }

        public override CodeTypeDeclaration Generate()
        {
            CodeTypeDeclaration enumDeclaration = base.Generate();
            enumDeclaration.IsEnum = true;

            foreach (var val in Values)
            {
                var member = new CodeMemberField { Name = val.Name };

                if (val.Name != val.Value) // illegal identifier chars in value
                {
                    var enumAttribute = GetDescriptionAttribute(val.Value);
                    member.CustomAttributes.Add(enumAttribute);
                }

                enumDeclaration.Members.Add(member);
            }

            return enumDeclaration;
        }

        private CodeAttributeDeclaration GetDescriptionAttribute(string description)
        {
            return new CodeAttributeDeclaration(new CodeTypeReference(typeof(DescriptionAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression(description)));
        }
    }


    public class NamespaceModel
    {
        public static CodeNamespace Generate(string namespaceName, IEnumerable<TypeModel> types)
        {
            var codeNamespace = new CodeNamespace(namespaceName);
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.ObjectModel"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Xml.Serialization"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Linq"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.ComponentModel"));


            foreach (var typeModel in types)
            {
                var type = typeModel.Generate();
                if (type != null)
                {
                    codeNamespace.Types.Add(type);
                }
            }

            return codeNamespace;
        }
    }
}
