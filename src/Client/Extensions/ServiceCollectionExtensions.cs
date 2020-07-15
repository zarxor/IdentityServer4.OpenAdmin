//  
//  ServiceCollectionExtensions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using IdentityServer4.OpenAdmin.BlazorClient.Services;
using Microsoft.Extensions.DependencyInjection;
using Tabler;

namespace IdentityServer4.OpenAdmin.BlazorClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAdminBlazorClient(this IServiceCollection services,
            Action<OpenAdminBlazorClientOptions> options = null)
        {
            options ??= clientOptions => { };
            services.Configure(options);

            services.AddRazorPages();
            services.AddServerSideBlazor().AddHubOptions(o => { o.MaximumReceiveMessageSize = 10 * 1024 * 1024; });

            services
                .AddScoped<OpenAdminApiService>()
                .AddScoped<TablerToastService>();
            //.AddScoped<DialogService>()
            //.AddScoped<NotificationService>();

            return services;
        }
    }
}