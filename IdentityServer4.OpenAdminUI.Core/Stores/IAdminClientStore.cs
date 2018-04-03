// 
//  IAdminClientStore.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace IdentityServer4.OpenAdminUI.Core.Stores
{
    public interface IAdminClientStore : IClientStore
    {
        Task<List<Client>> GetClientsAsync();
        Task<Client> AddClientAsync(Client client);
        Task<Client> SaveClientAsync(Client client);
        Task<Client> AddClientScopeAsync(string clientId, string scope);
        Task<Client> RemoveClientScopeAsync(string clientId, string scope);
    }
}