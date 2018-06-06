// <copyright file="GridViewSorter.cs" company="https://github.com/rajeevboobna/CompareShelvesets">Copyright https://github.com/rajeevboobna/CompareShelvesets. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>
namespace DiffFinder
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Custom property for sorting GridView headers
    /// </summary>
    public class GridViewSorter
    {
        /// <summary>
        /// DependencyProperty for ShowSortGlyph. 
        /// </summary>
        private static readonly DependencyProperty ShowSortGlyphProperty = DependencyProperty.RegisterAttached("ShowSortGlyph", typeof(bool), typeof(GridViewSorter), new UIPropertyMetadata(true));

        /// <summary>
        /// DependencyProperty for SortGlyphAscending.
        /// </summary>
        private static readonly DependencyProperty SortGlyphAscendingProperty = DependencyProperty.RegisterAttached("SortGlyphAscending", typeof(ImageSource), typeof(GridViewSorter), new UIPropertyMetadata(null));

        /// <summary>
        /// DependencyProperty for SortGlyphDescending.
        /// </summary>
        private static readonly DependencyProperty SortGlyphDescendingProperty = DependencyProperty.RegisterAttached("SortGlyphDescending", typeof(ImageSource), typeof(GridViewSorter), new UIPropertyMetadata(null));

        /// <summary>
        /// DependencyProperty for SortedColumn.
        /// </summary>
        private static readonly DependencyProperty SortedColumnHeaderProperty = DependencyProperty.RegisterAttached("SortedColumnHeader", typeof(GridViewColumnHeader), typeof(GridViewSorter), new UIPropertyMetadata(null));

        /// <summary>
        /// DependencyProperty for PropertyName.
        /// </summary>
        private static readonly DependencyProperty PropertyNameProperty = DependencyProperty.RegisterAttached("PropertyName", typeof(string), typeof(GridViewSorter), new UIPropertyMetadata(null));

        /// <summary>
        /// DependencyProperty for AutoSort.
        /// </summary>
        private static readonly DependencyProperty AutoSortProperty =
            DependencyProperty.RegisterAttached(
                "AutoSort",
                typeof(bool),
                typeof(GridViewSorter),
                new UIPropertyMetadata(
                    false,
                    (o, e) =>
                    {
                        ListView listView = o as ListView;
                        if (listView != null)
                        {
                            if (GetCommand(listView) == null) // Don't change click handler if a command is set
                            {
                                bool oldValue = (bool)e.OldValue;
                                bool newValue = (bool)e.NewValue;
                                if (oldValue && !newValue)
                                {
                                    listView.RemoveHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }

                                if (!oldValue && newValue)
                                {
                                    listView.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }
                            }
                        }
                    }));

        /// <summary>
        /// DependencyProperty for Command.
        /// </summary>
        private static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(GridViewSorter),
                new UIPropertyMetadata(
                    null,
                    (o, e) =>
                    {
                        ItemsControl listView = o as ItemsControl;
                        if (listView != null)
                        {
                            if (!GetAutoSort(listView)) // Don't change click handler if AutoSort enabled
                            {
                                if (e.OldValue != null && e.NewValue == null)
                                {
                                    listView.RemoveHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }

                                if (e.OldValue == null && e.NewValue != null)
                                {
                                    listView.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }
                            }
                        }
                    }));

        /// <summary>
        /// Gets Command property
        /// </summary>
        /// <param name="obj">the dependency object</param>
        /// <returns>The command property</returns>
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        /// <summary>
        /// Sets Command property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">The command object</param>
        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets AutoStart property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <returns>AutoStart property</returns>
        public static bool GetAutoSort(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoSortProperty);
        }

        /// <summary>
        /// Sets the AutoStart property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">AutoStart property</param>
        public static void SetAutoSort(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoSortProperty, value);
        }

        /// <summary>
        /// Gets the Name property
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <returns>The Name property</returns>
        public static string GetPropertyName(DependencyObject obj)
        {
            return (string)obj.GetValue(PropertyNameProperty);
        }

        /// <summary>
        /// Sets the Name property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">The Name property</param>
        public static void SetPropertyName(DependencyObject obj, string value)
        {
            obj.SetValue(PropertyNameProperty, value);
        }

        /// <summary>
        /// Gets the ShowSortGlyph property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <returns>The ShowSortGlyph property</returns>
        public static bool GetShowSortGlyph(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowSortGlyphProperty);
        }

        /// <summary>
        /// Sets the ShowSortGlyph property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">The ShowSortGlyph property</param>
        public static void SetShowSortGlyph(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowSortGlyphProperty, value);
        }

        /// <summary>
        /// Gets the SortGlyphAscending property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <returns>The SortGlyphAscending property</returns>
        public static ImageSource GetSortGlyphAscending(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(SortGlyphAscendingProperty);
        }

        /// <summary>
        /// Sets the SortGlyphAscending property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">The SortGlyphAscending property</param>
        public static void SetSortGlyphAscending(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(SortGlyphAscendingProperty, value);
        }

        /// <summary>
        /// Gets the GetSortGlyphDescending property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <returns>The GetSortGlyphDescending property</returns>
        public static ImageSource GetSortGlyphDescending(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(SortGlyphDescendingProperty);
        }

        /// <summary>
        /// Sets the SetSortGlyphDescending property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">The SetSortGlyphDescending property</param>
        public static void SetSortGlyphDescending(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(SortGlyphDescendingProperty, value);
        }

        /// <summary>
        /// Gets the GetSortedColumnHeader property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <returns>The GetSortedColumnHeader property</returns>
        public static GridViewColumnHeader GetSortedColumnHeader(DependencyObject obj)
        {
            return (GridViewColumnHeader)obj.GetValue(SortedColumnHeaderProperty);
        }

        /// <summary>
        /// Sets the SetSortedColumnHeader property
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">The SetSortedColumnHeader property</param>
        public static void SetSortedColumnHeader(DependencyObject obj, GridViewColumnHeader value)
        {
            obj.SetValue(SortedColumnHeaderProperty, value);
        }

        /// <summary>
        /// The event handler for the click on the attached ColumnHeader.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event argument</param>
        private static void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked != null && headerClicked.Column != null)
            {
                string propertyName = GetPropertyName(headerClicked.Column);
                if (!string.IsNullOrEmpty(propertyName))
                {
                    ListView listView = GetAncestor<ListView>(headerClicked);
                    if (listView != null)
                    {
                        ICommand command = GetCommand(listView);
                        if (command != null)
                        {
                            if (command.CanExecute(propertyName))
                            {
                                command.Execute(propertyName);
                            }
                        }
                        else if (GetAutoSort(listView))
                        {
                            ApplySort(listView.Items, propertyName, listView, headerClicked);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generic method returning the ancestor of the given dependency object. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="reference">The dependency object</param>
        /// <returns>The ancestor object of given type.</returns>
        private static T GetAncestor<T>(DependencyObject reference) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(reference);
            while (!(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return (T)parent;
        }

        /// <summary>
        /// Applies sort using the given property Name on the given ICollectionView
        /// </summary>
        /// <param name="view">The View of the list</param>
        /// <param name="propertyName">The property on which items will be sorted.</param>
        /// <param name="listView">The list view</param>
        /// <param name="sortedColumnHeader">The column header that needs to be sorting to be applied.</param>
        private static void ApplySort(ICollectionView view, string propertyName, ListView listView, GridViewColumnHeader sortedColumnHeader)
        {
            ListSortDirection direction = ListSortDirection.Ascending;
            if (view.SortDescriptions.Count > 0)
            {
                SortDescription currentSort = view.SortDescriptions[0];
                if (currentSort.PropertyName == propertyName)
                {
                    direction = (currentSort.Direction == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                }

                view.SortDescriptions.Clear();

                GridViewColumnHeader currentSortedColumnHeader = GetSortedColumnHeader(listView);
                if (currentSortedColumnHeader != null)
                {
                    RemoveSortGlyph(currentSortedColumnHeader);
                }
            }

            if (!string.IsNullOrEmpty(propertyName))
            {
                view.SortDescriptions.Add(new SortDescription(propertyName, direction));
                if (GetShowSortGlyph(listView))
                { 
                  AddSortGlyph(sortedColumnHeader, direction, direction == ListSortDirection.Ascending ? GetSortGlyphAscending(listView) : GetSortGlyphDescending(listView));
                }

                SetSortedColumnHeader(listView, sortedColumnHeader);
            }
        }

        /// <summary>
        /// Adds the sort glyph to the given column header.
        /// </summary>
        /// <param name="columnHeader">The column header where the glyph needs to be added</param>
        /// <param name="direction">The direction</param>
        /// <param name="sortGlyph">The glyph</param>
        private static void AddSortGlyph(GridViewColumnHeader columnHeader, ListSortDirection direction, ImageSource sortGlyph)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(columnHeader);
            adornerLayer.Add(new SortGlyphAdorner(columnHeader, direction, sortGlyph));
        }

        /// <summary>
        /// Removes the sort glyph to the given column header.
        /// </summary>
        /// <param name="columnHeader">The column header from where the glyph needs to be removed.</param>
        private static void RemoveSortGlyph(GridViewColumnHeader columnHeader)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(columnHeader);
            Adorner[] adorners = adornerLayer.GetAdorners(columnHeader);
            if (adorners != null)
            {
                foreach (Adorner adorner in adorners)
                {
                    if (adorner is SortGlyphAdorner)
                    {
                        adornerLayer.Remove(adorner);
                    }
                }
            }
        }
    }
}
