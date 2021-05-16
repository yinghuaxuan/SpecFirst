namespace SpecFirst.Setting
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.CodeAnalysis;
    using SpecFirst.Core.Setting;

    public class SpecFirstSettingManager
    {
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

            Settings.TestGeneration.TestProject = GetTestProject();
        }

        public SpecFirstSettings Settings { get; }

        public string GetTestProject()
        {
            return Settings.TestGeneration.TestProject!.Replace("{spec_project}", _context.Compilation.AssemblyName);
        }

        public string GetSpecName(AdditionalText specFile)
        {
            return $"{new FileInfo(specFile.Path).Name.Replace(Settings.SpecFileExtension, "")}";
        }

        public string GetTestFile(AdditionalText specFile)
        {
            return Settings.TestGeneration.TestFileName.Replace("{spec_name}", GetSpecName(specFile));
        }

        public string GetTestImplFile(AdditionalText specFile)
        {
            return Settings.TestGeneration.TestImplFileName.Replace("{spec_name}", GetSpecName(specFile));
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
                TestingFramework = settings.Descendants("TestingFramework").FirstOrDefault()?.Value ?? SpecFirstSettings.DefaultTestingFramework,
                SpecFileExtension = settings.Descendants("SpecFileExtension").FirstOrDefault()?.Value ?? SpecFirstSettings.DefaultSpecFileExtension,
                TestGeneration = new TestGeneration
                {
                    TestProject = settings.Descendants("TestProject").FirstOrDefault()?.Value ?? SpecFirstSettings.DefaultTestFileName,
                    TestFileName = settings.Descendants("TestFileName").FirstOrDefault()?.Value ?? SpecFirstSettings.DefaultTestFileName,
                    TestImplFileName = settings.Descendants("ImplFileName").FirstOrDefault()?.Value ?? SpecFirstSettings.DefaultImplementationFileName,
                }
            };
        }

        private SpecFirstSettings Default()
        {
            return new SpecFirstSettings
            {
                TestingFramework = SpecFirstSettings.DefaultTestingFramework,
                SpecFileExtension = SpecFirstSettings.DefaultSpecFileExtension,
                TestGeneration = new TestGeneration
                {
                    TestProject = SpecFirstSettings.DefaultTestProject,
                    TestFileName = SpecFirstSettings.DefaultTestFileName,
                    TestImplFileName = SpecFirstSettings.DefaultImplementationFileName,
                }
            };
        }
    }
}
