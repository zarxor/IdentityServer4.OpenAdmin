//  
//  OpenAdminApiOptions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

namespace IdentityServer4.OpenAdmin.API
{
    public class OpenAdminApiOptions
    {
        public const string DefaultApiPrefix = "/admin/api/";
        private string pathPrefix;

        /// <summary>
        /// Default: /oa-api/
        /// </summary>
        public string ApiPrefix
        {
            get => pathPrefix ?? DefaultApiPrefix;
            set => pathPrefix = value;
        }
    }
}