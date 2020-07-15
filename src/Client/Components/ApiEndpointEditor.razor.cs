using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyClone;
using IdentityServer4.OpenAdmin.API.Shared.Contracts;
using IdentityServer4.OpenAdmin.BlazorClient.Helpers;
using IdentityServer4.OpenAdmin.BlazorClient.Models;
using IdentityServer4.OpenAdmin.BlazorClient.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using Tabler;

namespace IdentityServer4.OpenAdmin.BlazorClient.Components
{
    public partial class ApiEndpointEditor<TContractModel> : ComponentBase
    {
        private TContractModel contract;
        private TContractModel contractOriginal;

        private List<FieldGroupModel.FieldsContainer> fieldsContainers;

        [Parameter] public Func<TContractModel, string> Header { get; set; } = (model => null); 

        [Parameter] public Func<TContractModel, string> Description { get; set; } = (model => null);

        [Parameter] public string Id { get; set; }

        [Parameter] public string EndpointName { get; set; }

        [Parameter] public string DefinitionName { get; set; }

        [Inject] private OpenAdminApiService OpenAdminApiService { get; set; }

        [Inject] private TablerToastService TablerToastService { get; set; }

        private JsonPatchDocument JsonPatchDocument =>
            JsonPatchHelper.GenerateJsonPatchDocument(contractOriginal, contract);

        protected override async Task OnParametersSetAsync()
        {
            if (fieldsContainers == null)
            {
                var definitions =
                    await OpenAdminApiService
                        .Get<Dictionary<string, ContractDefinitionContract>>("/metadata/contracts");
                fieldsContainers = FieldGroupHelper.BuildFieldsGroups(definitions[DefinitionName]);
            }

            contract = await OpenAdminApiService.Get<TContractModel>($"{EndpointName}/{Id}");
            contractOriginal = contract.Clone();

            await base.OnParametersSetAsync();
        }

        private async Task Save()
        {
            if (JsonPatchDocument.Operations.Any())
            {
                contract = await OpenAdminApiService.Patch<TContractModel>($"{EndpointName}/{Id}", JsonPatchDocument);
                contractOriginal = contract.Clone();
                TablerToastService.Toasts.Add(new TablerToastService.Toast
                {
                    Delay = 3,
                    Title = "Save successful"
                });
                //NotificationService.Notify(NotificationSeverity.Success, "Save successful");
            }
        }
    }
}