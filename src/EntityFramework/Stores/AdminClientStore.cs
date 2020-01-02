//  
//  AdminClientStore.cs
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
    public class AdminClientStore : IAdminStore<Client>
    {
        private readonly IConfigurationDbContext context;

        public AdminClientStore(IConfigurationDbContext context, ILogger<ClientStore> logger)
        {
            this.context = context;
            //mapper = new MapperConfiguration(cfg => { cfg.AddProfile<AdminClientMapperProfile>(); }).CreateMapper();
        }

        public async Task<Client> GetAsync(string clientId)
        {
            var clientEntity = await context.Clients
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.ClientSecrets)
                .Include(x => x.Claims)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.Properties)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
            return clientEntity.ToModel();
        }

        public async Task<List<Client>> GetAllAsync()
        {
            var clientEntities = await context.Clients
                .Include(x => x.AllowedGrantTypes)
                //.Include(x => x.RedirectUris)
                //.Include(x => x.PostLogoutRedirectUris)
                //.Include(x => x.AllowedScopes)
                //.Include(x => x.ClientSecrets)
                //.Include(x => x.Claims)
                //.Include(x => x.IdentityProviderRestrictions)
                //.Include(x => x.AllowedCorsOrigins)
                //.Include(x => x.Properties)
                .ToListAsync();

            return clientEntities.Select(c => c.ToModel()).ToList();
        }

        public async Task<Client> AddAsync(Client client)
        {
            var clientEntity = client.ToEntity();
            var entityEntry = await context.Clients.AddAsync(clientEntity);
            await context.SaveChangesAsync();

            return entityEntry.Entity.ToModel();
        }

        public async Task<Client> SaveAsync(Client client)
        {
            var clientEntity = await context.Clients
                .FirstOrDefaultAsync(c => c.ClientId == client.ClientId);

            //mapper.Map(client, clientEntity);

            await context.SaveChangesAsync();

            return clientEntity.ToModel();
        }

        public Task<bool> RemoveAsync(Client item)
        {
            throw new NotImplementedException();
        }

        public bool SupportsGet => true;
        public bool SupportsAdd => true;
        public bool SupportsSave => true;
        public bool SupportsRemove => true;
    }
}