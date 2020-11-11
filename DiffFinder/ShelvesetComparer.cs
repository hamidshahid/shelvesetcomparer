// <copyright file="SelectShelvesetTeamExplorerView.xaml.cs" company="https://github.com/rajeevboobna/CompareShelvesets">Copyright https://github.com/rajeevboobna/CompareShelvesets. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>

using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;

namespace DiffFinder
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ShelvesetComparer
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int ShelvesetComparerResuldId = 0x0100;
        public const int ShelvesetComparerTeamExplorerViewId = 0x0200;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("b8e98565-7b6d-4d64-b51d-97fe5e56c5ec");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShelvesetComparer"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private ShelvesetComparer(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService<IMenuCommandService, OleMenuCommandService>();
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, ShelvesetComparerResuldId);
                var menuItem = new MenuCommand(this.ShelvesetComparerResuldIdMenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);

                menuCommandID = new CommandID(CommandSet, ShelvesetComparerTeamExplorerViewId);
                menuItem = new MenuCommand(this.ShelvesetComparerTeamExplorerViewIdMenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ShelvesetComparer Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new ShelvesetComparer(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void ShelvesetComparerResuldIdMenuItemCallback(object sender, EventArgs e)
        {
            this.ShowResultWindow();
        }

        /// <summary>
        /// This function is called when the user clicks the menu item that shows the tool window.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event arguments</param>
        public void ShowResultWindow()
        {
            ToolWindowPane window = package.FindToolWindow(typeof(ShelvesetComparerToolWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            windowFrame.Show();
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void ShelvesetComparerTeamExplorerViewIdMenuItemCallback(object sender, EventArgs e)
        {
            NavigateToShelvestComparerPage();
        }

        public void NavigateToShelvestComparerPage()
        {
            var teamExplorer = ServiceProvider.GetService<ITeamExplorer>();
            teamExplorer.NavigateToShelvesetComparer();
        }
    }
}
