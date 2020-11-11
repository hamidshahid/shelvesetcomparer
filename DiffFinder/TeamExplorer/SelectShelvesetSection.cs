// <copyright file="SelectShelvesetSection.cs" company="https://github.com/rajeevboobna/CompareShelvesets">Copyright https://github.com/rajeevboobna/CompareShelvesets. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>

//#define FakeShelvesetResult // activate to get fake shelvest results with delay for debugging

using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Controls;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DiffFinder
{
    /// <summary>
    /// The class creates the team explorer section for the Shelveset Comparer extension.
    /// </summary>
    [TeamExplorerSection("1555C86B-9D88-4AA6-9B85-99D97710BD74", ShelvesetComparerPage.PageId, 20)]
    public class SelectShelvesetSection : TeamExplorerBaseSection
    {
        /// <summary>
        /// Contains the shelveset list
        /// </summary>
        private ObservableCollection<ShelvesetViewModel> shelvesets;

        /// <summary>
        /// Initializes a new instance of the SelectShelvesetSection class.
        /// </summary>
        public SelectShelvesetSection()
        {
            this.Title = Resources.TeamExplorerLinkCaption;
            this.FirstUserAccountName = string.Empty;
            this.SecondUserAccountName = string.Empty;
            this.IsVisible = true;
            this.IsExpanded = true;
            this.IsBusy = false;
            this.shelvesets = new ObservableCollection<ShelvesetViewModel>();
            this.SectionContent = new SelectShelvesetTeamExplorerView(this);
        }

        /// <summary>
        /// Gets or sets the user account name for first shelveset.
        /// </summary>
        public string FirstUserAccountName { get; set; }

        /// <summary>
        /// Gets or sets the user account name for second shelveset.
        /// </summary>
        public string SecondUserAccountName { get; set; }

        /// <summary>
        /// Gets or sets the shelveset list
        /// </summary>
        public ObservableCollection<ShelvesetViewModel> Shelvesets
        {
            get
            {
                return this.shelvesets;
            }

            protected set
            {
                this.shelvesets = value;
                this.RaisePropertyChanged(nameof(Shelvesets));
            }
        }

        /// <summary>
        /// Gets Team Foundation Context of the Team Explorer window.
        /// </summary>
        public ITeamFoundationContext Context
        {
            get
            {
                return this.CurrentContext;
            }
        }

        /// <summary>
        /// Gets the view of the current Team Explorer section
        /// </summary>
        protected SelectShelvesetTeamExplorerView View
        {
            get 
            { 
                return this.SectionContent as SelectShelvesetTeamExplorerView; 
            }
        }

        /// <summary>
        /// Overridden method that initializes the team explorer section
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        public async override void Initialize(object sender, SectionInitializeEventArgs e)
        {
            base.Initialize(sender, e);
            var sectionContext = e.Context as ShelvesetsContext;
            if (sectionContext != null)
            {
                ShelvesetsContext context = sectionContext;
                this.Shelvesets = context.Shelvesets;
            }
            else
            {
                await this.RefreshAsync();
            }
        }

        /// <summary>
        /// Refresh override.
        /// </summary>
        public async override void Refresh()
        {
            base.Refresh();
            await this.RefreshAsync();
        }

        /// <summary>
        /// Save the current state of the section
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        public override void SaveContext(object sender, SectionSaveContextEventArgs e)
        {
            base.SaveContext(sender, e);
            if (e != null)
            {
                e.Context = new ShelvesetsContext
                {
                    Shelvesets = this.Shelvesets
                };
            }
        }

        /// <summary>
        /// Refresh the list of shelveset shelveset asynchronously.
        /// </summary>
        /// <returns>The Task doing the refresh. Needed for Async methods</returns>
        private async System.Threading.Tasks.Task RefreshShelvesetsAsync()
        {
            // Make the server call asynchronously to avoid blocking the UI
            var fetchShelvesetsTask = Task.Run(() =>
            {
#if FakeShelvesetResult
                return FetchFakedShelveset();
#else
                return FetchShevlesets(this.FirstUserAccountName, this.SecondUserAccountName, this.CurrentContext);
#endif
            });

            this.Shelvesets = await fetchShelvesetsTask;
        }

        /// <summary>
        /// Opens up the shelveset details page for the given shelveset
        /// </summary>
        /// <param name="shelveset">The shelveset to be displayed.</param>
        public void ViewShelvesetDetails(Shelveset shelveset)
        {
            TeamExplorer.NavigateToShelvesetDetails(shelveset);
        }

        /// <summary>
        /// the method is invoked when the context of the current team explorer window has changed.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        protected override async void ContextChanged(object sender, ContextChangedEventArgs e)
        {
            base.ContextChanged(sender, e);

            // If the team project collection or team project changed, refresh the data for this section
            if (e.TeamProjectCollectionChanged || e.TeamProjectChanged)
            {
                await this.RefreshAsync();
            }
        }

        /// <summary>
        /// Retrieves the shelveset list for the current user 
        /// </summary>
        /// <param name="userName">The user name </param>
        /// <param name="secondUsername">The second user name </param>
        /// <param name="context">The Team foundation server context</param>
        /// <param name="shelveSets">The shelveset list to be returned</param>
        private static ObservableCollection<ShelvesetViewModel> FetchShevlesets(string userName, string secondUsername, ITeamFoundationContext context)
        {
            var shelveSets = new ObservableCollection<ShelvesetViewModel>();
            if (context != null && context.HasCollection && context.HasTeamProject)
            {
                var vcs = context.TeamProjectCollection.GetService<VersionControlServer>();
                if (vcs != null)
                {
                    string user = string.IsNullOrWhiteSpace(userName) ? vcs.AuthorizedUser : userName;
                    foreach (var shelveSet in vcs.QueryShelvesets(null, user).OrderByDescending(s => s.CreationDate))
                    {
                        shelveSets.Add(new ShelvesetViewModel(shelveSet));
                    }

                    if (!string.IsNullOrWhiteSpace(secondUsername) && secondUsername != userName)
                    {
                        user = string.IsNullOrWhiteSpace(secondUsername) ? vcs.AuthorizedUser : secondUsername;
                        foreach (var shelveSet in vcs.QueryShelvesets(null, user).OrderByDescending(s => s.CreationDate))
                        {
                            shelveSets.Add(new ShelvesetViewModel(shelveSet));
                        }
                    }
                }
            }

            return shelveSets;
        }



        /// <summary>
        /// Retrieves the shelveset for pending change for the current user 
        /// </summary>
        /// <param name="context">The Team foundation server context</param>
        internal Shelveset FetchPendingChangeShelveset(ITeamFoundationContext context, Workspace ws = null)
        {
            if (context != null && context.HasCollection && context.HasTeamProject)
            {
                var vcs = context.TeamProjectCollection.GetService<VersionControlServer>();
                if (vcs != null)
                {
                    var machineName = Environment.MachineName;
                    var currentUserName = Environment.UserName;

                    var workspace = ws ?? vcs.GetWorkspace(machineName, currentUserName);

                    var changes = workspace.GetPendingChanges();//we want to shelve all pending changes in the workspace

                    if (changes.Length != 0)
                    {

                        var pendChange = new Shelveset(vcs, "Pending Changes", workspace.OwnerName);
                        workspace.Shelve(pendChange, changes, ShelvingOptions.Replace);//you can specify to replace existing shelveset, or to remove pending changes from the local workspace with ShelvingOptions
                        pendChange.CreationDate = DateTime.Now;

                        return pendChange;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Refresh the list of shelveset and comparison shelveset asynchronously.
        /// </summary>
        /// <returns>The Task doing the refresh. Needed for Async methods</returns>
        public async System.Threading.Tasks.Task RefreshAsync()
        {
            try
            {
                this.IsBusy = true;

                await this.RefreshShelvesetsAsync();
            }
            catch (Exception ex)
            {
                this.ShowNotification(ex.Message, NotificationType.Error);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

#if FakeShelvesetResult
        private ObservableCollection<ShelvesetViewModel> FetchFakedShelveset()
        {
            var result = new ObservableCollection<ShelvesetViewModel>();
            for(var idx=0; idx < 1111; idx++)
            {
                // fake shelveset with 2 owners for sorting test
                result.Add(new ShelvesetViewModel("Shelveset" + idx, 
                    new DateTime(2020, idx % 12 + 1, idx % 28 + 1, idx % 24, idx % 60, idx % 60), 
                    "Owner" + (idx % 2)));
            }

            Task.Delay(1500); // for some time consuming operation, to show UI response

            return result;
        }
#endif
    }
}
