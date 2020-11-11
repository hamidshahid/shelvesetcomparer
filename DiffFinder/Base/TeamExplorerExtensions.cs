// <copyright file="SelectShelvesetSection.cs" company="https://github.com/rajeevboobna/CompareShelvesets">
// Copyright https://github.com/rajeevboobna/CompareShelvesets. All Rights Reserved. 
// This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html). 
// This is sample code only, do not use in production environments.
// </copyright>

using Microsoft.TeamFoundation.Controls;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.VersionControl.Controls.Extensibility;
using System;

namespace DiffFinder
{
    public static class TeamExplorerExtensions
    {
        public static Guid ShowNotification(this ITeamExplorer teamExplorer, string message, NotificationType type)
        {
            if (null != teamExplorer)
            {
                Guid guid = Guid.NewGuid();
                teamExplorer.ShowNotification(message, type, NotificationFlags.None, null, guid);
                return guid;
            }

            return Guid.Empty;
        }

        public static ITeamExplorerPage NavigateToShelvesetDetails(this ITeamExplorer teamExplorer, Shelveset shelveset)
        {
            if (null != teamExplorer)
            {
                return teamExplorer.NavigateToPage(new Guid(TeamExplorerPageIds.ShelvesetDetails), shelveset);
            }

            return null;
        }

        public static ITeamExplorerPage NavigateToShelvesetComparer(this ITeamExplorer teamExplorer)
        {
            try
            {
                if (teamExplorer != null)
                {
                    teamExplorer.NavigateToPage(new Guid(TeamExplorerPageIds.PendingChanges), null);
                    var pendingChangesExt = teamExplorer.CurrentPage?.GetExtensibilityService(typeof(IPendingChangesExt)) as IPendingChangesExt;
                    var ws = pendingChangesExt?.Workspace;

                    return teamExplorer.NavigateToPage(new Guid(ShelvesetComparerPage.PageId), ws);
                }
            }
            catch (Exception ex)
            {
                teamExplorer?.ShowNotification(ex.Message, NotificationType.Error);
            }

            return null;
        }

        public static ShelvesetComparerPage GetCurrentPageAsShelvesetComparerPage(this ITeamExplorer teamExplorer)
        {
            return teamExplorer?.CurrentPage?.GetExtensibilityService(typeof(ShelvesetComparerPage)) as ShelvesetComparerPage;
        }
    }
}
