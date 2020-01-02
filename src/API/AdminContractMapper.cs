//  
//  AdminContractMapper.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using AutoMapper;

namespace IdentityServer4.OpenAdmin.API
{
    public class AdminContractMapper : Mapper, IAdminMapper
    {
        public AdminContractMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
        {
        }

        public AdminContractMapper(IConfigurationProvider configurationProvider, Func<Type, object> serviceCtor) : base(
            configurationProvider, serviceCtor)
        {
        }
    }

    public interface IAdminMapper : IMapper
    {
    }
}