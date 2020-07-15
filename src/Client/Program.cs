
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentityServer4.OpenAdmin.BlazorClient
{
    public class Program
    {
        //public static async Task Main(string[] args)
        //{
        //    var builder = WebAssemblyHostBuilder.CreateDefault(args);
        //    builder.RootComponents.Add<App>("app");

        //    builder.Services.AddBaseAddressHttpClient();
        //    builder.Services.AddScoped<DialogService>();
        //    builder.Services.AddScoped<NotificationService>();
        //    builder.Services.AddBlazorState(options =>
        //    {
        //        options.Assemblies = new[]
        //        {
        //            typeof(Program).GetTypeInfo().Assembly
        //        };
        //    });

        //    await builder.Build().RunAsync();
        //}

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseStartup<Startup>();
                });
        }
    }
}