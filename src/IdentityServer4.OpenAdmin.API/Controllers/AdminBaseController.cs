//  
//  AdminBaseController.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.OpenAdmin.Core.Stores;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4.OpenAdmin.API.Controllers
{
    public abstract class BaseController<TItem, TContract> : Controller
        where TContract : class
    {
        protected readonly IAdminStore<TItem> AdminStore;
        protected readonly IAdminMapper AdminMapper;

        protected BaseController(IAdminStore<TItem> adminStore, IAdminMapper adminMapper)
        {
            AdminStore = adminStore;
            AdminMapper = adminMapper;
        }

        protected async Task<IActionResult> GetAllAsync()
        {
            return Ok(await AdminStore.GetAllAsync());
        }

        protected async Task<IActionResult> AddAsync(TContract contract, Func<TItem, object> id)
        {
            var item = AdminMapper.Map<TItem>(contract);
            if (TryValidateModel(item))
            {
                item = await AdminStore.AddAsync(item);
                return Created("", item);
            }

            return BadRequest();
        }

        protected async Task<IActionResult> GetAsync(string id)
        {
            var model = await AdminStore.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(AdminMapper.Map<TContract>(model));
        }

        protected async Task<IActionResult> PatchAsync(string id, JsonPatchDocument<TContract> document)
        {
            var item = await AdminStore.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var contract = AdminMapper.Map<TContract>(item);

            document.ApplyTo(contract);

            AdminMapper.Map(contract, item);

            await AdminStore.SaveAsync(item);

            return Ok(item);
        }
    }
}