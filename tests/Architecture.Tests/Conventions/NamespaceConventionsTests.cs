// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System.Reflection;
using System.Text.RegularExpressions;

namespace CocktailsApp.Architecture.Tests.Conventions
{
    [TestFixture]
    public class NamespaceConventionsTests
    {

        private static IEnumerable<TestCaseData> LayerTestCases
        {
            get
            {
                return
                [
                //  new TestCaseData(layerName       ),
                    new TestCaseData("Domain"        ),
                    new TestCaseData("Application"   ),
                    new TestCaseData("Infrastructure"),
                    new TestCaseData("API"           )
                ];
            }
        }

        [Test, TestCaseSource(nameof(LayerTestCases))]
        public void Namespaces_RespectConventions(string layerName)
        {
            // Path to the project root (adjust as needed)
            var projectRoot = Path.GetFullPath($"../../../../../src/{layerName}");

            var files = Directory.GetFiles(projectRoot, "*.cs", SearchOption.AllDirectories);

            var baseNamespace = $"CocktailsApp.{layerName}";

            var errors = new List<string>();

            foreach (var file in files)
            {
                var relativePath = Path.GetRelativePath(projectRoot, file).Replace('\\', '/');

                // Skip AssemblyInfo or other non-type files
                if (relativePath.Contains("AssemblyInfo") ||
                    relativePath.Contains("AssemblyAttributes") ||
                    relativePath.Contains("obj/") ||
                    relativePath.Contains("Migrations"))
                    continue;

                var firstFolder = relativePath.Split('/').FirstOrDefault();
                if (string.IsNullOrWhiteSpace(firstFolder)) continue;

                var expectedNamespace = $"{baseNamespace}.{firstFolder}";

                var typeName = Path.GetFileNameWithoutExtension(file);
                //if (typeName == "ICommandHandler")
                //{
                //    Console.Write("ici");
                //    var test = Assembly.Load($"CocktailsApp.{layerName}")
                //        .GetTypes()
                //        .FirstOrDefault(t => t.Name.StartsWith(typeName));
                //    Console.Write(test);
                //}
                var matchingType = Assembly.Load($"CocktailsApp.{layerName}")
                    .GetTypes()
                    .FirstOrDefault(t => Regex.Replace(t.Name, "`.*", "") == typeName);

                if (matchingType == null)
                {
                    errors.Add($"{typeName}: no class match this file name.");
                    continue;
                }

                if (matchingType.IsDefined(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true))
                    continue;

                var actualNamespace = matchingType.Namespace;

                if (actualNamespace != expectedNamespace)
                    errors.Add($"{typeName}: expected namespace '{expectedNamespace}', but found '{actualNamespace}'");
            }

            Assert.That(errors, Is.Empty, string.Join("\n", errors));
        }
    }
}
