using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Processing.Src
{
    public class CodeParser
    {
        public ParsingResult Parse(string typeName, string methodName, string codeToCompile)
        {
            if(string.IsNullOrWhiteSpace(codeToCompile)) return ParsingResult.Failure(new List<string>() { $"Unexpected code: {codeToCompile}" });

            var syntaxTree = CSharpSyntaxTree.ParseText(codeToCompile);

            var assemblyName = Path.GetRandomFileName();
            var refPaths = new[] {
                typeof(System.Object).GetTypeInfo().Assembly.Location,
                typeof(Console).GetTypeInfo().Assembly.Location,
                Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll")
            };
            var references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();
            
            var compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using var ms = new MemoryStream();

            var result = compilation.Emit(ms);

            if (!result.Success)
            {
                var failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                return ParsingResult.Failure(failures.Select(diagnostic => diagnostic.Id + ": " + diagnostic.GetMessage()));
            }

            ms.Seek(0, SeekOrigin.Begin);

            var assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
            var type = assembly.GetType(typeName);
            var meth = type.GetMember(methodName).FirstOrDefault();
            if (meth == null) return ParsingResult.Failure(new List<string>() { $"Method '{methodName}' does not exist in code" });
            var method = meth as MethodInfo;

            return ParsingResult.Success(method);
        }
    }
}
