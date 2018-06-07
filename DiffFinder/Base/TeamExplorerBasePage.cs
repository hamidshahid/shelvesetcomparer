// <copyright file="TeamExplorerBasePage.cs" company="https://github.com/rajeevboobna/CompareShelvesets">Copyright https://github.com/rajeevboobna/CompareShelvesets. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>

using Microsoft.TeamFoundation.Controls;
using System;

namespace DiffFinder
{
    /// <summary>
    /// Team Explorer page base class.
    /// </summary>
    public class TeamExplorerBasePage : TeamExplorerBase, ITeamExplorerPage
    {
        private string title;
        private bool isBusy;
        private object pageContent;

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value; 
                this.RaisePropertyChanged("Title");
            }
        }

        public object PageContent
        {
            get
            {
                return this.pageContent;
            }

            set
            {
                this.pageContent = value; 
                this.RaisePropertyChanged("PageContent");
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.isBusy = value; 
                this.RaisePropertyChanged("IsBusy");
            }
        }

        public virtual void Initialize(object sender, PageInitializeEventArgs e)
        {
            if (e != null)
            {
                this.ServiceProvider = e.ServiceProvider;
            }
        }

        public virtual void Loaded(object sender, PageLoadedEventArgs e)
        {
        }

        public virtual void SaveContext(object sender, PageSaveContextEventArgs e)
        {
        }

        public virtual void Refresh()
        {
        }

        public virtual void Cancel()
        {
        }

        public virtual object GetExtensibilityService(Type serviceType)
        {
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
