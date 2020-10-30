// <copyright file="ServiceProviderExtension.cs" company="https://github.com/rajeevboobna/CompareShelvesets">
// Copyright https://github.com/rajeevboobna/CompareShelvesets. All Rights Reserved.
// This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html).
// This is sample code only, do not use in production environments.
// </copyright>

using System;

namespace DiffFinder
{
    public static class ServiceProviderExtension
    {
        public static T GetService<T>(this IServiceProvider serviceProvider) where T : class
        {
            return serviceProvider.GetService<T, T>();
        }

        public static TOut GetService<TIn, TOut>(this IServiceProvider serviceProvider) where TIn : class
                                                                                        where TOut : class
        {
            return serviceProvider.GetService(typeof(TIn)) as TOut ?? throw new InvalidOperationException($"Internal error: Interface '{typeof(TOut).Name}' is not available");
        }
    }
}