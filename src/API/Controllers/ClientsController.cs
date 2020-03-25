//  
//  ClientsController.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.OpenAdmin.API.Contracts;
using IdentityServer4.OpenAdmin.Core.Stores;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4.OpenAdmin.API.Controllers
{
    [Route(OpenAdminApiOptions.DefaultApiPrefix + "clients")]
    public class ClientsController : BaseController<Client, ClientContract>
    {
        public ClientsController(IAdminStore<Client> adminStore, IAdminMapper adminMapper) : base(adminStore,
            adminMapper)
        {
        }


        [HttpGet]
        public new async Task<IActionResult> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ClientContract contract)
        {
            return await base.AddAsync(contract, c => c.ClientId);
        }

        [HttpGet("{id}")]
        public new async Task<IActionResult> GetAsync(string id)
        {
            return await base.GetAsync(id);
        }

        [HttpPatch("{id}")]
        public new async Task<IActionResult> PatchAsync(string id,
            [FromBody] JsonPatchDocument<ClientContract> document)
        {
            return await base.PatchAsync(id, document);
        }
    }
}