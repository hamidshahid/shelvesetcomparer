// <copyright file="FileComparisonViewModel.cs" company="https://github.com/rajeevboobna/shelvesetcomparer">Copyright https://github.com/rajeevboobna/shelvesetcomparer. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>
namespace ShelvesetComparer
{
    using System.Globalization;
    using Microsoft.TeamFoundation.VersionControl.Client;

    /// <summary>
    /// The view model used for each file in the comparison grid.
    /// </summary>
    public class FileComparisonViewModel
    {
        /// <summary>
        /// Gets or sets the first pending change file
        /// </summary>
        public PendingChange FirstFile { get; set; }

        /// <summary>
        /// Gets or sets the second pending change file
        /// </summary>
        public PendingChange SecondFile { get; set; }

        /// <summary>
        /// Gets the display name of the first file
        /// </summary>
        public string FirstFileDisplayName
        {
            get
            {
                return GetFullFilePath(this.FirstFile);
            }
        }

        /// <summary>
        /// Gets the display name of the second file
        /// </summary>
        public string SecondFileDisplayName
        {
            get
            {
                return GetFullFilePath(this.SecondFile);
            }
        }

        /// <summary>
        /// Gets or sets the Color of text.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Returns the full file path of the pending change file.
        /// </summary>
        /// <param name="pendingChange">The pending change file</param>
        /// <returns>The full file path of the given pending change</returns>
        private static string GetFullFilePath(PendingChange pendingChange)
        {
            return (pendingChange != null) ? string.Format(CultureInfo.CurrentCulture, @"{0}/{1}", pendingChange.LocalOrServerFolder, pendingChange.FileName) : string.Empty;
        }
    }
}
