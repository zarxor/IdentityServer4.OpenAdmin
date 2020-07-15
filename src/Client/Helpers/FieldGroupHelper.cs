using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4.OpenAdmin.API.Shared.Contracts;
using IdentityServer4.OpenAdmin.BlazorClient.Extensions;
using IdentityServer4.OpenAdmin.BlazorClient.Models;
using Newtonsoft.Json.Linq;

namespace IdentityServer4.OpenAdmin.BlazorClient
{
    public class FieldGroupHelper
    {
        public static List<FieldGroupModel.Field> GetFields(
            Dictionary<string, ContractDefinitionContract.Property> properties,
            IEnumerable<FieldGroupModel.Field> customFields = null, string[] requiredFields = null)
        {
            properties ??= new Dictionary<string, ContractDefinitionContract.Property>();
            customFields ??= new List<FieldGroupModel.Field>();
            requiredFields ??= new string[0];

            var fields = new List<FieldGroupModel.Field>();

            foreach (var clientDefinitionProperty in properties)
            {
                var field = customFields.FirstOrDefault(f => f.PropertyName == clientDefinitionProperty.Key);
                if (field != null)
                {
                    field.Description ??= clientDefinitionProperty.Value?.Description;
                    fields.Add(field);
                    continue;
                }

                var prop = clientDefinitionProperty.Value;
                if (prop is null)
                    continue;

                var types = prop.Type is string
                    ? new[] { prop.Type?.ToString() }
                    : ((JArray)prop.Type)?.Select(t => (string)t).ToArray();

                types ??= new[] {"ref"};
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
                        var comboBoxTypes = new[] {"string", "integer"};
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

                fields.Add(new FieldGroupModel.Field
                {
                    Name = clientDefinitionProperty.Key?.FromCamelCase(),
                    PropertyName = clientDefinitionProperty.Key,
                    Type = fieldType,
                    Format = fieldFormat,
                    Required = requiredFields.Contains(clientDefinitionProperty.Key),
                    Description = clientDefinitionProperty.Value?.Description
                });
            }

            return fields;
        }

        public static List<FieldGroupModel.FieldsContainer> BuildFieldsGroups(ContractDefinitionContract definition,
            IEnumerable<FieldGroupModel> fieldGroups = null, IEnumerable<FieldGroupModel.Field> customFields = null)
        {
            var outputs = new List<FieldGroupModel.FieldsContainer>();

            fieldGroups ??= new FieldGroupModel[0];

            var returnedProperties = new List<string>();
            foreach (var fieldGroup in fieldGroups)
            {
                var propertyNames = fieldGroup.PropertyNames.ToList();
                returnedProperties.AddRange(fieldGroup.PropertyNames);
                outputs.Add(new FieldGroupModel.FieldsContainer
                {
                    Name = fieldGroup.Name,
                    Fields = GetFields(definition.Properties
                        .OrderBy(p =>
                            propertyNames.FindIndex(s => s.Equals(p.Key, StringComparison.InvariantCultureIgnoreCase)))
                        .Where(p => fieldGroup.PropertyNames.Contains(p.Key))
                        .ToDictionary(d => d.Key, d => d.Value), customFields)
                });
            }

            outputs.Add(new FieldGroupModel.FieldsContainer
            {
                Name = "Miscellaneous",
                Fields = GetFields(definition.Properties?.Where(p => !returnedProperties.Contains(p.Key))
                    .ToDictionary(d => d.Key, d => d.Value), customFields)
            });

            return outputs;
        }
    }
}