using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IdentityServer4.OpenAdmin.BlazorClient.Helpers
{
    public class JsonPatchHelper
    {
        public static JsonPatchDocument GenerateJsonPatchDocument<TModel>(TModel original, TModel modified)
        {
            var originalObject = JObject.FromObject(original);
            var modifiedObject = JObject.FromObject(modified);

            var patch = new JsonPatchDocument();
            FillPatchForObject(originalObject, modifiedObject, patch, "/");

            return patch;
        }

        private static void FillPatchForObject(JObject orig, JObject mod, JsonPatchDocument patch, string path)
        {
            var origNames = orig.Properties().Select(x => x.Name).ToArray();
            var modNames = mod.Properties().Select(x => x.Name).ToArray();

            // Names removed in modified
            foreach (var k in origNames.Except(modNames))
            {
                var prop = orig.Property(k);
                patch.Remove(path + prop.Name);
            }

            // Names added in modified
            foreach (var k in modNames.Except(origNames))
            {
                var prop = mod.Property(k);
                patch.Add(path + prop.Name, prop.Value);
            }

            // Present in both
            foreach (var k in origNames.Intersect(modNames))
            {
                var origProp = orig.Property(k);
                var modProp = mod.Property(k);

                if (origProp.Value.Type != modProp.Value.Type)
                {
                    patch.Replace(path + modProp.Name, modProp.Value);
                }
                else if (!string.Equals(
                    origProp.Value.ToString(Formatting.None),
                    modProp.Value.ToString(Formatting.None)))
                {
                    if (origProp.Value.Type == JTokenType.Object)
                    {
                        // Recurse into objects
                        FillPatchForObject(origProp.Value as JObject, modProp.Value as JObject, patch,
                            path + modProp.Name + "/");
                    }
                    else
                    {
                        // Replace values directly
                        patch.Replace(path + modProp.Name, modProp.Value);
                    }
                }
            }
        }
    }
}