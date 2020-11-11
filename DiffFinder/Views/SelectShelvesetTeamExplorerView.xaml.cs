// <copyright file="SelectShelvesetTeamExplorerView.xaml.cs" company="https://github.com/rajeevboobna/CompareShelvesets">Copyright https://github.com/rajeevboobna/CompareShelvesets. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>

using EnvDTE;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Controls;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DiffFinder
{
    /// <summary>
    /// Team Explorer view allow to select shelveset for comparison.
    /// </summary>
    public partial class SelectShelvesetTeamExplorerView
    {
        /// <summary>
        /// Initializes a new instance of the SelectShelvesetTeamExplorerView class
        /// </summary>
        /// <param name="parentSection">Reference to the Team Explorer section where the view is initialized.</param>
        public SelectShelvesetTeamExplorerView(SelectShelvesetSection parentSection)
        {
            this.InitializeComponent();
            this.ParentSection = parentSection;
            this.DataContext = this;
        }

        /// <summary>
        /// Gets the Team Explorer section in to which the view is created.
        /// </summary>
        public SelectShelvesetSection ParentSection
        {
            get;
            private set;
        }

        /// <summary>
        /// Event Handler for Selection change in the Shelvesets list
        /// </summary>
        /// <param name="sender">The ListShelvesets ListView control</param>
        /// <param name="e">The event arguments</param>
        public void ListShelvesetsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListShelvesets.SelectedItems.Count > 2)
            {
                this.ListShelvesets.SelectedItems.RemoveAt(0);
            }
        }

        /// <summary>
        /// Event Handler for keying user name for the first shelveset.
        /// </summary>
        /// <param name="sender">The first shelveset user text box</param>
        /// <param name="e">The event arguments</param>
        private async void FirstShelvesetUserTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.ClearError();
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(this.FirstShelvesetUserTextBox.Text) && this.UserIdentityExists(this.FirstShelvesetUserTextBox.Text) == false)
                {
                    this.ShowError("User Account Name or display name could not be found");
                    return;
                }

                await this.ParentSection.RefreshAsync();
            }
        }

        /// <summary>
        /// Event Handler for keying user name for the second shelveset.
        /// </summary>
        /// <param name="sender">The second shelveset user text box</param>
        /// <param name="e">The event arguments</param>
        private async void SecondShelvesetUserTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.ClearError();
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(this.SecondShelvesetUserTextBox.Text) && this.UserIdentityExists(this.SecondShelvesetUserTextBox.Text) == false)
                {
                    this.ShowError("User Account Name or display name could not be found");
                    return;
                }

                await this.ParentSection.RefreshAsync();
            }
        }

        /// <summary>
        /// Returns whether the user identity of the given account name or display name exists or not
        /// </summary>
        /// <param name="userDisplayNameOrAccount">The given account name or display name</param>
        /// <returns>True if the given user account or display name exists. False otherwise</returns>
        private bool UserIdentityExists(string userDisplayNameOrAccount)
        {
            ITeamFoundationContext context = this.ParentSection.Context;
            IIdentityManagementService ims = context.TeamProjectCollection.GetService<IIdentityManagementService>();

            // First try search by AccountName 
            TeamFoundationIdentity userIdentity = ims.ReadIdentity(IdentitySearchFactor.AccountName, userDisplayNameOrAccount, MembershipQuery.None, ReadIdentityOptions.ExtendedProperties);
            if (userIdentity == null)
            {
                // Next we try search by DisplayName
                userIdentity = ims.ReadIdentity(IdentitySearchFactor.DisplayName, userDisplayNameOrAccount, MembershipQuery.None, ReadIdentityOptions.ExtendedProperties);
                if (userIdentity == null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Displays the error panel
        /// </summary>
        /// <param name="error">The error text</param>
        private void ShowError(string error)
        {
            this.ErrorText.Text = error;
            this.ErrorPanel.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Clears the error panel
        /// </summary>
        private void ClearError()
        {
            this.ErrorText.Text = string.Empty;
            this.ErrorPanel.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Event Handler for the list button.
        /// </summary>
        /// <param name="sender">The list button</param>
        /// <param name="e">Event parameters</param>
        private async void ListButton_Click(object sender, RoutedEventArgs e)
        {
            this.ClearError();
            await this.ParentSection.RefreshAsync();
        }

        /// <summary>
        /// Event Handler for the compare button.
        /// </summary>
        /// <param name="sender">The compare button</param>
        /// <param name="e">Event parameters</param>
        private void CompareButtons_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ClearError();
                if (this.ListShelvesets.SelectedItems != null && this.ListShelvesets.SelectedItems.Count != 2)
                {
                    this.ShowError(DiffFinder.Resources.ShelvesetsNotSelectedErrorMessage);
                    return;
                }
                
                var firstSheleveset = CastAsShelveset(this.ListShelvesets.SelectedItems[0]);
                var secondSheleveset = CastAsShelveset(this.ListShelvesets.SelectedItems[1]);
                ShelvesetComparerViewModel.Instance.Initialize(firstSheleveset, secondSheleveset);

                ShelvesetComparer.Instance.ShowResultWindow();
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Event Handler for the compare button.
        /// </summary>
        /// <param name="sender">The compare button</param>
        /// <param name="e">Event parameters</param>
        private void CompareWithPendChangeButtons_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ClearError();
                if (this.ListShelvesets.SelectedItems != null && this.ListShelvesets.SelectedItems.Count != 1)
                {
                    this.ShowError(DiffFinder.Resources.ShelvesetNotSelectedErrorMessage);
                    return;
                }

                // get workspace from page
                var parent = this.ParentSection;
                var page = parent.TeamExplorer.GetCurrentPageAsShelvesetComparerPage();
                var pendChangeShelveset = parent.FetchPendingChangeShelveset(this.ParentSection.Context, page?.CurrentWorkspace);
                
                var firstSheleveset = this.ListShelvesets.SelectedItems[0] as Shelveset;
                var secondSheleveset = pendChangeShelveset;
                ShelvesetComparerViewModel.Instance.Initialize(firstSheleveset, secondSheleveset);

                ShelvesetComparer.Instance.ShowResultWindow();
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Event Handler for mouse double click on the shelvesets list.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event arguments</param>
        private void ListShelvesets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e != null && e.ChangedButton == MouseButton.Left && this.ListShelvesets.SelectedItems.Count == 1)
            {
                this.ViewShelvesetDetails(this.ListShelvesets);
            }
        }
       
        /// <summary>
        /// Event Handler for key up event on the shelvesets list.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event arguments</param>
        private void ListShelvesets_KeyUp(object sender, KeyEventArgs e)
        {
            if (e != null && e.Key == Key.Enter && this.ListShelvesets.SelectedItems.Count == 1)
            {
                this.ViewShelvesetDetails(this.ListShelvesets);
            }
        }
        
        /// <summary>
        /// Opens up the shelveset details team explorer page for given shelveset
        /// </summary>
        /// <param name="listView">The listView control to read selected shelveset from</param>
        private void ViewShelvesetDetails(ListView listView)
        {
            if (listView.SelectedItems.Count == 1)
            {
                var shelveset = CastAsShelveset(listView.SelectedItems[0]);
                if (shelveset != null)
                {
                    this.ParentSection.ViewShelvesetDetails(shelveset);
                }
            }
        }

        private static Shelveset CastAsShelveset(object listViewSelectedItem)
        {
            return (listViewSelectedItem as ShelvesetViewModel)?.Shelveset;
        }
    }
}
