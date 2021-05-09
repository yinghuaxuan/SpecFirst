# SpecFirst
SpecFirst is a .NET [source generator](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) to automatically generate tests in target frameworks from [decision tables](https://github.com/yinghuaxuan/spec-first/blob/develop/tests/SpecFirst.Specs/DecisionTable/Validator/DecisionTable.spec.md) authored in markdown.

## The goal
- Test specs as your documentation / communication / collaboration tool
- Seamless integration with Visual Studio
- Generate tests in your favourite framework (currently your favourite has to be xUnit)
- Reduce the complexity in authoring and maintaining tests

## How does it work
SpecFirst has the following components:
- a spec file  
A spec file is any markdown file with .spec.md suffix. If there are decision tables in the spec file, SpecFirst will convert these tables into xUnit tests. A spec file must be included as 'C# analyzer additional file' in 'Build Action' in order to be considered by SpecFirst generator.
- a source generator
SpecFirst package is a source generator that converts decision tables in markdown format to tests in target frameworks. SpecFirst is more like a shell and it requires two more packages in order to work: the markdown parser and the test generator.
- a markdown parser  
A markdown parser converts the markdown text to html and SpecFirst uses the parsed html to extract decision tables and returns the decision table objects.  
Technically any markdown parser that can convert decision tables in markdown format into valid decision tables in html can be used here. The SpecFirst.MarkdownParser package uses the markdown-it parser with markdown-it-multimd-table support for its intuitive way to author decision tables. A markdown parser must implement the IDecisionTableMarkdownParser interface from SpecFirst.Core package.   
- a test generator  
A test generator is the engine to generate tests in target frameworks. SpecFirst.xUnit is a generator to generate tests in xUnit framework. A test generator must implement the ITestsGenerator interface from SpecFirst.Core package.   

## Usage

### Known issues

## Features
- Extensible architecture
- Simple yet powwerful decision tables
- 

## Credits
- FitNesse