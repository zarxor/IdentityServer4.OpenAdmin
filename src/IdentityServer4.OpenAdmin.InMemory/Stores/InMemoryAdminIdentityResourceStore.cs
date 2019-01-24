//  
//  InMemoryAdminIdentityResourceStore.cs
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
    public class InMemoryAdminIdentityResourceStore : IAdminStore<IdentityResource>
    {
        private readonly IEnumerable<IdentityResource> context;

        public InMemoryAdminIdentityResourceStore(IEnumerable<IdentityResource> context)
        {
            this.context = context;
        }

        public bool SupportsGet => true;
        public bool SupportsAdd => false;
        public bool SupportsSave => true;
        public bool SupportsRemove => false;

        public Task<List<IdentityResource>> GetAllAsync()
        {
            return Task.FromResult(context.ToList());
        }

        public Task<IdentityResource> GetAsync(string name)
        {
            return Task.FromResult(context.FirstOrDefault(r => r.Name == name));
        }

        public Task<IdentityResource> AddAsync(IdentityResource identityResource)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResource> SaveAsync(IdentityResource identityResource)
        {
            return Task.FromResult(identityResource);
        }

        public Task<bool> RemoveAsync(IdentityResource item)
        {
            throw new NotImplementedException();
        }
    }
}