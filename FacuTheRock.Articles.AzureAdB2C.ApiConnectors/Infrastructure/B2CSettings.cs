namespace FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Infrastructure
{
    public class B2CSettings
    {
        public const string B2CSettingsName = "Values:B2CSettings";

        public string TenantId { get; set; }
        public string ApplicationId { get; set; }
        public string ClientSecret { get; set; }
    }
}
