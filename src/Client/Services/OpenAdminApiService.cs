using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IdentityServer4.OpenAdmin.BlazorClient.Services
{
    public class OpenAdminApiService
    {
        private readonly string apiUrl;
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IOptionsMonitor<OpenAdminBlazorClientOptions> openAdminBlazorClientOptions;

        public OpenAdminApiService(IHttpContextAccessor httpContextAccessor, IOptionsMonitor<OpenAdminBlazorClientOptions> openAdminBlazorClientOptions)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.openAdminBlazorClientOptions = openAdminBlazorClientOptions;

            var optionsApiUrl = openAdminBlazorClientOptions.CurrentValue.ApiUrl;

            var request = httpContextAccessor.HttpContext.Request;
            apiUrl = optionsApiUrl.StartsWith("/")
                ? $"{request.Scheme}://{request.Host.Value.Trim('/')}/{optionsApiUrl.Trim('/')}"
                : optionsApiUrl.Trim('/');

            httpClient = new HttpClient();
        }

        public async Task<TModel> Get<TModel>(string endpoint)
        {
            var data = await httpClient.GetStringAsync($"{apiUrl}/{endpoint.TrimStart('/')}");
            return JsonConvert.DeserializeObject<TModel>(data);
        }

        public async Task<TModel> Patch<TModel>(string endpoint, JsonPatchDocument jsonPatchDocument)
        {
            var content = new StringContent(JsonConvert.SerializeObject(jsonPatchDocument), Encoding.UTF8, "application/json");
            var response = await httpClient.PatchAsync($"{apiUrl}/{endpoint.TrimStart('/')}", content);
            return JsonConvert.DeserializeObject<TModel>(await response.Content.ReadAsStringAsync());
        }
    }
}