# FileHelpers [![Join the chat at https://gitter.im/MarcosMeli/FileHelpers](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/MarcosMeli/FileHelpers?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge) 

<a href="http://www.filehelpers.net"><img src="http://www.filehelpers.net/images/homepage.jpg"  /></a>

www.filehelpers.net

[![GitHub license](https://img.shields.io/github/license/MarcosMeli/FileHelpers.svg)](https://github.com/MarcosMeli/FileHelpers#license)
[![NuGet](https://img.shields.io/nuget/vpre/FileHelpers.svg)](https://www.nuget.org/packages/FileHelpers/) [![NuGet](https://img.shields.io/nuget/dt/FileHelpers.svg)](https://www.nuget.org/packages/FileHelpers/)
[![Stories in Backlog](https://badge.waffle.io/MarcosMeli/FileHelpers.png?label=backlog&title=Issues+Pending)](https://waffle.io/MarcosMeli/FileHelpers?milestone=3.1)
[![Stories in Ready](https://badge.waffle.io/MarcosMeli/FileHelpers.png?label=ready&title=Issues+Ready)](https://waffle.io/MarcosMeli/FileHelpers?milestone=3.1)
[![Stories in progress](https://badge.waffle.io/MarcosMeli/FileHelpers.png?label=in%20progress&title=Issues+In%20Progress)](https://waffle.io/MarcosMeli/FileHelpers?milestone=3.1)

  The FileHelpers are a **free and easy to use** .NET library to read/write data from fixed length or delimited records in files, strings or streams
 
  If you want to start using the library go directly to the Quick Start Guide in the CHM.

### Build Status


TeamCity: master: [![TeamCity CodeBetter](https://img.shields.io/teamcity/codebetter/FileHelpersMaster.svg)](http://teamcity.codebetter.com/viewType.html?buildTypeId=FileHelpersMaster) stable: [![TeamCity CodeBetter](https://img.shields.io/teamcity/codebetter/FileHelpersStable.svg)](http://teamcity.codebetter.com/viewType.html?buildTypeId=FileHelpersStable)  Coverage: [![TeamCity CodeBetter Coverage](https://img.shields.io/teamcity/coverage/FileHelpersStable.svg)](http://teamcity.codebetter.com/viewLog.html?buildId=lastSuccessful&buildTypeId=FileHelpersStable&tab=coverage_dotnet)

Travis: master: [![Build Status](https://travis-ci.org/MarcosMeli/FileHelpers.svg?branch=master)](https://travis-ci.org/MarcosMeli/FileHelpers)  stable: [![Build Status](https://travis-ci.org/MarcosMeli/FileHelpers.svg?branch=stable)](https://travis-ci.org/MarcosMeli/FileHelpers/branches)

AppVeyor [![Build status](https://ci.appveyor.com/api/projects/status/pi6ipa7wd4vqws35/branch/master?svg=true)](https://ci.appveyor.com/project/MarcosMeli/filehelpers/branch/master)   [![Coverage Status](https://coveralls.io/repos/MarcosMeli/FileHelpers/badge.svg?branch=master&service=github)](https://coveralls.io/github/MarcosMeli/FileHelpers?branch=master)


###Downloads


You can **download** the last stable version from:

####[Nuget Prerelease packages](https://www.nuget.org/packages/FileHelpers/)

####[Build Server: Last Stable Build](http://teamcity.codebetter.com/viewLog.html?buildId=lastSuccessful&buildTypeId=FileHelpersStable&tab=artifacts&guest=1)

    Download the zip with the format: FileHelpers_x.x.x_Build.zip


###Delimited Example 


Source Data
```
    1732,Juan Perez,435.00,11-05-2002 
    554,Pedro Gomez,12342.30,06-02-2004 
    112,Ramiro Politti,0.00,01-02-2000 
    924,Pablo Ramirez,3321.30,24-11-2002 
```
Record Type

```csharp
	[DelimitedRecord(",")]
	public class Customer
	{
		public int CustId;
		
		public string Name;

		public decimal Balance;

		[FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
		public DateTime AddedDate;
	}
```
Usage

```csharp
  var engine = new FileHelperEngine<Customer>();

  // To Read Use:
  Customer[] res = engine.ReadFile("FileIn.txt");

  // To Write Use:
  engine.WriteFile("FileOut.txt", res);
```

[Check the QuickStart](http://www.filehelpers.net/quickstart/) and [More Examples ](http://www.filehelpers.net/examples/)

###Who needs the File Helpers Library ? 


  In almost every project there is a need to read/write data from/to a file of a specified format.

  For example for log parsing, data warehouse and OLAP applications, 
  communication between systems, file format transformations 
  (for example from a fixed length to a CSV file).

  This library aims to provide an easy and reliable way to accomplish this task.


### License

 The FileHelpers are released under the **MIT License**
 
 FileHelpers Library source and binaries are **completely free for commercial and non commercial use**

###Sponsors

 Our build server is kindly provided by [CodeBetter](http://codebetter.com/) at [FileHelpers Builds](http://teamcity.codebetter.com/project.html?tab=projectOverview&projectId=FileHelpers)

 We have also the awesome .Net tools from [JetBrains](http://www.jetbrains.com/).

[![Resharper](http://www.filehelpers.net/images/tools_resharper.gif)](http://www.jetbrains.com/resharper/)
[![TeamCity](http://www.filehelpers.net//images/tools_teamcity.gif)](http://www.jetbrains.com/teamcity/)
[![dotCover](http://www.filehelpers.net//images/tools_dotcover.gif)](http://www.jetbrains.com/dotcover/)
[![dotTrace](http://www.filehelpers.net//images/tools_dottrace.gif)](http://www.jetbrains.com/dottrace/)
