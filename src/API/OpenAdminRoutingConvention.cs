//  
//  OpenAdminRoutingConvention.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IdentityServer4.OpenAdmin.API
{
    public class OpenAdminRoutingConvention : IControllerModelConvention
    {
        private readonly Assembly assembly;
        private readonly IServiceCollection services;
        private OpenAdminApiOptions openAdminApiOptions;

        public OpenAdminRoutingConvention(IServiceCollection services)
        {
            this.services = services;
            assembly = typeof(OpenAdminRoutingConvention).Assembly;
        }

        public void Apply(ControllerModel controller)
        {
            openAdminApiOptions = openAdminApiOptions
                                  ?? services.BuildServiceProvider()
                                      .GetRequiredService<IOptions<OpenAdminApiOptions>>()?
                                      .Value;

            if (openAdminApiOptions == null)
            {
                return;
            }

            if (controller.ControllerType.Assembly != assembly)
            {
                return;
            }


            var selectors = controller.Selectors
                .Where(selector => selector.AttributeRouteModel != null);

            foreach (var selectorModel in selectors)
            {
                if (selectorModel.AttributeRouteModel.Template.StartsWith(OpenAdminApiOptions.DefaultApiPrefix))
                {
                    selectorModel.AttributeRouteModel.Template =
                        selectorModel.AttributeRouteModel.Template.Substring(
                            OpenAdminApiOptions.DefaultApiPrefix.Length - 1);
                }

                selectorModel.AttributeRouteModel.Template =
                    $"/{openAdminApiOptions.ApiPrefix.Trim('/')}/{selectorModel.AttributeRouteModel.Template.TrimStart('/')}";
            }
        }
    }
}