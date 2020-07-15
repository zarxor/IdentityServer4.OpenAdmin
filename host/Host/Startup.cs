//  
//  Startup.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using IdentityServer4.Host.Configuration;
using IdentityServer4.OpenAdmin.API.Extensions;
using IdentityServer4.OpenAdmin.BlazorClient.Extensions;
using IdentityServer4.OpenAdmin.InMemory.Extensions;
using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer4.Host
{
    public class Startup
    {
        public IWebHostEnvironment Environment;

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddControllers()
                .AddNewtonsoftJson(); // Required for APIs

            services.AddOpenAdminApi( /*o => o.ApiPrefix = "/test-api/"*/);
            services.AddOpenAdminInMemoryIdentityResources();
            services.AddOpenAdminBlazorClient(options =>
            {
                //options.AddRazorPages = false; // True by default
                //options.AddServerSideBlazor = false; // True by default
                //options.ApiUrl = "/test-api/"; // Default set to "/admin/api/"
            });


            ////services.AddOpenAdminUI(o =>
            ////{
            ////    //o.Path = "/open/";
            ////    //o.ApiUrl = "/test-api/";
            ////});

            ////.AddOpenAdminApiEF();

            var identityServerBuilder = services
                .AddIdentityServer()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddTestUsers(TestUsers.Users);

            if (Environment.IsDevelopment())
            {
                identityServerBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddAuthentication();
        }

        //private void ContextOptionsBuilder(DbContextOptionsBuilder options)
        //{
        //    var migrationsAssemblyName = GetType().Assembly.GetName().Name;
        //    options.UseSqlServer(
        //        "Server=(localdb)\\MSSQLLocalDB;Database=IdentityServer4.OpenAdminUI;Trusted_Connection=True;MultipleActiveResultSets=true",
        //        b => b.MigrationsAssembly(migrationsAssemblyName));
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            app.UseHttpsRedirection();
            //app.UseBlazorFrameworkFiles("/portal");
            app.UseStaticFiles();

            app.Map("/portal", child =>
            {
                child.UseRouting();
                child.UseEndpoints(endpoints =>
                {
                    endpoints.MapBlazorHub();
                    endpoints.MapFallbackToPage("/_Host");
                    //endpoints.MapFallbackToFile("portal/index.html");
                });
            });

            app.UseRouting();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}