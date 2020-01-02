//  
//  ServiceCollectionExtensions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using IdentityServer4.Models;
using IdentityServer4.OpenAdmin.Core.Stores;
using IdentityServer4.OpenAdmin.InMemory.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4.OpenAdmin.InMemory.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAdminInMemoryIdentityResources(this IServiceCollection services)
        {
            services.AddScoped<IAdminStore<Client>, InMemoryAdminClientStore>()
                .AddScoped<IAdminStore<IdentityResource>, InMemoryAdminIdentityResourceStore>()
                .AddScoped<IAdminStore<ApiResource>, InMemoryAdminApiResourceStore>();

            return services;
        }
    }
}