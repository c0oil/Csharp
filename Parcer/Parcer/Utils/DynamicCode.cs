using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;

namespace Parcer.Utils
{
    public class DynamicCode
    {
        public const string DefaultCode =
@"
using System.Collections.Generic;
using System.Linq;

namespace Parcer.ViewModel
{
    public class Bar
    {
        public static IEnumerable<IEnumerable<string>> Match(IEnumerable<IEnumerable<string>> matches)
        {
            {0};
        }
    }
}";

        private static CompilerResults CompileAssembly(string code)
        {
            Dictionary<string, string> providerOptions = new Dictionary<string, string>
            {
                {"CompilerVersion", "v4.0"}
            };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

            CompilerParameters compilerParams = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false
            };
            compilerParams.ReferencedAssemblies.Add("System.Core.Dll");

            return provider.CompileAssemblyFromSource(compilerParams, DefaultCode.Replace("{0}", code));
        }


        public static IEnumerable<IEnumerable<string>> ExecuteMethod(string code, IEnumerable<IEnumerable<string>> matches)
        {
            CompilerResults results = CompileAssembly(code);
            if (results.Errors.Count != 0)
                throw new Exception("Compile assembly failed!");

            object o = results.CompiledAssembly.CreateInstance("Parcer.ViewModel.Bar");
            MethodInfo mi = o.GetType().GetMethod("Match");
            object result = mi.Invoke(o, new[] { matches });

            return result as IEnumerable<IEnumerable<string>>;
        }
    }
}