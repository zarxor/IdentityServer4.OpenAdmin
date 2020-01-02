//  
//  AdminIdentityResourceStore.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4.Models;
using IdentityServer4.OpenAdmin.Core.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityServer4.OpenAdmin.EntityFramework.Stores
{
    public class AdminIdentityResourceStore : IAdminStore<IdentityResource>
    {
        private readonly IConfigurationDbContext context;

        public AdminIdentityResourceStore(IConfigurationDbContext context, ILogger<ClientStore> logger)
        {
            this.context = context;
        }

        public bool SupportsGet => true;
        public bool SupportsAdd => true;
        public bool SupportsSave => true;
        public bool SupportsRemove => true;

        public async Task<IdentityResource> GetAsync(string name)
        {
            var identityResourceEntity = await context.IdentityResources
                .Include(r => r.Properties)
                .Include(r => r.UserClaims)
                .FirstOrDefaultAsync(r => r.Name == name);

            return identityResourceEntity.ToModel();
        }

        public async Task<List<IdentityResource>> GetAllAsync()
        {
            var identityResourceEntities = await context.IdentityResources.ToListAsync();
            return identityResourceEntities
                .Select(r => r.ToModel())
                .ToList();
        }

        public async Task<IdentityResource> AddAsync(IdentityResource identityResource)
        {
            var identityResourceEntity = identityResource.ToEntity();

            await context.IdentityResources.AddAsync(identityResourceEntity);
            await context.SaveChangesAsync();

            return identityResourceEntity.ToModel();
        }

        public Task<IdentityResource> SaveAsync(IdentityResource identityResource)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(IdentityResource item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}