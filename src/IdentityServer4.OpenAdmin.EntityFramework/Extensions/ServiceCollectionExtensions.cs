//  
//  ServiceCollectionExtensions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using IdentityServer4.Models;
using IdentityServer4.OpenAdmin.Core.Stores;
using IdentityServer4.OpenAdmin.EntityFramework.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4.OpenAdmin.EntityFramework.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAdminApiEF(this IServiceCollection services)
        {
            services.AddScoped<IAdminStore<Client>, AdminClientStore>()
                .AddScoped<IAdminStore<IdentityResource>, AdminIdentityResourceStore>();

            return services;
        }
    }
}