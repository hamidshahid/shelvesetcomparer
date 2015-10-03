// <copyright file="ShelvesetComparerPackage.cs" company="http://shelvesetcomparer.codeplex.com">Copyright http://shelvesetcomparer.codeplex.com. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>
namespace WiredTechSolutions.ShelvesetComparer
{
    using System;
    using System.ComponentModel.Design;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [ProvideAutoLoad("ADFC4E64-0397-11D1-9F4E-00A0C911004F")]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(ShelvesetComparerToolWindow))]
    [Guid("50f4bcb5-060b-4090-93c5-30d3260ddc8c")]
    public sealed class ShelvesetComparerPackage : Package
    {
        /// <summary>
        /// The unique id of the Tools menu item for extension
        /// </summary>
        private const uint CommandIdShelvesetComparerMenu = 0x100;

        /// <summary>
        /// The unique id of the Tool window for the extension
        /// </summary>
        private const uint CommandIdShelvesetComparerToolWindow = 0x101;

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            ExtensionSettings.CreateInstance(this);
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                CommandID menuCommandID = new CommandID(new Guid("def78c97-03e0-4caf-9c9c-4b70455da864"), (int)CommandIdShelvesetComparerMenu);
                MenuCommand menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                mcs.AddCommand(menuItem);

                CommandID toolwndCommandID = new CommandID(new Guid("def78c97-03e0-4caf-9c9c-4b70455da864"), (int)CommandIdShelvesetComparerToolWindow);
                MenuCommand menuToolWin = new MenuCommand(this.ShowToolWindow, toolwndCommandID);
                mcs.AddCommand(menuToolWin);
            }
        }

        /// <summary>
        /// This function is called when the user clicks the menu item that shows the tool window.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event arguments</param>
        private void ShowToolWindow(object sender, EventArgs e)
        {
            ToolWindowPane window = this.FindToolWindow(typeof(ShelvesetComparerToolWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event arguments</param>/// 
        private void MenuItemCallback(object sender, EventArgs e)
        {
            ToolWindowPane window = this.FindToolWindow(typeof(ShelvesetComparerToolWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Resources.NoWindowFound);
            }

            var wnd = window as ShelvesetComparerToolWindow;
            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
    }
}
