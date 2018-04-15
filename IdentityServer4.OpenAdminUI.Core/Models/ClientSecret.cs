// 
//  ClientSecret.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using IdentityServer4.Models;

namespace IdentityServer4.OpenAdminUI.Core.Models
{
    public class ClientSecret : Secret
    {
        public int Id { get; set; }
    }
}