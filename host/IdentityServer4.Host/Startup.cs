//  
//  Startup.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using IdentityServer4.Host.Configuration;
using IdentityServer4.OpenAdmin.API.Extensions;
using IdentityServer4.OpenAdmin.InMemory.Extensions;
using IdentityServer4.OpenAdmin.UI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4.Host
{
    public class Startup
    {
        private readonly IHostingEnvironment environment;

        public Startup(IHostingEnvironment environment) => this.environment = environment;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOpenAdminApi(/*o => o.ApiPrefix = "/test-api/"*/);
            services.AddOpenAdminInMemoryIdentityResources();
            services.AddOpenAdminUI(o =>
            {
                //o.Path = "/open/";
                //o.ApiUrl = "/test-api/";
            });

            services.AddMvc();

                //.AddOpenAdminApiEF();

            var identityServerBuilder = services
                .AddIdentityServer()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddInMemoryApiResources(Resources.GetApiResources());

            if (environment.IsDevelopment())
            {
                identityServerBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddAuthentication();
        }

        private void ContextOptionsBuilder(DbContextOptionsBuilder options)
        {
            var migrationsAssemblyName = GetType().Assembly.GetName().Name;
            options.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=IdentityServer4.OpenAdminUI;Trusted_Connection=True;MultipleActiveResultSets=true",
                b => b.MigrationsAssembly(migrationsAssemblyName));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer()
                .UseStaticFiles()
                .UseMvcWithDefaultRoute();
        }
    }
}