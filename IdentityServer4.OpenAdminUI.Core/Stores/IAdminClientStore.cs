// 
//  IAdminClientStore.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.OpenAdminUI.Core.Models;
using IdentityServer4.Stores;

namespace IdentityServer4.OpenAdminUI.Core.Stores
{
    public interface IAdminClientStore : IClientStore
    {
        Task<List<Client>> GetClientsAsync();
        Task<Client> AddClientAsync(Client client);
        Task<Client> SaveClientAsync(Client client);
        Task<bool> AddClientScopeAsync(string clientId, string scope);
        Task<bool> RemoveClientScopeAsync(string clientId, string scope);
        Task<List<ClientSecret>> GetClientSecretsAsync(string clientId);
        Task<ClientSecret> AddClientSecretAsync(string clientId, ClientSecret clientSecret);
        Task<bool> RemoveClientSecretAsync(string clientId, ClientSecret clientSecret);

        //Task<bool> RemoveClientSecretAsync(string clientId, string secret);
        //Task<bool> AddClientRedirectUriAsync(string clientId, Uri uri);
        //Task<bool> RemoveClientRedirectUriAsync(string clientId, Uri uri);
        //Task<bool> AddClientPostLogoutRedirectUriAsync(string clientId, Uri uri);
        //Task<bool> RemoveClientPostLogoutRedirectUriAsync(string clientId, Uri uri);
    }
}