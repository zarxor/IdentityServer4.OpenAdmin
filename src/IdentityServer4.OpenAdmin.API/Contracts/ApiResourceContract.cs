//  
//  ApiResourceContract.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer4.OpenAdmin.API.Contracts
{
    public class ApiResourceContract : ApiResource
    {
        public new List<Secret> ApiSecrets { get; set; }
        public new List<ScopeContract> Scopes { get; set; }
        public new List<string> UserClaims { get; set; }
    }
}