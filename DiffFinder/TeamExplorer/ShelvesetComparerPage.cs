// <copyright file="ShelvesetComparerPage.cs" company="https://github.com/rajeevboobna/CompareShelvesets">Copyright https://github.com/rajeevboobna/CompareShelvesets. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>

using System;
using Microsoft.TeamFoundation.Controls;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace DiffFinder
{
    /// <summary>
    /// The class creates the team explorer page for Shelveset Comparer extension.
    /// </summary>
    [TeamExplorerPage(ShelvesetComparerPage.PageId)]
    public class ShelvesetComparerPage : TeamExplorerBasePage
    {
        /// <summary>
        /// The unique id of the Team explorer page
        /// </summary>
        public const string PageId = "{9A984CF5-9D99-4C24-BDCB-E53A0D174343}";

        /// <summary>
        /// Initializes a new instance of the ShelvesetComparerPage class
        /// </summary>
        public ShelvesetComparerPage()
        {
            this.Title = Resources.ToolWindowTitle;
        }

        public override void Initialize(object sender, PageInitializeEventArgs e)
        {
            base.Initialize(sender, e);
            var context = e.Context as Workspace;
            CurrentWorkspace = context;
        }

        public override object GetExtensibilityService(Type serviceType)
        {
            if (serviceType == GetType())
            {
                return this;
            }
            return base.GetExtensibilityService(serviceType);
        }

        public Workspace CurrentWorkspace { get; protected set; }
    }
}
