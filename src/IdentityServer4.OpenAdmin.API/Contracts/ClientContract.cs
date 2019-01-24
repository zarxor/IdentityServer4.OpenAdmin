//  
//  ClientContract.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityServer4.Models;

namespace IdentityServer4.OpenAdmin.API.Contracts
{
    public class ClientContract : Client
    {
        public new List<Secret> ClientSecrets { get; set; }
        public new List<string> RedirectUris { get; set; }
        public new List<string> PostLogoutRedirectUris { get; set; }
        public new List<string> AllowedScopes { get; set; }
        public new List<string> IdentityProviderRestrictions { get; set; }
        public new List<Claim> Claims { get; set; }
        public new List<string> AllowedCorsOrigins { get; set; }
    }
}