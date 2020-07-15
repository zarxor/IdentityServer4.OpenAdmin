using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityServer4.OpenAdmin.BlazorClient.Services;
using Microsoft.AspNetCore.Components;

namespace IdentityServer4.OpenAdmin.BlazorClient.Components
{
    public partial class ApiEndpointList<TContractModel> : ComponentBase
    {
        private List<TContractModel> contracts;

        [Parameter] public Func<TContractModel, string> ContractHeader { get; set; }

        [Parameter] public Func<TContractModel, string> ContractDescription { get; set; }

        [Parameter] public Func<TContractModel, string> ContractId { get; set; }

        [Parameter] public string EndpointName { get; set; }

        [Parameter] public string PagePath { get; set; }

        [Inject] private OpenAdminApiService OpenAdminApiService { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            contracts = await OpenAdminApiService.Get<List<TContractModel>>(EndpointName);

            await base.OnParametersSetAsync();
        }
    }
}