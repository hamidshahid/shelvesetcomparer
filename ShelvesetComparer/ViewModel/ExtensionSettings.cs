// <copyright file="ExtensionSettings.cs" company="http://shelvesetcomparer.codeplex.com">Copyright http://shelvesetcomparer.codeplex.com. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>
namespace WiredTechSolutions.ShelvesetComparer
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.VisualStudio.Settings;
    using Microsoft.VisualStudio.Shell.Settings;

    public class ExtensionSettings
    {
        private static ExtensionSettings singleton;

        private WritableSettingsStore writableSettingsStore;
        private SettingsStore readableSettingStore;

        /// <summary>
        /// Prevents a default instance of the <see cref=" ExtensionSettings"/> class from being created.
        /// </summary>
        private ExtensionSettings()
        {
        }

        /// <summary>
        /// Gets the Instance object
        /// </summary>
        public static ExtensionSettings Instance
        {
            get
            {
                return singleton;
            }            
        }

        /// <summary>
        /// Gets or sets the a property that ShowAsButton 
        /// </summary>
        public bool ShowAsButton
        {
            get
            {
                return this.readableSettingStore.GetBoolean("ShelveSetComparer", "ShowAsButton");
            }

            set
            {
                this.writableSettingsStore.SetBoolean("ShelveSetComparer", "ShowAsButton", value); 
            }
        }

        /// <summary>
        /// Gets or sets the setting indicating whether second user is shown in team explorer or not.
        /// </summary>
        public bool TwoUsersView
        {
            get
            {
                return this.readableSettingStore.GetBoolean("ShelveSetComparer", "TwoUsersView");
            }

            set
            {
                this.writableSettingsStore.SetBoolean("ShelveSetComparer", "TwoUsersView", value);
            }
        }

        /// <summary>
        /// Creates a new instances of the ExtensionSetting class
        /// </summary>
        /// <param name="package">The Visual studio extension package</param>
        public static void CreateInstance(ShelvesetComparerPackage package)
        {
            if (singleton == null)
            {
                singleton = new ExtensionSettings();
                singleton.Initialize(package);
            }
        }

        /// <summary>
        /// Initializes properties in the package
        /// </summary>
        /// <param name="package">The package</param>
        private void Initialize(ShelvesetComparerPackage package)
        {
            SettingsManager settingsManager = new ShellSettingsManager(package);
            this.writableSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
            if (!this.writableSettingsStore.CollectionExists("ShelveSetComparer"))
            {
                this.writableSettingsStore.CreateCollection("ShelveSetComparer");
                this.ShowAsButton = true;
                this.TwoUsersView = true;
            }

            this.readableSettingStore = settingsManager.GetReadOnlySettingsStore(SettingsScope.UserSettings);
        }
    }
}
