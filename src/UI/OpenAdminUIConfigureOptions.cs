//  
//  OpenAdminUIConfigureOptions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace IdentityServer4.OpenAdmin.UI
{
    internal class OpenAdminUIConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        private readonly IWebHostEnvironment environment;

        public OpenAdminUIConfigureOptions(IWebHostEnvironment environment) => this.environment = environment;

        public void PostConfigure(string name, StaticFileOptions options)
        {
            // Basic initialization in case the options weren't initialized by any other component
            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();

            if (options.FileProvider == null && environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? environment.WebRootFileProvider;


            // Add our provider
            var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "resources");
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }
    }
}