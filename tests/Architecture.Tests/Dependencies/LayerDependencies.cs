// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NetArchTest.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace CocktailsApp.Architecture.Tests.Dependencies
{
    [TestFixture]
    public class LayerDependencies
    {
        private static IEnumerable<TestCaseData> LayerTestCases
        {
            get
            {
                return
                [
                //  new TestCaseData(layerName       , forbidenDependencies                    ),
                    new TestCaseData("Domain"        , new List<string> { "Application", "Infrastructure", "API" }),
                    new TestCaseData("Application"   , new List<string> { "Infrastructure", "API"                }),
                    new TestCaseData("Infrastructure", new List<string> { "API"                                  }),
                ];
            }
        }

        [Test, TestCaseSource(nameof(LayerTestCases))]
        public void LayersDependencies_NoDependencies(string layerName, List<string> dependencies)
        {
            // Arrange
            var assembly = Assembly.Load($"CocktailsApp.{layerName}");

            // Act
            var result = Types
              .InAssembly(assembly)
              .Should()
              .NotHaveDependencyOnAll(dependencies.ToArray())
              .GetResult();

            //Assert
            Assert.True(result.IsSuccessful);
        }
    }
}
