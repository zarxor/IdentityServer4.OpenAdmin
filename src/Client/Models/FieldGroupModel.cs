using System.Collections.Generic;

namespace IdentityServer4.OpenAdmin.BlazorClient.Models
{
    public class FieldGroupModel
    {
        public string Name { get; set; }
        public IEnumerable<string> PropertyNames { get; set; }

        public class FieldsContainer
        {
            public string Name { get; set; }
            public IEnumerable<Field> Fields { get; set; }
        }

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
}