//  
//  ScopeContract.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer4.OpenAdmin.API.Contracts
{
    public class ScopeContract : Scope
    {
        public new List<string> UserClaims { get; set; }
    }
}