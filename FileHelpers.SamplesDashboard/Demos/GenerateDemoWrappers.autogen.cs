using FileHelpers;
using System.Diagnostics;
using System; 

namespace FileHelpers.Demos
{
public partial class Demos
{
public partial class ReadFile: DemoCode
{

 protected override string GetFullPathName()
	   	    { return @"ReadFile.cs"; }


}
public partial class WriteFile: DemoCode
{

 protected override string GetFullPathName()
	   	    { return @"WriteFile.cs"; }


}



}
public partial class GenerateDemoWrappers.tt
{
public partial class GenerateDemoWrappers.autogen: DemoCode
{

 protected override string GetFullPathName()
	   	    { return @"GenerateDemoWrappers.autogen.cs"; }


}



}

}namespace FileHelpers.Tests
{
public partial class FileTest
{
public partial class Demos
{

private static ClassesFileTest.Demos.ReadFile mReadFile = new ClassesFileTest.Demos.ReadFile();
public static ClassesFileTest.Demos.ReadFile ReadFile
{ get { return  mReadFile; } }
private static ClassesFileTest.Demos.WriteFile mWriteFile = new ClassesFileTest.Demos.WriteFile();
public static ClassesFileTest.Demos.WriteFile WriteFile
{ get { return  mWriteFile; } }


}
public partial class GenerateDemoWrappers.tt
{

private static ClassesFileTest.GenerateDemoWrappers.tt.GenerateDemoWrappers.autogen mGenerateDemoWrappers.autogen = new ClassesFileTest.GenerateDemoWrappers.tt.GenerateDemoWrappers.autogen();
public static ClassesFileTest.GenerateDemoWrappers.tt.GenerateDemoWrappers.autogen GenerateDemoWrappers.autogen
{ get { return  mGenerateDemoWrappers.autogen; } }


}



}

}

