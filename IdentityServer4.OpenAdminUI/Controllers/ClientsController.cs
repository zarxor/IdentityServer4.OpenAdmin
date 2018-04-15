// 
//  ClientsController.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.OpenAdminUI.Core.Models;
using IdentityServer4.OpenAdminUI.Core.Stores;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4.OpenAdminUI.Controllers
{
    [Route("clients")]
    public class ClientsController : Controller
    {
        private readonly IAdminClientStore adminClientStore;

        public ClientsController(IAdminClientStore adminClientStore)
        {
            this.adminClientStore = adminClientStore;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetClients()
        {
            return View("Clients", await adminClientStore.GetClientsAsync());
        }

        [HttpGet("new")]
        public async Task<IActionResult> NewClient()
        {
            return View("ClientNew", new Client
            {
                ClientId = Guid.NewGuid().ToString()
            });
        }

        [HttpPost("new")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClient(Client client)
        {
            if (TryValidateModel(client))
            {
                client = await adminClientStore.AddClientAsync(client);
                return RedirectToAction("GetClientGeneral", new {clientId = client.ClientId});
            }

            return View("ClientNew", client);
        }

        [HttpGet("{clientId}/token")]
        public async Task<IActionResult> ClientAsync(string clientId)
        {
            return View("ClientGeneral", await adminClientStore.FindClientByIdAsync(clientId));
        }

        #region Secrets

        [HttpGet("{clientId}/secrets")]
        public async Task<IActionResult> GetClientSecrets(string clientId, [FromQuery] int removeSecret)
        {
            //if (!string.IsNullOrWhiteSpace(removeScope))
            //{
            //    await adminClientStore.RemoveClientScopeAsync(clientId, removeScope);
            //}

            return View("ClientSecrets", await adminClientStore.FindClientByIdAsync(clientId));
        }

        [HttpPost("{clientId}/secrets")]
        public async Task<IActionResult> AddClientSecret(string clientId, [FromForm] ClientSecret clientSecret)
        {
            clientSecret.Value = clientSecret.Value.ToSha512();

            await adminClientStore.AddClientSecretAsync(clientId, clientSecret);

            return View("ClientSecrets", await adminClientStore.FindClientByIdAsync(clientId));
        }

        #endregion

        #region Scopes

        [HttpGet("{clientId}/scopes")]
        public async Task<IActionResult> GetClientScopes(string clientId, [FromQuery] string removeScope)
        {
            if (!string.IsNullOrWhiteSpace(removeScope))
            {
                await adminClientStore.RemoveClientScopeAsync(clientId, removeScope);
            }

            return View("ClientScopes", await adminClientStore.FindClientByIdAsync(clientId));
        }

        [HttpPost("{clientId}/scopes")]
        public async Task<IActionResult> AddClientScope(string clientId, [FromForm] string scope)
        {
            await adminClientStore.AddClientScopeAsync(clientId, scope);

            return View("ClientScopes", await adminClientStore.FindClientByIdAsync(clientId));
        }

        [HttpDelete("{clientId}/scopes")]
        public async Task<IActionResult> RemoveClientScope(string clientId, [FromForm] string scope)
        {
            await adminClientStore.AddClientScopeAsync(clientId, scope);

            return View("ClientScopes", await adminClientStore.FindClientByIdAsync(clientId));
        }

        #endregion


        #region General

        [HttpGet("{clientId}/general")]
        public async Task<IActionResult> GetClientGeneral(string clientId)
        {
            return View("ClientGeneral", await adminClientStore.FindClientByIdAsync(clientId));
        }

        [HttpPost("{clientId}/general")]
        public async Task<IActionResult> SaveClientGeneral(string clientId, Client client)
        {
            if (TryValidateModel(client)) client = await adminClientStore.SaveClientAsync(client);

            return View("ClientGeneral", client);
        }

        #endregion
    }
}