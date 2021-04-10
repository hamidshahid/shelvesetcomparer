// <copyright file="AssemblyInfo.cs" company="http://shelvesetcomparer.codeplex.com">Copyright http://shelvesetcomparer.codeplex.com. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>

#if DEBUG

// activate to get fake shelvest results with delay for debugging
#define FakeShelvesetResult

#endif

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ShelvesetComparer")]
[assembly: AssemblyDescription("Visual Studio extension for comparing shelvesets.")]
[assembly: AssemblyCompany("Hamid Shahid")]
[assembly: AssemblyProduct("ShelvesetComparer")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]
[assembly: NeutralResourcesLanguage("en-US")]

[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]