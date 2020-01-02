//  
//  InMemoryAdminClientStore.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.OpenAdmin.Core.Stores;

namespace IdentityServer4.OpenAdmin.InMemory.Stores
{
    public class InMemoryAdminClientStore : IAdminStore<Client>
    {
        private readonly IEnumerable<Client> context;

        public InMemoryAdminClientStore(IEnumerable<Client> context)
        {
            this.context = context;
        }

        public Task<Client> GetAsync(string id)
        {
            return Task.FromResult(context.FirstOrDefault(c => c.ClientId == id));
        }

        public Task<List<Client>> GetAllAsync()
        {
            return Task.FromResult(context.ToList());
        }

        public Task<Client> AddAsync(Client client)
        {
            throw new Exception("Unable to add when in memory");
        }

        public Task<Client> SaveAsync(Client client)
        {
            var clientEntity = context.FirstOrDefault(c => c.ClientId == client.ClientId);

            return Task.FromResult(clientEntity);
        }

        public Task<bool> RemoveAsync(Client item)
        {
            throw new NotImplementedException();
        }

        public bool SupportsGet => true;
        public bool SupportsAdd => false;
        public bool SupportsSave => true;
        public bool SupportsRemove => false;
    }
}