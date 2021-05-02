// <copyright file="ServiceProviderExtension.cs" company="https://github.com/dprZoft/shelvesetcomparer">
// Copyright https://github.com/dprZoft/shelvesetcomparer, 2021. All Rights Reserved. 
// This code released under the terms of the Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html). 
// This is sample code only, do not use in production environments.
// </copyright>

using System;

namespace WiredTechSolutions.ShelvesetComparer
{
    public static class ServiceProviderExtension
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider) where TService : class
        {
            return serviceProvider.GetService<TService, TService>();
        }

        public static TOut GetService<TService, TOut>(this IServiceProvider serviceProvider) where TOut : class
        {
            var service = serviceProvider.GetService(typeof(TService));
            if (service == null)
            {
                throw new InvalidOperationException($"Internal error: Service '{typeof(TService).Name}' is not available");
            }

            return service as TOut
                ?? throw new InvalidCastException($"Internal error: Cannot cast '{typeof(TService).Name}' to '{typeof(TOut).Name}'");
        }
    }
}