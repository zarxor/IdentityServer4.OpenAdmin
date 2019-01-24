//  
//  ServiceCollectionExtensions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4.OpenAdmin.UI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAdminUI(this IServiceCollection services,
            Action<OpenAdminUIOptions> options = null)
        {
            if (options != null)
            {
                services.Configure(options);
            }

            services.ConfigureOptions(typeof(OpenAdminUIConfigureOptions));
            services.Configure<MvcOptions>(o => o.Conventions.Add(new OpenAdminUIRoutingConvention(services)));

            return services;
        }
    }
}