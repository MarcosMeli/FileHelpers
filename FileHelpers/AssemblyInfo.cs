#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
#if ! MINI
[assembly : AssemblyTitle("FileHelpers Lib   http://www.filehelpers.com")]
[assembly : AssemblyDescription("A simple to use file library for .NET that supports automatic formated file read/write operations.")]
[assembly : AssemblyProduct("FileHelpers   http://www.filehelpers.com")]
[assembly : ReflectionPermission(SecurityAction.RequestMinimum, ReflectionEmit = true)]
[assembly : SecurityPermission(SecurityAction.RequestMinimum, SerializationFormatter = true)]
#else
[assembly : AssemblyTitle("FileHelpers Lib (Pocket PC)  http://www.filehelpers.com")]
[assembly : AssemblyDescription("A simple to use file library for .NET Compact Framework that supports automatic formated file read/write operations.")]
[assembly : AssemblyProduct("FileHelpersPPC   http://www.filehelpers.com")]
#endif

[assembly : AssemblyVersion("1.7.0.0")]
[assembly : AssemblyCompany("Marcos Meli")]
[assembly : AssemblyCopyright("Copyright 2005-06. Marcos Meli")]
[assembly : AssemblyTrademark("FileHelpers")]
[assembly : AssemblyCulture("")]
[assembly : AssemblyConfiguration("")]
[assembly : ComVisible(false)]

[assembly : AssemblyDelaySign(false)]
[assembly : AssemblyKeyName("")]

//#if STRONG_NAME
[assembly: AssemblyKeyFile(@"..\..\FileHelpers.snk")]
//#endif