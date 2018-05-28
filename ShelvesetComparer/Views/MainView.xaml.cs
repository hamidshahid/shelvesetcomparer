// <copyright file="MainView.xaml.cs" company="https://github.com/rajeevboobna/shelvesetcomparer">Copyright https://github.com/rajeevboobna/shelvesetcomparer. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>
namespace WiredTechSolutions.ShelvesetComparer
{
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Microsoft.Win32;

    /// <summary>
    /// The Main View of the shelveset comparison window.
    /// </summary>
    public partial class MainView : UserControl
    {
        /// <summary>
        /// The dependency property containing for the Shelveset Comparison View Model
        /// </summary>
        private static readonly DependencyProperty ComparisonModelProperty = DependencyProperty.Register("ComparisonModel", typeof(ShelvesetComparerViewModel), typeof(MainView));

        /// <summary>
        /// Keeps the visual studio version
        /// </summary>
        private static string visualStudioVersion = string.Empty;

        /// <summary>
        /// Initializes a new instance of the MainView class.
        /// </summary>
        public MainView()
        {
            this.InitializeComponent();
            this.DataContext = this;
            this.ComparisonModel = ShelvesetComparerViewModel.Instance;
        }

        /// <summary>
        /// Gets the Visual Studio Version the extension is currently running in
        /// </summary>
        public static string VisualStudioVersion
        {
            get
            {
                if (string.IsNullOrWhiteSpace(visualStudioVersion))
                {
                    var dte = Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;
                    visualStudioVersion = dte.SourceControl.Parent.Version;
                }

                return visualStudioVersion;
            }
        }

        /// <summary>
        /// Gets or sets the ComparisonModel
        /// </summary>
        public ShelvesetComparerViewModel ComparisonModel
        {
            get
            {
                return this.GetValue(ComparisonModelProperty) as ShelvesetComparerViewModel;
            }

            set
            {
                this.SetValue(ComparisonModelProperty, value);
            }
        }

        /// <summary>
        /// The method opens up a window comparing two files
        /// </summary>
        /// <param name="compareFiles">The compare files view model</param>
        private static void CompareFiles(FileComparisonViewModel compareFiles)
        {   
            string firstFileName = Path.GetTempFileName();
            string secondFileName = Path.GetTempFileName();
            if (compareFiles.FirstFile != null)
            {
                compareFiles.FirstFile.DownloadShelvedFile(firstFileName);
            }

            if (compareFiles.SecondFile != null)
            {
                compareFiles.SecondFile.DownloadShelvedFile(secondFileName);
            }

            string diffToolCommandArguments = string.Empty;
            string diffToolCommand = string.Empty;

            GetExternalTool(Path.GetExtension(compareFiles.FirstFile.FileName), out diffToolCommand, out diffToolCommandArguments);

            if (string.IsNullOrWhiteSpace(diffToolCommand))
            {
                var currentProcess = Process.GetCurrentProcess();
                currentProcess.StartInfo.FileName = currentProcess.Modules[0].FileName;
                currentProcess.StartInfo.Arguments = string.Format(CultureInfo.CurrentCulture, @"/diff ""{0}"" ""{1}""", firstFileName, secondFileName);
                currentProcess.Start();
            }
            else
            {
                // So there is a tool configured. Let's use it
                diffToolCommandArguments = diffToolCommandArguments.Replace("%1", firstFileName).Replace("%2", secondFileName);
                var startInfo = new ProcessStartInfo()
                {
                    Arguments = diffToolCommandArguments,
                    FileName = diffToolCommand
                };

                Process.Start(startInfo);
            }
        }

        /// <summary>
        /// Returns the file path of the external tool configured for comparison for the file with given extension.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <param name="diffToolCommand">If a comparison tool is found this will contain the path of the tool</param>
        /// <param name="diffToolCommandArguments">If a comparison tool is found this will contain command line arguments for the tool</param>
        private static void GetExternalTool(string extension, out string diffToolCommand, out string diffToolCommandArguments)
        {
            diffToolCommand = string.Empty;
            diffToolCommandArguments = string.Empty;

            // read registry key for the extension
            diffToolCommand = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\VisualStudio\" + VisualStudioVersion + @"\TeamFoundation\SourceControl\DiffTools\" + extension + @"\Compare", "Command", null);
            diffToolCommandArguments = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\VisualStudio\" + VisualStudioVersion + @"\TeamFoundation\SourceControl\DiffTools\" + extension + @"\Compare", "Arguments", null);
            if (diffToolCommand != null && diffToolCommandArguments != null)
            {
                return;
            }

            // read registry key for the wildcard
            diffToolCommand = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\VisualStudio\" + VisualStudioVersion + @"\TeamFoundation\SourceControl\DiffTools\.*\Compare", "Command", null);
            diffToolCommandArguments = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\VisualStudio\" + VisualStudioVersion + @"\TeamFoundation\SourceControl\DiffTools\.*\Compare", "Arguments", null);
        }

        /// <summary>
        /// Event Handler for Mouse Double click event 
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">Event Argument</param>
        private void ComparisonFiles_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e != null && e.ChangedButton == MouseButton.Left)
            {
                var compareFiles = this.ComparisonFiles.SelectedItem as FileComparisonViewModel;

                if (compareFiles != null)
                {
                    CompareFiles(compareFiles);
                }            
            }
        }

        /// <summary>
        /// Event Handler for Key up event
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">Event Argument</param>
        private void ComparisonFiles_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e != null && e.Key == Key.Enter)
            {
                var compareFiles = this.ComparisonFiles.SelectedItem as FileComparisonViewModel;
                if (compareFiles != null)
                {
                    CompareFiles(compareFiles);
                }            
            }
        }

        /// <summary>
        /// Key up event for the search dialog
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">Event Argument</param>
        private void SearchFilesTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ShelvesetComparerViewModel.Instance.Filter = this.SearchFilesTextBox.Text;
        }
    }
}
