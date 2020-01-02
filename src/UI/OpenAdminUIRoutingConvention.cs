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

namespace IdentityServer4.OpenAdmin.UI
{
    public class OpenAdminUIRoutingConvention : IControllerModelConvention
    {
        private readonly Assembly assembly;
        private readonly IServiceCollection services;
        private OpenAdminUIOptions openAdminUIOptions;

        public OpenAdminUIRoutingConvention(IServiceCollection services)
        {
            this.services = services;
            assembly = GetType().Assembly;
        }

        public void Apply(ControllerModel controller)
        {
            openAdminUIOptions = openAdminUIOptions
                                  ?? services.BuildServiceProvider()
                                      .GetRequiredService<IOptions<OpenAdminUIOptions>>()?
                                      .Value;

            if (openAdminUIOptions == null)
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
                if (selectorModel.AttributeRouteModel.Template.StartsWith(OpenAdminUIOptions.DefaultPath))
                {
                    selectorModel.AttributeRouteModel.Template =
                        selectorModel.AttributeRouteModel.Template.Substring(
                            OpenAdminUIOptions.DefaultPath.Length - 1);
                }

                selectorModel.AttributeRouteModel.Template =
                    $"/{openAdminUIOptions.Path.Trim('/')}/{selectorModel.AttributeRouteModel.Template.TrimStart('/')}";
            }
        }
    }
}