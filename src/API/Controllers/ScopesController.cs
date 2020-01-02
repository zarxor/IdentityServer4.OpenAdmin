// 
//  ClientsController.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4.OpenAdmin.API.Controllers
{
    [Route(OpenAdminApiOptions.DefaultApiPrefix + "identityresources")]
    public class ScopesController : Controller
    {
        //public ScopesController(IAdminIdentityResourceStore adminResourceStore)
        //{
        //    this.adminResourceStore = adminResourceStore;
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetIdentityResourcesAsync()
        //{
        //    return Ok(await adminResourceStore.GetIdentityResourcesAsync());
        //}
    }
}