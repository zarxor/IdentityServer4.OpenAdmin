// 
//  ClientsController.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
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

        [HttpGet("{clientId}/scopes")]
        public async Task<IActionResult> GetClientScopes(string clientId)
        {
            return View("ClientScopes", await adminClientStore.FindClientByIdAsync(clientId));
        }

        [HttpPost("{clientId}/scopes")]
        public async Task<IActionResult> AddClientScopes(string clientId, [FromForm] string scope)
        {
            var client = await adminClientStore.FindClientByIdAsync(clientId);
            if (client != null && client.AllowedScopes.Contains(scope))
            {
                client.AllowedScopes.Add(scope);
                client = await adminClientStore.SaveClientAsync(client);
            }

            return View("ClientScopes", client);
        }

        [HttpGet("{clientId}/token")]
        public async Task<IActionResult> ClientAsync(string clientId)
        {
            return View("ClientGeneral", await adminClientStore.FindClientByIdAsync(clientId));
        }
    }
}