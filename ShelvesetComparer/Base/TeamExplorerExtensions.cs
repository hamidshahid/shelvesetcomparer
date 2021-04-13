// <copyright file="SelectShelvesetSection.cs" company="https://github.com/rajeevboobna/CompareShelvesets">
// Copyright https://github.com/rajeevboobna/CompareShelvesets. All Rights Reserved. 
// This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html). 
// This is sample code only, do not use in production environments.
// </copyright>

using Microsoft.TeamFoundation.Controls;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.VersionControl.Controls.Extensibility;
using System;
using System.Linq;

namespace WiredTechSolutions.ShelvesetComparer
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
                    // workspace from IPendingChangesExt for our context
                    teamExplorer.NavigateToPage(new Guid(TeamExplorerPageIds.PendingChanges), null);
                    var pendingChangesExt = teamExplorer.CurrentPageOrUndockedGetExtensibilityService<IPendingChangesExt>();
                    var ws = pendingChangesExt?.Workspace;

                    ShelvesetComparer.Instance.TraceOutput($"Open TeamExplorer ShelvesetComparer page with WS: {ws?.DisplayName ?? "<null>"}");
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
            return teamExplorer.CurrentPageOrUndockedGetExtensibilityService<ShelvesetComparerPage>();
        }

        public static TService CurrentPageOrUndockedGetExtensibilityService<TService>(this ITeamExplorer teamExplorer) where TService : class
        {
            return teamExplorer.CurrentPageOrUndockedGetExtensibilityService<TService, TService>();
        }

        public static TOut CurrentPageOrUndockedGetExtensibilityService<TService, TOut>(this ITeamExplorer teamExplorer) where TOut : class
        {
            if (teamExplorer == null)
            {
                return null;
            }

            var result = teamExplorer?.CurrentPage.GetExtensibilityService<TService, TOut>();
            if (result == null)
            {
                // try undocked pages
                if (teamExplorer is ITeamExplorerUndockedPageManager undockedManager)
                {
                    var undocked = undockedManager.UndockedPages;
                    if (undocked.Any())
                    {
                        result = undocked.Select(p => p.GetExtensibilityService<TService, TOut>())
                                                .FirstOrDefault(bp => bp != null);
                    }
                }
            }

            return result;
        }

        public static TService GetExtensibilityService<TService>(this ITeamExplorerPage page) where TService : class
        {
            return GetExtensibilityService<TService, TService>(page);
        }

        public static TOut GetExtensibilityService<TService, TOut>(this ITeamExplorerPage page) where TOut : class
        {
            if (page == null)
            {
                return null;
            }

            return page.GetExtensibilityService(typeof(TService)) as TOut;
        }

        public static TService GetService<TService>(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<TService, TService>();
        }

        public static TOut GetService<TService, TOut>(this IServiceProvider serviceProvider)
        {
            if (serviceProvider != null)
            {
                return (TOut) serviceProvider.GetService(typeof(TService));
            }

            return default(TOut);
        }
    }
}
