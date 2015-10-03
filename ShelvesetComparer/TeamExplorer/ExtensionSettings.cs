// <copyright file="ExtensionSettings.cs" company="http://shelvesetcomparer.codeplex.com">Copyright http://shelvesetcomparer.codeplex.com. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>
namespace WiredTechSolutions.ShelvesetComparer
{
    using Microsoft.VisualStudio.Settings;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Settings;

    /// <summary>
    /// The class stores settings for extension which is persisted with Visual Studio settings.
    /// </summary>
    public class ExtensionSettings
    {
        /// <summary>
        /// The settings that determines whether the extension is displayed as Team View button or not.
        /// </summary>
        private bool displayTeamViewerButton;

        /// <summary>
        /// The setting that determins whether to allow shelvesets from two users or not.
        /// </summary>
        private bool twoUsersView;

        /// <summary>
        /// The name of settings collection.
        /// </summary>
        private const string COLLECTION_NAME = "ShelveSetComparer";

        /// <summary>
        /// Member variable to store the package.
        /// </summary>
        private Package extensionPackage;

        /// <summary>
        /// Initializes a new instance of the ExtensionSettings class
        /// </summary>
        /// <param name="extensionPackage">The extension package</param>
        public ExtensionSettings(Package extensionPackage)
        {
            this.extensionPackage = extensionPackage;
            var settingsManager = new ShellSettingsManager(this.extensionPackage);
            var configurationSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
            if (configurationSettingsStore.CollectionExists(COLLECTION_NAME))
            {
                this.displayTeamViewerButton = configurationSettingsStore.GetBoolean(COLLECTION_NAME, "ShowAsButton");
                this.twoUsersView = configurationSettingsStore.GetBoolean(COLLECTION_NAME, "TwoUsersView");
            }
            else
            {
                this.displayTeamViewerButton = false;
                this.twoUsersView = false;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the extension as a Team Explorer button or not.
        /// </summary>
        public bool DisplayTeamViewerButton 
        {
            get
            {
                return this.displayTeamViewerButton;
            }

            set
            {
                this.displayTeamViewerButton = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to compare Shelvesets for Two users or not.
        /// </summary>
        public bool TwoUsersView
        {
            get
            {
                return this.twoUsersView;
            }

            set
            {
                var settingsManager = new ShellSettingsManager(this.extensionPackage);
                var configurationSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
                if (!configurationSettingsStore.CollectionExists(COLLECTION_NAME))
                {
                    configurationSettingsStore.CreateCollection(COLLECTION_NAME);
                }

                configurationSettingsStore.SetBoolean(COLLECTION_NAME, "TwoUsersView", true);
            }
        }
    }
}
