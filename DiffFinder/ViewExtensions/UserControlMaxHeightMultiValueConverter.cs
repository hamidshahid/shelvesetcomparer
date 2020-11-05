// <copyright file="GridViewSorter.cs" company="http://shelvesetcomparer.codeplex.com">
// Copyright http://shelvesetcomparer.codeplex.com. All Rights Reserved. 
// This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html).
// This is sample code only, do not use in production environments.
// </copyright>

namespace WiredTechSolutions.ShelvesetComparer
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converter to update UserControl.MaxHeight.
    /// Reducing MaxHeight to visible area ensures used ListView will only load items for visible area which improves loading a lot with >500 Shelvesets
    /// Algorithm: Use TeamExplorer-ScrollViewPresenter MaxHeight - 35.0 (nav bar height)
    /// 
    /// Based on https://social.technet.microsoft.com/wiki/contents/articles/30936.wpf-multibinding-and-imultivalueconverter.aspx
    /// </summary>
    public class UserControlMaxHeightMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 1)
            {
                throw new ArgumentException("Converter needs at least 1 Binding argument: ActualHeight of TeamExplorerFrame");
            }

            var teamExplorerFrameActualHeight = ConvertValue(values[0]);
            // magic number: 35.0 is the height of the nav bar
            var controlsActualHeightToRemove = 35.0;
            for (var idx =1; idx < values.Length; idx++) 
            {
                controlsActualHeightToRemove += ConvertValue(values[idx]);
            }

            var resultHeight = teamExplorerFrameActualHeight - controlsActualHeightToRemove;
            if (resultHeight < 0)
            {
                // if negative, we do not set a value
                return double.PositiveInfinity;
            }

            return resultHeight;
        }

        private double ConvertValue(object bindingValue)
        {
            return (bindingValue != null
                && bindingValue != DependencyProperty.UnsetValue)
                    ? System.Convert.ToDouble(bindingValue)
                    : 0.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
