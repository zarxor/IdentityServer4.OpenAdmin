namespace IdentityServer4.OpenAdmin.BlazorClient
{
    public class OpenAdminBlazorClientOptions
    {
        public bool AddServerSideBlazor { get; set; } = true;
        public bool AddRazorPages { get; set; } = true;

        public const string DefaultApiUrl = "/admin/api/";
        private string apiUrl;

        /// <summary>
        /// Default: /oa-api/
        /// </summary>
        public string ApiUrl
        {
            get => apiUrl ?? DefaultApiUrl;
            set => apiUrl = value;
        }
    }
}