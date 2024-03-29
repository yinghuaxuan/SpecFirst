#### [![CI](https://github.com/yinghuaxuan/SpecFirst.xUnit/workflows/ci/badge.svg)](https://github.com/yinghuaxuan/SpecFirst.xUnit/actions?query=workflow%3ACI) [![Nuget](https://img.shields.io/nuget/v/SpecFirst)](https://www.nuget.org/packages/SpecFirst/)

# SpecFirst
SpecFirst is a .NET [source generator](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) to automatically generate tests in target frameworks from [decision tables](https://github.com/yinghuaxuan/spec-first/blob/develop/tests/SpecFirst.Specs/DecisionTable/Validator/DecisionTable.spec.md) authored in markdown.

## The goal
- Specs as your documentation / communication / collaboration tool
- Auto-generate tests in your favourite framework (currently your favourite has to be xUnit)
- Seamless integration with Visual Studio
- Reduce the time & complexity in authoring and maintaining tests

## How does it work
SpecFirst has the following components:
- a spec file  
A spec file is any markdown file with .spec.md suffix. If there are decision tables in the spec file, they will be converted into xUnit tests. A spec file must be included as 'C# analyzer additional file' in 'Build Action' in order to be considered.  
Sample spec files can be found [DecisionTable.spec.md](https://github.com/yinghuaxuan/SpecFirst.Core/blob/main/tests/SpecFirst.Core.Specs/DecisionTable/Validator/DecisionTable.spec.md), [PrimitiveTypeResolver.spec.md](https://github.com/yinghuaxuan/SpecFirst.Core/blob/main/tests/SpecFirst.Core.Specs/TypeResolver/PrimitiveTypeResolver.spec.md), and [CollectionTypeResolver.spec.md](https://github.com/yinghuaxuan/SpecFirst.Core/blob/main/tests/SpecFirst.Core.Specs/TypeResolver/CollectionTypeResolver.spec.md)
- a source generator  
A source generator converts decision tables in markdown format to tests in target frameworks. SpecFirst is more like a shell and it requires two more packages in order to work: the markdown parser and the test generator.
- a markdown parser  
A markdown parser converts the markdown text to html, extract decision tables in html, and returns the decision table objects. A markdown parser must implement the [IDecisionTableMarkdownParser](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst.Core/IDecisionTableMarkdownParser.cs) interface from [SpecFirst.Core](https://www.nuget.org/packages/SpecFirst.Core/) package.  
A sample of the markdown parser can be seen here: [SpecFirst.MarkdownParser](https://github.com/yinghuaxuan/SpecFirst.MarkdownParser)
- a test generator  
A test generator is the engine to generate tests in target frameworks. A test generator must implement the [ITestsGenerator](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst.Core/ITestsGenerator.cs) interface from [SpecFirst.Core](https://www.nuget.org/packages/SpecFirst.Core/) package.  
A sample of the test generator can be seen here: [SpecFirst.TestGeneration.xUnit](https://github.com/yinghuaxuan/SpecFirst.TestGeneration.xUnit)  
Sample generated tests can be found [DecisionTableTests.g.cs](https://github.com/yinghuaxuan/SpecFirst.Core/blob/main/tests/SpecFirst.Core.Specs.Tests/DecisionTable/Validator/DecisionTableTests.g.cs), [PrimitiveTypeResolverTests.g.cs](https://github.com/yinghuaxuan/SpecFirst.Core/blob/main/tests/SpecFirst.Core.Specs.Tests/TypeResolver/PrimitiveTypeResolverTests.g.cs), and [CollectionTypeResolverTests.g.cs](https://github.com/yinghuaxuan/SpecFirst.Core/blob/main/tests/SpecFirst.Core.Specs.Tests/TypeResolver/CollectionTypeResolverTests.g.cs)  
- a config file (optional)  
The config file must be named as specfirst.config and included as 'C# analyzer additional file' in 'Build Action'. A sample of such a config file can be found [specfirst.config](https://github.com/yinghuaxuan/SpecFirst.Core/blob/main/tests/SpecFirst.Core.Specs/specfirst.config)

## Usage
- Add a spec file (.spec.md) containing some decision tables to the current project 
- Mark the file as 'C# analyzer additional file' in 'Build Action'
- Install [SpecFirst](https://www.nuget.org/packages/SpecFirst/) nuget package
- Install [SpecFirst.MarkdownParser](https://www.nuget.org/packages/SpecFirst.MarkdownParser/) nuget package
- Install [SpecFirst.TestGenerator.xUnit](https://www.nuget.org/packages/SpecFirst.xUnit/) nuget package
- Add a config file named [specfirst.config](https://github.com/yinghuaxuan/spec-first/blob/develop/tests/SpecFirst.Specs/specfirst.config) (optional - [default settings](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst/Setting/SpecFirstSettingManager.cs#L11) will be used instead if the config file not present)
- Rebuild the current project  
- Two test files will be auto-generated for each spec file containing at least one decision table: one for the skeleton of the tests and the other for the implementation of the tests.  

## Known Issues
- The sample spec files above writes decision tables with [markdown-it-multimd-table](https://github.com/redbug312/markdown-it-multimd-table) format, which is not supported by GitHub. To properly view these spec files, use VSCode with [Markdown Extended](https://marketplace.visualstudio.com/items?itemName=jebbs.markdown-extended) extension.
- The [source generator](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) project is a great work but it is still evolving and currently missing a couple of important features for this project:
    - Persist generated files to disk (see [output files](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.md#output-files) for more details.)  
    I workaround this issue by writing the generated text to disk using File.WriteAllText method. However, adding a file to the current project hierarchy will cause Visual Studio to recompile the project, which leads to a dead loop between regeneration/recompiling. I workaround this issue by persisting the generated test files to a separate test project (via [this configuration](https://github.com/yinghuaxuan/spec-first/blob/develop/tests/SpecFirst.Specs/specfirst.config#L6)).  
    - Recompile the spec files only when the spec files are changed (see [participate-in-the-ide-experience](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md#participate-in-the-ide-experience) and [progressive-complexity-opt-in](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.md#progressive-complexity-opt-in) for more details)  
    I don't have any reasonable workaround for this issue, which means Visual Studio will regenerate the tests whenever it recompiles the projects where the source generator is added to. If this is too big an issue, remove the source generator package (SpecFirst) once tests are generated.  
- An unhandled Microsoft .NET Framework exception occurred in devenv.exe  
I seem to get this error very frequently either on loading the solution initially or during the solution/project getting recompiled. It gives me the chance to choose the debugger but I don't see any error from my code during debugging.

## Extending SpecFirst
THere are two extension points for SpecFirst:
- Markdown parser  
To use your own markdown parser, implement the [IDecisionTableMarkdownParser](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst.Core/IDecisionTableMarkdownParser.cs) in [SpecFirst.Core](https://www.nuget.org/packages/SpecFirst.Core/) package. The input is the raw markdown text and the output is a collection of [decision table object](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst.Core/DecisionTable/DecisionTable.cs) for each decision table in the markdown text.  
The default implementation can be seen here: [DecisionTableMarkdownParser.cs](https://github.com/yinghuaxuan/SpecFirst.MarkdownParser/blob/main/src/SpecFirst.MarkdownParser/DecisionTableMarkdownParser.cs). 
- Test generator  
To use your own test generator, implement [ITestsGenerator](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst.Core/ITestsGenerator.cs) interface from [SpecFirst.Core](https://www.nuget.org/packages/SpecFirst.Core/) package. The input are the generation configuration and the decision table objects from the markdown parser and the output are two text blocks: one is the skeleton for the tests and the other one is the skeleton for the test implementations.  
The default implementation can be seen here: [XUnitTestsGenerator.cs](https://github.com/yinghuaxuan/SpecFirst.TestGenerator.xUnit/blob/main/src/SpecFirst.TestGenerator.xUnit/XUnitTestsGenerator.cs).

## Credits
- [FitNesse](http://docs.fitnesse.org/FrontPage)  
I have used FitNesse at work for 5+ years and been very impressed by the power and simplicity of the decision tables. This project implements the decsion tables in markdown language with auto-generated tests skeleton and seamless integration with Visual Studio.
- [Source generator](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.md) - compile time metaprogramming 
- [xUnit](https://github.com/xunit/xunit) - popular unit testing framework for .NET
- [markdown-it](https://github.com/markdown-it/markdown-it) - a popular markdown parser in Javascript
- [markdown-it-multimd-table](https://github.com/redbug312/markdown-it-multimd-table) - MultiMarkdown table syntax plugin for markdown-it markdown parser
- [Markdown Extended](https://marketplace.visualstudio.com/items?itemName=jebbs.markdown-extended) - an brilliant vscode extension for markdown
- [Jurassic](https://github.com/paulbartrum/jurassic) - run Javascript code in .NET
