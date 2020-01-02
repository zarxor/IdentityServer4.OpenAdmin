//  
//  OpenAdminController.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.OpenAdmin.UI.Contracts;
using IdentityServer4.OpenAdmin.UI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IdentityServer4.OpenAdmin.UI.Controllers
{
    [Route(OpenAdminUIOptions.DefaultPath)]
    public class OpenAdminController : Controller
    {
        private readonly OpenAdminUIOptions options;

        public OpenAdminController(IOptions<OpenAdminUIOptions> options)
        {
            this.options = options.Value;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("api/routes")]
        public async Task<IActionResult> GetRoutesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var url = options.ApiUrl.StartsWith("/")
                    ? $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{options.ApiUrl}"
                    : options.ApiUrl;
                var dataResponse = await httpClient.GetStringAsync($"{url}/metadata/contracts");
                var dataContracts =
                    JsonConvert.DeserializeObject<Dictionary<string, DataContractDefinition>>(dataResponse,
                        new JsonSerializerSettings
                        {
                            MetadataPropertyHandling = MetadataPropertyHandling.Ignore
                        });


                return Ok(new[]
                {
                    new RouteContract
                    {
                        Name = "Dashboard",
                        Path = "/dashboard",
                        Icon = "dashboard",
                        ShowInMenu = true,
                        Type = RouteType.Dashboard,
                        MenuGroup = "Home"
                    },
                    new RouteContract
                    {
                        Name = "Clients",
                        Path = "/clients",
                        Icon = "group",
                        Type = RouteType.List,
                        MenuGroup = "Clients and resources",
                        ShowInMenu = true,
                        Properties = new Dictionary<string, object>
                        {
                            {
                                "listOptions",
                                new
                                {
                                    Url = $"{options.ApiUrl.TrimEnd('/')}/clients",
                                    Header = "Clients",
                                    ItemsUrl = "/clients/[id]",
                                    ItemKeys = new
                                    {
                                        Id = nameof(Client.ClientId).ToCamelCase(),
                                        Title = nameof(Client.ClientName).ToCamelCase(),
                                        Subtitle = nameof(Client.Description).ToCamelCase()
                                    }
                                }
                            }
                        }
                    },
                    new RouteContract
                    {
                        Name = "Client",
                        Path = "/clients/:itemId",
                        Type = RouteType.Editor,
                        Properties = new Dictionary<string, object>
                        {
                            {
                                "editorOptions",
                                new
                                {
                                    Url = $"{options.ApiUrl.TrimEnd('/')}/clients/[id]",
                                    DataType = "client",
                                    ItemKeys = new
                                    {
                                        Id = nameof(Client.ClientId).ToCamelCase(),
                                        Title = nameof(Client.ClientName).ToCamelCase(),
                                        Subtitle = nameof(Client.Description).ToCamelCase()
                                    },
                                    FieldGroups = BuildFieldsGroups(dataContracts["client"], new[]
                                    {
                                        new FieldGroup
                                        {
                                            Name = "General",
                                            PropertyNames = new[]
                                            {
                                                nameof(Client.ClientId).ToCamelCase(),
                                                nameof(Client.ClientName).ToCamelCase(),
                                                nameof(Client.Enabled).ToCamelCase()
                                            }
                                        },
                                        new FieldGroup
                                        {
                                            Name = "Urls",
                                            PropertyNames = new[]
                                            {
                                                nameof(Client.AllowedCorsOrigins).ToCamelCase(),
                                                nameof(Client.RedirectUris).ToCamelCase(),
                                                nameof(Client.PostLogoutRedirectUris).ToCamelCase()
                                            }
                                        },
                                        new FieldGroup
                                        {
                                            Name = "Secrets",
                                            PropertyNames = new[]
                                            {
                                                nameof(Client.RequireClientSecret).ToCamelCase(),
                                                nameof(Client.ClientSecrets).ToCamelCase()
                                            }
                                        },
                                        new FieldGroup
                                        {
                                            Name = "Claims",
                                            PropertyNames = new[]
                                            {
                                                nameof(Client.AlwaysIncludeUserClaimsInIdToken).ToCamelCase(),
                                                nameof(Client.AlwaysSendClientClaims).ToCamelCase(),
                                                nameof(Client.ClientClaimsPrefix).ToCamelCase(),
                                                nameof(Client.UpdateAccessTokenClaimsOnRefresh).ToCamelCase(),
                                                nameof(Client.Claims).ToCamelCase()
                                            }
                                        },
                                        new FieldGroup
                                        {
                                            Name = "Properties",
                                            PropertyNames = new[]
                                            {
                                                nameof(Client.Properties).ToCamelCase()
                                            }
                                        }
                                    }, new[]
                                    {
                                        new FieldGroup.Field
                                        {
                                            PropertyName = nameof(Client.ProtocolType).ToCamelCase(),
                                            Name = nameof(Client.ProtocolType).FromCamelCase(),
                                            Type = "select",
                                            MetadataName = "protocolTypes"
                                        },
                                        new FieldGroup.Field
                                        {
                                            PropertyName = nameof(Client.RefreshTokenExpiration).ToCamelCase(),
                                            Name = nameof(Client.RefreshTokenExpiration).FromCamelCase(),
                                            Type = "select",
                                            MetadataName = "tokenExpirationTypes"
                                        },
                                        new FieldGroup.Field
                                        {
                                            PropertyName = nameof(Client.AccessTokenType).ToCamelCase(),
                                            Name = nameof(Client.AccessTokenType).FromCamelCase(),
                                            Type = "select",
                                            MetadataName = "accessTokenTypes"
                                        },
                                        new FieldGroup.Field
                                        {
                                            PropertyName = nameof(Client.RefreshTokenUsage).ToCamelCase(),
                                            Name = nameof(Client.RefreshTokenUsage).FromCamelCase(),
                                            Type = "select",
                                            MetadataName = "tokenUsageTypes"
                                        },
                                        //new FieldGroup.Field
                                        //{
                                        //    PropertyName = nameof(Client.ClientSecrets).ToCamelCase(),
                                        //    Name = nameof(Client.ClientSecrets).FromCamelCase(),
                                        //    Type = "special-clientSecret"
                                        //}
                                    }),
                                    Definitions = dataContracts["client"]?.Definitions?.ToDictionary(d => d.Key, d => BuildFieldsGroups(d.Value))
                                }
                            }
                        }
                    },
                    new RouteContract
                    {
                        Name = "Identity resources",
                        Path = "/identity-resources",
                        Type = RouteType.List,
                        MenuGroup = "Clients and resources",
                        ShowInMenu = true,
                        Properties = new Dictionary<string, object>
                        {
                            {
                                "listOptions",
                                new
                                {
                                    Url = $"{options.ApiUrl.TrimEnd('/')}/identity-resources",
                                    Header = "Identity resources",
                                    ItemsUrl = "/identity-resources/[id]",
                                    ItemKeys = new
                                    {
                                        Id = "name",
                                        Title = "displayName",
                                        Subtitle = "description"
                                    }
                                }
                            }
                        }
                    },
                    new RouteContract
                    {
                        Name = "IdentityResource",
                        Path = "/identity-resources/:itemId",
                        Type = RouteType.Editor,
                        Properties = new Dictionary<string, object>
                        {
                            {
                                "editorOptions",
                                new
                                {
                                    Url = $"{options.ApiUrl.TrimEnd('/')}/identity-resources/[id]",
                                    DataType = "identityResource",
                                    ItemKeys = new
                                    {
                                        Id = "name",
                                        Title = "displayName",
                                        Subtitle = "description"
                                    },
                                    FieldGroups = BuildFieldsGroups(dataContracts["identityResource"]),
                                    Definitions = dataContracts["identityResource"]?.Definitions?.ToDictionary(d => d.Key, d => BuildFieldsGroups(d.Value))
                                }
                            }
                        }
                    },
                    new RouteContract
                    {
                        Name = "Api resources",
                        Path = "/api-resources",
                        Type = RouteType.List,
                        MenuGroup = "Clients and resources",
                        ShowInMenu = true,
                        Properties = new Dictionary<string, object>
                        {
                            {
                                "listOptions",
                                new
                                {
                                    Url = $"{options.ApiUrl.TrimEnd('/')}/api-resources",
                                    Header = "Api resources",
                                    ItemsUrl = "/api-resources/[id]",
                                    ItemKeys = new
                                    {
                                        Id = "name",
                                        Title = "displayName",
                                        Subtitle = "description"
                                    }
                                }
                            }
                        }
                    },
                    new RouteContract
                    {
                        Name = "ApiResource",
                        Path = "/api-resources/:itemId",
                        Type = RouteType.Editor,
                        Properties = new Dictionary<string, object>
                        {
                            {
                                "editorOptions",
                                new
                                {
                                    Url = $"{options.ApiUrl.TrimEnd('/')}/api-resources/[id]",
                                    DataType = "apiResource",
                                    ItemKeys = new
                                    {
                                        Id = "name",
                                        Title = "displayName",
                                        Subtitle = "description"
                                    },
                                    FieldGroups = BuildFieldsGroups(dataContracts["apiResource"]),
                                    Definitions = dataContracts["apiResource"]?.Definitions?.ToDictionary(d => d.Key, d => BuildFieldsGroups(d.Value))
                                }
                            }
                        }
                    }
                });
            }
        }

        public IEnumerable<FieldGroup.Field> GetFields(Dictionary<string, DataContractDefinition.Property> properties,
            IEnumerable<FieldGroup.Field> customFields = null, string[] requiredFields = null)
        {
            properties = properties ?? new Dictionary<string, DataContractDefinition.Property>();
            customFields = customFields ?? new List<FieldGroup.Field>();
            requiredFields = requiredFields ?? new string[0];

            foreach (var clientDefinitionProperty in properties)
            {
                var field = customFields.FirstOrDefault(f => f.PropertyName == clientDefinitionProperty.Key);
                if (field != null)
                {
                    field.Description = field.Description ?? clientDefinitionProperty.Value?.Description;
                    yield return field;
                    continue;
                }

                var prop = clientDefinitionProperty.Value;
                var types = prop.Type is string
                    ? new[] {prop.Type?.ToString()}
                    : ((JArray) prop.Type)?.Select(t => (string) t).ToArray();

                types = types ?? new[] {"ref"};
                types = types.Where(t => t != "null").ToArray();

                var fieldType = $"{types[0]}";
                string fieldFormat = null;
                switch (types[0])
                {
                    case "boolean":
                        fieldType = "checkbox";
                        break;
                    case "integer":
                        fieldType = "text-input";
                        fieldFormat = "number";
                        break;
                    case "string":
                        fieldType = prop.Format == "date-time"
                            ? "datetime-picker"
                            : "text-input";
                        fieldFormat = "text";
                        break;
                    case "array":
                        var arrayType = prop.Items?.Reference ?? prop.Items?.Type[0];
                        var comboBoxTypes = new[] { "string", "integer" };
                        if (comboBoxTypes.Contains(arrayType))
                        {
                            fieldType = "combobox";
                            fieldFormat = arrayType;
                        }
                        else
                        {
                            fieldFormat = arrayType ?? "No arrayType";
                        }

                        break;
                    case "object":
                        var valueType = prop.Items?.Reference ?? prop.Items?.Type[0];
                        fieldType = "object";
                        fieldFormat = valueType;

                        break;
                }

                yield return new FieldGroup.Field
                {
                    Name = clientDefinitionProperty.Key?.FromCamelCase(),
                    PropertyName = clientDefinitionProperty.Key,
                    Type = fieldType,
                    Format = fieldFormat,
                    Required = requiredFields.Contains(clientDefinitionProperty.Key),
                    Description = clientDefinitionProperty.Value?.Description
                };
            }
        }

        public IEnumerable<object> BuildFieldsGroups(DataContractDefinition definition,
            IEnumerable<FieldGroup> fieldGroups = null, IEnumerable<FieldGroup.Field> customFields = null)
        {
            fieldGroups = fieldGroups ?? new FieldGroup[0];

            var returnedProperties = new List<string>();
            foreach (var fieldGroup in fieldGroups)
            {
                var propertyNames = fieldGroup.PropertyNames.ToList();
                returnedProperties.AddRange(fieldGroup.PropertyNames);
                yield return new
                {
                    fieldGroup.Name,
                    Fields = GetFields(definition.Properties
                        .OrderBy(p =>
                            propertyNames.FindIndex(s => s.Equals(p.Key, StringComparison.InvariantCultureIgnoreCase)))
                        .Where(p => fieldGroup.PropertyNames.Contains(p.Key))
                        .ToDictionary(d => d.Key, d => d.Value), customFields, null)
                };
            }

            yield return new
            {
                Name = "Miscellaneous",
                Fields = GetFields(definition.Properties?.Where(p => !returnedProperties.Contains(p.Key))
                    .ToDictionary(d => d.Key, d => d.Value), customFields, null)
            };
        }

        public class FieldGroup
        {
            public string Name { get; set; }
            public IEnumerable<string> PropertyNames { get; set; }

            public class Field
            {
                public string Name { get; set; }
                public string PropertyName { get; set; }
                public string Type { get; set; }
                public string Format { get; set; }
                public string MetadataName { get; set; }
                public bool Required { get; set; }
                public string Description { get; set; }
            }
        }

        public class DataContractDefinition
        {
            public Dictionary<string, DataContractDefinition> Definitions { get; set; }
            public object Type { get; set; }
            public Dictionary<string, Property> Properties { get; set; }
            public string[] Required { get; set; }
            public string Description { get; set; }

            [JsonProperty("x-enumNames")] public string[] EnumNames { get; set; }
            [JsonProperty("enum")] public int[] EnumValue { get; set; }

            public class Property
            {
                public object Type { get; set; }
                public PropertyItems Items { get; set; }
                public string Format { get; set; }
                public PropertyItems AdditionalProperties { get; set; }
                public string Description { get; set; }

                public class PropertyItems
                {
                    [JsonProperty("$ref", IsReference = true)]
                    public string Reference { get; set; }

                    //[JsonProperty("type", ItemConverterType = typeof(StringToArrayJsonConverter))]
                    [JsonConverter(typeof(StringToArrayJsonConverter))]
                    public string[] Type { get; set; }

                    [JsonProperty("enum")] public int[] EnumValue { get; set; }
                }
            }
        }

        public class StringToArrayJsonConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {   
                if (reader.Value is string s)
                    return new[] {s};
                else
                    return reader.Value;
            }

            public override bool CanConvert(Type objectType)
            {
                throw new NotImplementedException();
            }
        }

        //public class JsonDataContractProperties
        //{
        //    public Issuer issuer { get; set; }
        //    public Originalissuer originalIssuer { get; set; }
        //    public Properties1 properties { get; set; }
        //    public Subject subject { get; set; }
        //    public string[] type { get; set; }
        //    public Value value { get; set; }
        //    public Valuetype valueType { get; set; }
        //}
    }
}