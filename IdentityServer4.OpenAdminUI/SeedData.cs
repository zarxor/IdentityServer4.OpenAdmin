// 
//  SeedData.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4.OpenAdminUI
{
    public static class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            //var persistedGrantContext = serviceProvider.GetService<PersistedGrantDbContext>();
            //persistedGrantContext.Database.Migrate();

            var configurationContext = serviceProvider.GetService<ConfigurationDbContext>();
            configurationContext.Database.Migrate();

            //SeedUsers(serviceProvider);
            //SeedClients(serviceProvider);
        }
    }
}