//  
//  ServiceCollectionExtensions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4.OpenAdmin.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAdminApi(this IServiceCollection services,
            Action<OpenAdminApiOptions> options = null)
        {
            if (options != null)
            {
                services.Configure(options);
            }

            services.AddSingleton<IAdminMapper>(
                new AdminContractMapper(new MapperConfiguration(m => m.AddProfile<AdminContractMapperProfile>())));

            services.Configure<MvcOptions>(o => o.Conventions.Add(new OpenAdminRoutingConvention(services)));
            return services;
        }
    }
}