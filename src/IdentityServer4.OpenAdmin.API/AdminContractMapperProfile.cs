//  
//  AdminContractMapperProfile.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.OpenAdmin.API.Contracts;

namespace IdentityServer4.OpenAdmin.API
{
    public class AdminContractMapperProfile : Profile
    {
        public AdminContractMapperProfile()
        {
            CreateMap<Client, ClientContract>()
                .ReverseMap();

            CreateMap<IdentityResource, IdentityResourceContract>()
                .ReverseMap();

            CreateMap<ApiResource, ApiResourceContract>()
                .ReverseMap();

            CreateMap<Scope, ScopeContract>()
                .ReverseMap();
        }
    }
}