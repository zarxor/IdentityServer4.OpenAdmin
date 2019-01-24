//  
//  InMemoryAdminApiResourceStore.cs
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
    public class InMemoryAdminApiResourceStore : IAdminStore<ApiResource>
    {
        private readonly IEnumerable<ApiResource> context;

        public InMemoryAdminApiResourceStore(IEnumerable<ApiResource> context)
        {
            this.context = context;
        }

        public bool SupportsGet => true;
        public bool SupportsAdd => false;
        public bool SupportsSave => true;
        public bool SupportsRemove => false;

        public Task<List<ApiResource>> GetAllAsync()
        {
            return Task.FromResult(context.ToList());
        }

        public Task<ApiResource> GetAsync(string name)
        {
            return Task.FromResult(context.FirstOrDefault(r => r.Name == name));
        }

        public Task<ApiResource> AddAsync(ApiResource apiResource)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResource> SaveAsync(ApiResource apiResource)
        {
            return Task.FromResult(apiResource);
        }

        public Task<bool> RemoveAsync(ApiResource item)
        {
            throw new NotImplementedException();
        }
    }
}