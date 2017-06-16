using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public enum ImportMethodType
    {
        Element,
        Atrribution,
    }

    public class ImportMethod
    {
        public string Name { get; set; }
        public string ResultClass { get; set; }
        public ImportMethodType ImportType { get; set; }

        public string[] Body { get; set; }
    }

    public class XmlImportGenerator
    {
        public int OffsetTab { get; set; } = 0;

        public string GenerateImportElement()
        {
            string xsdFileName = "open-protocol-schema.xsd";
            string path = @"D:\GitHub\empty\empty";
            string xsdPath = Path.Combine(path, xsdFileName);

            string nameProject = "RaObjects";
            string nameSpace = "RaObjects.Objects";

            var xsdImporter = new XsdImport();
            XmlMap xsdElement = xsdImporter.ImportElement(xsdPath, nameProject, nameSpace);

            var methodBuilder = new ImportMethodBuilder();
            IEnumerable<ImportMethod> importMethods = methodBuilder.CreateMethods(xsdElement);

            var result = new List<string>();
            result.AddRange(importMethods.SelectMany(x => x.Body));
            SetOffset(result, OffsetTab);

            return result.Aggregate((aggResult, aggItem) => aggResult + Environment.NewLine + aggItem);
        }

        private void SetOffset(List<string> result, int offsetTab)
        {
            throw new NotImplementedException();
        }
    }
}
