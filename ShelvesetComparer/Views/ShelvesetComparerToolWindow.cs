// <copyright file="ShelvesetComparerToolWindow.cs" company="https://github.com/rajeevboobna/shelvesetcomparer">Copyright https://github.com/rajeevboobna/shelvesetcomparer. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>
namespace WiredTechSolutions.ShelvesetComparer
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    [Guid("0866ec06-7a85-4e90-8244-4da11ad1677b")]
    public sealed class ShelvesetComparerToolWindow : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the ShelvesetComparerToolWindow class.
        /// </summary>
        public ShelvesetComparerToolWindow() : base(null)
        {
            this.Caption = Resources.ToolWindowTitle;
            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;
            this.Content = new MainView();
        }
    }
}
