# SpecFirst
SpecFirst is a .NET [source generator](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) to automatically generate tests in target frameworks from [decision tables](https://github.com/yinghuaxuan/spec-first/blob/develop/tests/SpecFirst.Specs/DecisionTable/Validator/DecisionTable.spec.md) authored in markdown.

## The goal
- Specs as your documentation / communication / collaboration tool
- Seamless integration with Visual Studio
- Generate tests in your favourite framework (currently your favourite has to be xUnit)
- Reduce the complexity in authoring and maintaining tests

## How does it work
SpecFirst has the following components:
- a spec file  
A spec file is any markdown file with .spec.md suffix. If there are decision tables in the spec file, SpecFirst will convert these tables into xUnit tests. A spec file must be included as 'C# analyzer additional file' in 'Build Action' in order to be considered by SpecFirst generator.  
Sample spec files can be found [DecisionTable.spec.md](https://github.com/yinghuaxuan/spec-first/blob/develop/tests/SpecFirst.Specs/DecisionTable/Validator/DecisionTable.spec.md), [ScalaValueTypeResolver.spec.md](https://github.com/yinghuaxuan/spec-first/tree/develop/tests/SpecFirst.Specs/TypeResolver), and [CollectionTypeResolver.spec.md](https://github.com/yinghuaxuan/spec-first/tree/develop/tests/SpecFirst.Specs/TypeResolver)
- a source generator  
SpecFirst package is a source generator that converts decision tables in markdown format to tests in target frameworks. SpecFirst is more like a shell and it requires two more packages in order to work: the markdown parser and the test generator.
- a markdown parser  
A markdown parser converts the markdown text to html and SpecFirst uses the parsed html to extract decision tables and returns the decision table objects.  
Technically any markdown parser that can convert decision tables in markdown format into valid decision tables in html can be used here. The SpecFirst.MarkdownParser package uses the [markdown-it](https://github.com/markdown-it/markdown-it) parser with [markdown-it-multimd-table](https://github.com/redbug312/markdown-it-multimd-table) support for its intuitive way to author decision tables. A markdown parser must implement the [IDecisionTableMarkdownParser](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst.Core/IDecisionTableMarkdownParser.cs) interface from SpecFirst.Core package.   
- a test generator  
A test generator is the engine to generate tests in target frameworks. SpecFirst.xUnit is a generator to generate tests in xUnit framework. A test generator must implement the [ITestsGenerator](https://github.com/yinghuaxuan/spec-first/blob/develop/src/SpecFirst.Core/ITestsGenerator.cs) interface from SpecFirst.Core package.  
Sample generated tests can be found [DecisionTableTests.g.cs](https://github.com/yinghuaxuan/spec-first/tree/develop/tests/SpecFirst.Specs.Tests/DecisionTable/Validator), [ScalaValueTypeResolverTests.g.cs](https://github.com/yinghuaxuan/spec-first/tree/develop/tests/SpecFirst.Specs.Tests/TypeResolver), and [CollectionTypeResolverTests.g.cs](https://github.com/yinghuaxuan/spec-first/tree/develop/tests/SpecFirst.Specs.Tests/TypeResolver)  
- a config file (optional)  
The config file must be included as 'C# analyzer additional file' in 'Build Action' in order to be effective. A sample of such a config file can be found [specfirst.config](https://github.com/yinghuaxuan/spec-first/blob/develop/tests/SpecFirst.Specs/specfirst.config)

## Usage
- Add a spec file (.spec.md) to the project containing some decision tables
- Mark the file as 'C# analyzer additional file' in 'Build Action'
- Install SpecFirst nuget package
- Install SpecFirst.MarkdownParser nuget package
- SpecFirst.xUnit nuget package
- Add a config file (optional)
- Rebuild the project  
The generated tests file will be persisted based on the configuration. If no config file is provided, generated tests will be persisted in the same place as the spec file.

### Known issues
- The sample spec files above creates decision tables in [markdown-it-multimd-table](https://github.com/redbug312/markdown-it-multimd-table) format, which is not supported by GitHub. To properly view these spec files, use VSCode with [Markdown Extended](https://marketplace.visualstudio.com/items?itemName=jebbs.markdown-extended) extension.
- The [source generator](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) project is still evolving and currently missing a couple of important features for this project:
    - Persistence of generated files
    - Recompile the spec files only when the spec files are changed

## Extending SpecFirst
- Markdown parser
- Test generator