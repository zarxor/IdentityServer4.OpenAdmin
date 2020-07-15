using System.Threading.Tasks;
using IdentityServer4.OpenAdmin.BlazorClient.Models;
using IdentityServer4.OpenAdmin.BlazorClient.Services;
using Microsoft.AspNetCore.Components;
using Tabler;
using TypeSupport.Extensions;

namespace IdentityServer4.OpenAdmin.BlazorClient.Components
{
    public partial class FieldInput<TContractModel> : ComponentBase
    {
        private TContractModel contract;

        [Parameter]
        public TContractModel Contract
        {
            get => contract;
            set
            {
                contract = value;
                ContractChanged.InvokeAsync(value);
            }
        }

        [Parameter] public EventCallback<TContractModel> ContractChanged { get; set; }

        [Parameter] public FieldGroupModel.Field Field { get; set; }

        [Inject] private OpenAdminApiService OpenAdminApiService { get; set; }

        [Inject] private TablerToastService TablerToastService { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }

        protected TValue GetValue<TValue>()
        {
            return Contract.GetPropertyValue<TValue>(Field.PropertyName);
        }

        protected void SetValue<TValue>(TValue value)
        {
            Contract.SetPropertyValue(Field.PropertyName, value);
            ContractChanged.InvokeAsync(Contract);
        }
    }
}