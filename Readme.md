# FileHelpers

<a href="https://www.filehelpers.net"><img src="http://www.filehelpers.net/images/homepage.jpg"  /></a>

www.filehelpers.net

[![GitHub license](https://img.shields.io/github/license/MarcosMeli/FileHelpers.svg)](https://github.com/MarcosMeli/FileHelpers#license)
[![NuGet](https://img.shields.io/nuget/vpre/FileHelpers.svg)](https://www.nuget.org/packages/FileHelpers/) [![NuGet](https://img.shields.io/nuget/dt/FileHelpers.svg)](https://www.nuget.org/packages/FileHelpers/)

  The FileHelpers are a **free and easy to use** .NET library to read/write data from fixed length or delimited records in files, strings or streams.

### Current support for .NET Standard / .NET Core

Support for .NET Core is available through .NET Standard.
Since version 3.3.0 **support** for **.NET Standard 2.0** is available!
There is a FileHelpers dll targeting .NET Standard 2.0 in the FileHelpers NuGet package.

The **.NET Standard FileHelpers** dll is a **subset** of the **.NET Framework FileHelpers** dll.
The .NET Standard FileHelpers dll contains:
* FileHelperEngine
* MasterDetailEngine
* Attributes
* Converters
* Events
* ErrorManager
* Sorting of big files

The .NET Standard FileHelpers dll currently does **not** contain the following features:
* Dynamic: ClassBuilder and CsvClassBuilder. ClassBuilder is difficult to maintain and a rewrite in
  .NET Core / Standard as is, would require to reference three NuGet packages. There must be better
  ways to write record structures at runtime.
* SmartFormatDetector: depends on ClassBuilder (Dynamic).
* DataLink: code was very old and seemed not worth maintaining.

We do not have any plans to port the FileHelpers.ExcelNPOIStorage nor FileHelpers.ExcelStorage to .NET Core or .NET Standard.

### Continuous Integration

AppVeyor [![Build status](https://ci.appveyor.com/api/projects/status/pi6ipa7wd4vqws35/branch/master?svg=true)](https://ci.appveyor.com/project/MarcosMeli/filehelpers/branch/master)

### Prerelease NuGet Packages

NuGet feed

    https://ci.appveyor.com/nuget/filehelpers

[Manual download](https://ci.appveyor.com/project/marcosmeli/filehelpers/build/artifacts)

### License

 The FileHelpers are released under the **MIT License**.
 FileHelpers Library source and binaries are **completely free for commercial and non commercial use**.

### Sponsors

 We have the awesome .NET tools from [JetBrains](http://www.jetbrains.com/).

[![Resharper](https://www.filehelpers.net/images/tools_resharper.gif)](http://www.jetbrains.com/resharper/)
[![dotCover](https://www.filehelpers.net//images/tools_dotcover.gif)](http://www.jetbrains.com/dotcover/)
[![dotTrace](https://www.filehelpers.net//images/tools_dottrace.gif)](http://www.jetbrains.com/dottrace/)
