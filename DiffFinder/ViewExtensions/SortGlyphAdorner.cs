// <copyright file="SortGlyphAdorner.cs" company="https://github.com/rajeevboobna/CompareShelvesets">Copyright https://github.com/rajeevboobna/CompareShelvesets. All Rights Reserved. This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) This is sample code only, do not use in production environments.</copyright>

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DiffFinder
{
    /// <summary>
    /// The class creates a layer on top of Grid Column headers showing the ascending or descending arrow based on how the sort is selected.
    /// </summary>
    internal class SortGlyphAdorner : Adorner
    {
        /// <summary>
        /// The underlying column  header.
        /// </summary>
        private GridViewColumnHeader columnHeader;

        /// <summary>
        /// The current sort direction
        /// </summary>
        private ListSortDirection direction;

        /// <summary>
        /// The Image glyph to be shown on column
        /// </summary>
        private ImageSource sortGlyph;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortGlyphAdorner"/> class.
        /// </summary>
        /// <param name="columnHeader">The column header</param>
        /// <param name="direction">The direction</param>
        /// <param name="sortGlyph">The image glyph</param>
        public SortGlyphAdorner(GridViewColumnHeader columnHeader, ListSortDirection direction, ImageSource sortGlyph)
            : base(columnHeader)
        {
            this.columnHeader = columnHeader;
            this.direction = direction;
            this.sortGlyph = sortGlyph;
        }

        /// <summary>
        /// The overridden OnRender method that displays image on top of attached UIElement.
        /// </summary>
        /// <param name="drawingContext">The drawing context object.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.sortGlyph != null)
            {
                double x = this.columnHeader.ActualWidth - 13;
                double y = (this.columnHeader.ActualHeight / 2) - 5;
                Rect rect = new Rect(x, y, 10, 10);
                drawingContext.DrawImage(this.sortGlyph, rect);
            }
            else
            {
                drawingContext.DrawGeometry(Brushes.LightGray, new Pen(Brushes.Gray, 1.0), this.GetDefaultGlyph());
            }
        }

        /// <summary>
        /// Returns the Glyph to display
        /// </summary>
        /// <returns>Glyph object</returns>
        private Geometry GetDefaultGlyph()
        {
            double x1 = this.columnHeader.ActualWidth - 13;
            double x2 = x1 + 10;
            double x3 = x1 + 5;
            double y1 = (this.columnHeader.ActualHeight / 2) - 3;
            double y2 = y1 + 5;

            if (this.direction == ListSortDirection.Ascending)
            {
                double tmp = y1;
                y1 = y2;
                y2 = tmp;
            }

            PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();
            pathSegmentCollection.Add(new LineSegment(new Point(x2, y1), true));
            pathSegmentCollection.Add(new LineSegment(new Point(x3, y2), true));

            PathFigure pathFigure = new PathFigure(new Point(x1, y1), pathSegmentCollection, true);

            PathFigureCollection pathFigureCollection = new PathFigureCollection();
            pathFigureCollection.Add(pathFigure);

            PathGeometry pathGeometry = new PathGeometry(pathFigureCollection);
            return pathGeometry;
        }
    }
}
