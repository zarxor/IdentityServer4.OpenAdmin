using System.Collections.Generic;
using IdentityServer4.OpenAdmin.API.Shared.Converters;
using Newtonsoft.Json;

namespace IdentityServer4.OpenAdmin.API.Shared.Contracts
{
    public class ContractDefinitionContract
    {
        public Dictionary<string, ContractDefinitionContract> Definitions { get; set; }
        public object Type { get; set; }
        public Dictionary<string, Property> Properties { get; set; }
        public string[] Required { get; set; }
        public string Description { get; set; }

        [JsonProperty("x-enumNames")] public string[] EnumNames { get; set; }
        [JsonProperty("enum")] public int[] EnumValue { get; set; }

        public class Property
        {
            //[JsonConverter(typeof(StringToArrayJsonConverter))]
            public object Type { get; set; }
            public PropertyItems Items { get; set; }
            public string Format { get; set; }
            public PropertyItems AdditionalProperties { get; set; }
            public string Description { get; set; }

            public class PropertyItems
            {
                [JsonProperty("$ref", IsReference = true)]
                public string Reference { get; set; }

                [JsonConverter(typeof(StringToArrayJsonConverter))]
                public string[] Type { get; set; }

                [JsonProperty("enum")] public int[] EnumValue { get; set; }
            }
        }
    }
}