//  
//  OpenAdminUIOptions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

namespace IdentityServer4.OpenAdmin.UI
{
    public class OpenAdminUIOptions
    {
        public const string DefaultPath = "/admin/";
        private string path;

        /// <summary>
        ///     Default: /OpenAdmin/
        /// </summary>
        public string Path
        {
            get => path ?? DefaultPath;
            set => path = value;
        }

        public string ApiUrl = "/admin/api/";
    }
}