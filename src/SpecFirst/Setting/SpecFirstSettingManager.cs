namespace SpecFirst.Setting
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.CodeAnalysis;

    public class SpecFirstSettingManager
    {
        const string DefaultTestingFramework = "xUnit";
        const string DefaultTestProject = "{spec_project}.Tests";
        const string DefaultTestFileName = "{spec_name}Tests.g.cs";
        const string DefaultImplementationFileName = "{spec_name}Tests.impl.g.cs";
        const string DefaultSpecFileExtension = ".spec.md";

        private readonly GeneratorExecutionContext _context;

        public SpecFirstSettingManager(GeneratorExecutionContext context)
        {
            _context = context;
            AdditionalText settingFile =
                context
                .AdditionalFiles
                .FirstOrDefault(f => f.Path.EndsWith("specfirst.config", System.StringComparison.OrdinalIgnoreCase));
            if (settingFile != null)
            {
                Settings = Parse(settingFile.Path);
            }
            else
            {
                Settings = Default();
            }
        }

        public SpecFirstSettings Settings { get; }

        public string GetTestProject()
        {
            return Settings.TestProject.Replace("{spec_project}", _context.Compilation.AssemblyName);
        }

        public string GetSpecName(AdditionalText specFile)
        {
            return $"{new FileInfo(specFile.Path).Name.Replace(Settings.SpecFileExtension, "")}";
        }

        public string GetTestFile(AdditionalText specFile)
        {
            return Settings.TestFile.Replace("{spec_name}", GetSpecName(specFile));
        }

        public string GetTestImplFile(AdditionalText specFile)
        {
            return Settings.TestImplFile.Replace("{spec_name}", GetSpecName(specFile));
        }

        public string GetTestFilePath(AdditionalText specFile)
        {
            string specPath = Path.GetDirectoryName(specFile.Path)!;
            string[] paths = specPath.Split(
                new[] { _context.Compilation.AssemblyName },
                StringSplitOptions.RemoveEmptyEntries);
            
            if (paths.Length == 1) // spec file is at the root of the project
            {
                return Path.Combine(paths[0], GetTestProject());
            }
            if (paths.Length == 2)
            {
                return Path.Combine(paths[0], GetTestProject(), paths[1].TrimStart('\\'));
            }

            return specPath;
        }

        private SpecFirstSettings Parse(string settingFile)
        {
            XDocument settings = XDocument.Load(settingFile);
            return new SpecFirstSettings
            {
                TestingFramework = settings.Descendants("TestingFramework").FirstOrDefault()?.Value ?? DefaultTestingFramework,
                SpecFileExtension = settings.Descendants("SpecFileExtension").FirstOrDefault()?.Value ?? DefaultSpecFileExtension,
                TestProject = settings.Descendants("TestProject").FirstOrDefault()?.Value ?? DefaultTestFileName,
                TestFile = settings.Descendants("TestFile").FirstOrDefault()?.Value ?? string.Empty,
                TestImplFile = settings.Descendants("ImplFile").FirstOrDefault()?.Value ?? string.Empty,
            };
        }

        private SpecFirstSettings Default()
        {
            return new SpecFirstSettings
            {
                TestingFramework = DefaultTestingFramework,
                SpecFileExtension = DefaultSpecFileExtension,
                TestProject = DefaultTestProject,
                TestFile = DefaultTestFileName,
                TestImplFile = DefaultImplementationFileName,
            };
        }
    }
}
