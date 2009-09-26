#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

[assembly : AssemblyTitle("FileHelpers Lib   http://www.filehelpers.com")]
[assembly : AssemblyDescription("An easy to use file library for .NET that supports automatic formated file read/write operations.")]
[assembly : AssemblyProduct("FileHelpers   http://www.filehelpers.com")]
[assembly : ReflectionPermission(SecurityAction.RequestMinimum, ReflectionEmit = true)]
[assembly : SecurityPermission(SecurityAction.RequestMinimum, SerializationFormatter = true)]

[assembly : AssemblyVersion("2.2.0.0")]
[assembly : AssemblyCompany("Marcos Meli")]
[assembly : AssemblyCopyright("Copyright 2005-07. Marcos Meli")]
[assembly : AssemblyTrademark("FileHelpers")]
[assembly : AssemblyCulture("")]
[assembly : AssemblyConfiguration("")]
[assembly : ComVisible(false)]

[assembly : AssemblyDelaySign(false)]
[assembly : AssemblyKeyName("")]

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("FileHelpers.Tests")]
