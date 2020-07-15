//  
//  ApiResourcesController.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.OpenAdmin.API.Shared.Contracts;
using IdentityServer4.OpenAdmin.Core.Stores;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4.OpenAdmin.API.Controllers
{
    [Route(OpenAdminApiOptions.DefaultApiPrefix + "api-resources")]
    public class ApiResourcesController : BaseController<ApiResource, ApiResourceContract>
    {
        public ApiResourcesController(IAdminStore<ApiResource> adminStore, IAdminMapper adminMapper)
            : base(adminStore, adminMapper)
        {
        }

        [HttpGet]
        public new async Task<IActionResult> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ApiResourceContract contract)
        {
            return await base.AddAsync(contract, c => c.Name);
        }

        [HttpGet("{id}")]
        public new async Task<IActionResult> GetAsync(string id)
        {
            return await base.GetAsync(id);
        }

        [HttpPatch("{id}")]
        public new async Task<IActionResult> PatchAsync(string id,
            [FromBody] JsonPatchDocument<ApiResourceContract> document)
        {
            return await base.PatchAsync(id, document);
        }
    }
}