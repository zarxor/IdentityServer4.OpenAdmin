// 
//  ClientSecretExtensions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.OpenAdminUI.EntityFramework.MapperProfiles;

namespace IdentityServer4.OpenAdminUI.EntityFramework.Extensions
{
    public static class ClientSecretExtensions
    {
        static ClientSecretExtensions()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<AdminClientMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static ClientSecret ToEntity(this Core.Models.ClientSecret clientSecret)
        {
            return Mapper.Map<ClientSecret>(clientSecret);
        }

        public static Core.Models.ClientSecret ToAdminModel(this ClientSecret clientSecret)
        {
            return Mapper.Map<Core.Models.ClientSecret>(clientSecret);
        }
    }
}