//  
//  AdminScopeStore.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.OpenAdmin.Core.Stores;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer4.OpenAdmin.AspNetIdentity
{
    public class AdminScopeStore : IAdminStore<Scope>
    {
        private readonly IUserStore<AdminScopeStore> userStore;

        public AdminScopeStore(IUserStore<AdminScopeStore> userStore)
        {
            this.userStore = userStore;
        }

        public bool SupportsGet => true;
        public bool SupportsAdd => true;
        public bool SupportsSave => true;
        public bool SupportsRemove => true;
        public Task<List<Scope>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Scope> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Scope> AddAsync(Scope item)
        {
            throw new NotImplementedException();
        }

        public Task<Scope> SaveAsync(Scope item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Scope item)
        {
            throw new NotImplementedException();
        }
    }
}