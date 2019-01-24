//  
//  IAdminStore.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer4.OpenAdmin.Core.Stores
{
    public interface IAdminStore<TItem>
    {
        bool SupportsGet { get; }
        bool SupportsAdd { get; }
        bool SupportsSave { get; }
        bool SupportsRemove { get; }

        Task<List<TItem>> GetAllAsync();
        Task<TItem> GetAsync(string id);
        Task<TItem> AddAsync(TItem item);
        Task<TItem> SaveAsync(TItem item);
        Task<bool> RemoveAsync(TItem item);
    }
}