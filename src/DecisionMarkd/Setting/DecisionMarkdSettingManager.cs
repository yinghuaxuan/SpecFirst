using Microsoft.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DecisionMarkd
{
    public class DecisionMarkdSettingManager
    {
        const string DefaultTestingFramework = "xUnit";
        const string DefaultTestFileName = "{spec_name}.generated.cs";
        const string DefaultImplementationFileName = "{spec_name}.implementation.generated.cs";
        const string DefaultSpecFileExtension = ".spec.md";

        public DecisionMarkdSettingManager(SourceGeneratorContext context)
        {
            AdditionalText settingFile =
                context
                .AdditionalFiles
                .FirstOrDefault(f => f.Path.EndsWith("decisionmarkd.config", System.StringComparison.OrdinalIgnoreCase));
            if(settingFile != null)
            {
                Settings = Parse(
                    settingFile.Path,
                    context.Compilation.Assembly.Name);
            }
            else
            {
                Settings = Default(context.Compilation.AssemblyName);
            }
        }

        public DecisionMarkdSettings Settings { get; set; }

        private DecisionMarkdSettings Parse(string settingFile, string defaultNamespace)
        {
            XDocument settings = XDocument.Load(settingFile);
            return new DecisionMarkdSettings
            {
                TestingFramework = settings.Descendants("TestingFramework").FirstOrDefault()?.Value ?? DefaultTestingFramework,
                TestFileNamePattern = settings.Descendants("TestFileNamePattern").FirstOrDefault()?.Value ?? DefaultTestFileName,
                ImplementationFileNamePattern = settings.Descendants("ImplementationFileNamePattern").FirstOrDefault()?.Value ?? DefaultImplementationFileName,
                TestFilePath = settings.Descendants("TestFilePath").FirstOrDefault()?.Value ?? string.Empty,
                ImplementationFilePath = settings.Descendants("ImplementationFilePath").FirstOrDefault()?.Value ?? string.Empty,
                Namespace = settings.Descendants("Namespace").FirstOrDefault()?.Value ?? defaultNamespace,
                SpecFileExtension = settings.Descendants("SpecFileExtension").FirstOrDefault()?.Value ?? DefaultSpecFileExtension
            };
        }

        private DecisionMarkdSettings Default(string defaultNamespace)
        {
            return new DecisionMarkdSettings
            {
                TestingFramework = DefaultTestingFramework,
                TestFileNamePattern = DefaultTestFileName,
                ImplementationFileNamePattern = DefaultImplementationFileName,
                TestFilePath = string.Empty,
                ImplementationFilePath = string.Empty,
                SpecFileExtension = DefaultSpecFileExtension,
                Namespace = defaultNamespace
            };
        }
    }
}
