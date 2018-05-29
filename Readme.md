# FileHelpers [![Join the chat at https://gitter.im/MarcosMeli/FileHelpers](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/MarcosMeli/FileHelpers?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

<a href="https://www.filehelpers.net"><img src="http://www.filehelpers.net/images/homepage.jpg"  /></a>

www.filehelpers.net

[![GitHub license](https://img.shields.io/github/license/MarcosMeli/FileHelpers.svg)](https://github.com/MarcosMeli/FileHelpers#license)
[![NuGet](https://img.shields.io/nuget/vpre/FileHelpers.svg)](https://www.nuget.org/packages/FileHelpers/) [![NuGet](https://img.shields.io/nuget/dt/FileHelpers.svg)](https://www.nuget.org/packages/FileHelpers/)
[![Stories in Ready](https://badge.waffle.io/MarcosMeli/FileHelpers.png?label=ready&title=Issues+Ready)](https://waffle.io/MarcosMeli/FileHelpers)
[![Stories in progress](https://badge.waffle.io/MarcosMeli/FileHelpers.png?label=in%20progress&title=Issues+In%20Progress)](https://waffle.io/MarcosMeli/FileHelpers)

  The FileHelpers are a **free and easy to use** .NET library to read/write data from fixed length or delimited records in files, strings or streams.

### Current support for .NET Core

In the current **alpha** releases is **support** for **.NET Core**! These releases are available in the appveyor NuGet repository (see below).

.NET Core support is directly available, not via .NET Standard. That means there is a dll directly targeting
.NET Core 2.0 in the FileHelpers NuGet package.

The **.NET Core FileHelpers** dll is a **subset** of the **.NET Framework FileHelpers** dll.
The .NET Core FileHelpers dll contains:
* FileHelperEngine
* MasterDetailEngine
* Attributes
* Converters
* Events
* ErrorManager
* Sorting of big files

It currently does **not** contain the following features:
* Dynamic: ClassBuilder and CsvClassBuilder. ClassBuilder is difficult to maintain and a rewrite in
  .NET Core as is, would require reference three NuGet packages. There must be better ways
  to write record structures at runtime.
* SmartFormatDetector: depends on ClassBuilder (Dynamic).
* DataLink: code was very old and seemed not worth maintaining.

We do not have any plans to port the FileHelpers.ExcelNPOIStorage nor FileHelpers.ExcelStorage to .NET Core or .NET Standard.

### Continuous Integration

AppVeyor [![Build status](https://ci.appveyor.com/api/projects/status/pi6ipa7wd4vqws35/branch/master?svg=true)](https://ci.appveyor.com/project/MarcosMeli/filehelpers/branch/master)

Travis [![Build Status](https://travis-ci.org/MarcosMeli/FileHelpers.svg?branch=master)](https://travis-ci.org/MarcosMeli/FileHelpers)

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
