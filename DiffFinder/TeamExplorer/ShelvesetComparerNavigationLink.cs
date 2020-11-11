// <copyright file="ShelvesetComparerNavigationLink.cs" company="https://github.com/rajeevboobna/CompareShelvesets">Copyright https://github.com/rajeevboobna/CompareShelvesets. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>

using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Composition;

namespace DiffFinder
{
    /// <summary>
    /// The class creates the navigation link for Shelveset Comparer extension.
    /// </summary>
    [TeamExplorerNavigationLink(ShelvesetComparerNavigationLink.LinkId, TeamExplorerNavigationItemIds.MyWork, 1000)]
    public class ShelvesetComparerNavigationLink : TeamExplorerBaseNavigationLink
    {
        /// <summary>
        /// The unique Id given to the navigation link
        /// </summary>
        public const string LinkId = "A1EC4AFD-FEBF-499B-82F7-E51F987D30D2";

        /// <summary>
        /// Initializes a new instance of the ShelvesetComparerNavigationLink class.
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        [ImportingConstructor]
        public ShelvesetComparerNavigationLink([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.Text = Resources.TeamExplorerLinkCaption;
            this.IsVisible = true;
            this.IsEnabled = true;
        }

        /// <summary>
        /// Overridden method called when the navigation link is clicked.
        /// </summary>
        public override void Execute()
        {
            TeamExplorer.NavigateToShelvesetComparer();
        }

        /// <summary>
        /// Overridden method called when the navigation link is refreshed.
        /// </summary>
        public override void Invalidate()
        {
            base.Invalidate();
            if (this.CurrentContext != null && this.CurrentContext.HasCollection && this.CurrentContext.HasTeamProject)
            {
                this.IsVisible = true;
            }
            else
            {
                this.IsVisible = false;
            }
        }
    }
}
