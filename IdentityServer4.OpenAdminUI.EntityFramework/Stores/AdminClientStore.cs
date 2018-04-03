// 
//  AdminClientStore.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4.Models;
using IdentityServer4.OpenAdminUI.Core.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityServer4.OpenAdminUI.EntityFramework.Stores
{
    public class AdminClientStore : ClientStore, IAdminClientStore
    {
        private readonly IConfigurationDbContext context;
        private readonly IMapper mapper;

        public AdminClientStore(IConfigurationDbContext context, ILogger<ClientStore> logger)
            : base(context, logger)
        {
            this.context = context;
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>()).CreateMapper();
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            var clientEntities = await context.Clients
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.ClientSecrets)
                .Include(x => x.Claims)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.Properties)
                .ToListAsync();

            return clientEntities.Select(c => c.ToModel()).ToList();
        }

        public async Task<Client> AddClientAsync(Client client)
        {
            var clientEntity = client.ToEntity();
            var entityEntry = await context.Clients.AddAsync(clientEntity);
            await context.SaveChangesAsync();

            return entityEntry.Entity?.ToModel();
        }

        public async Task<Client> SaveClientAsync(Client client)
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
                .FirstOrDefaultAsync(c => c.ClientId == client.ClientId);

            mapper.Map(client, clientEntity);

            await context.SaveChangesAsync();

            return clientEntity.ToModel();
        }

        public Task<Client> AddClientScopeAsync(string clientId, string scope)
        {
            throw new NotImplementedException();
        }

        public Task<Client> RemoveClientScopeAsync(string clientId, string scope)
        {
            throw new NotImplementedException();
        }

        #region Private

        private async Task<IdentityServer4.EntityFramework.Entities.Client> GetClientEntityAsync(string clientId)
        {
            return await context.Clients
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
        }

        #endregion
    }
}