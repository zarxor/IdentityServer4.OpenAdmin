//  
//  MetadataController.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.OpenAdmin.API.Shared.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NJsonSchema.Generation;

namespace IdentityServer4.OpenAdmin.API.Controllers
{
    [ApiController]
    [Route(OpenAdminApiOptions.DefaultApiPrefix + "metadata")]
    public class MetadataController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetMetadataAsync()
        {
            var result = Ok(new
            {
                GrantTypes = GetConstValues(typeof(GrantType)),
                ProtocolTypes = GetConstValues(typeof(IdentityServerConstants.ProtocolTypes)),
                TokenTypes = GetConstValues(typeof(IdentityServerConstants.TokenTypes)),
                StandardScopes = GetConstValues(typeof(IdentityServerConstants.StandardScopes)),
                TokenExpirationTypes = GetEnumValues<TokenExpiration>(),
                TokenUsageTypes = GetEnumValues<TokenUsage>(),
                AccessTokenTypes = GetEnumValues<AccessTokenType>(),
                DefaultClient = new Client()
            });

            return result;
        }

        [HttpGet("contracts")]
        public async Task<IActionResult> GetContractsAsync()
        {
            var fetchFrom = new[]
            {
                typeof(Client),
                typeof(IdentityResource),
                typeof(ApiResource)
            };

            //var generator = new JSchemaGenerator {ContractResolver = new CamelCasePropertyNamesContractResolver()};
            var schemas = new Dictionary<string, object>();
            var generatorSettings = new JsonSchemaGeneratorSettings
            {
                SerializerSettings = new JsonSerializerSettings(), 
                FlattenInheritanceHierarchy = true
            };
            generatorSettings.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //generatorSettings.SerializerSettings.PreserveReferencesHandling =
            //    PreserveReferencesHandling.All;
            foreach (var type in fetchFrom)
            {
                var schema = JsonSchema.FromType(type, generatorSettings);
                schemas.Add(ToLowerFirstLetter(type.Name), JsonConvert.DeserializeObject(schema.ToJson()));
            }

            return Ok(schemas);
        }

        private string ToLowerFirstLetter(string str)
        {
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        private IEnumerable<MetadataItem<int>> GetEnumValues<TEnumType>() where TEnumType : struct, IConvertible
        {
            return Enum.GetValues(typeof(TEnumType))
                .OfType<TEnumType>()
                .Select(t => new MetadataItem<int>
                {
                    Id = t.ToInt32(NumberFormatInfo.CurrentInfo),
                    Name = t.ToString()
                });
        }

        private static IEnumerable<MetadataItem<string>> GetConstValues(IReflect classType)
        {
            return classType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(f => new MetadataItem<string>
                {
                    Id = (string) f.GetValue(null),
                    Name = Regex.Replace(f.Name, "[A-Z]", " $0").Trim()
                });
        }

        public class MetadataItem<TId>
        {
            public TId Id { get; set; }
            public string Name { get; set; }
        }
    }
}