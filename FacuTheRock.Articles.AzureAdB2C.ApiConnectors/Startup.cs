using FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Infrastructure;
using FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

[assembly: FunctionsStartup(typeof(FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Startup))]

namespace FacuTheRock.Articles.AzureAdB2C.ApiConnectors
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<B2CSettings>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(B2CSettings.B2CSettingsName).Bind(settings);
                });

            builder.Services.AddSingleton(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<B2CSettings>>();
                return Create(settings.Value);
            });

            builder.Services.AddSingleton<IUsersService, UsersService>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();

            builder.ConfigurationBuilder
               .SetBasePath(context.ApplicationRootPath)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
               .AddUserSecrets<Startup>(optional: true, reloadOnChange: false)
               .AddEnvironmentVariables();
        }

        private IGraphServiceClient Create(B2CSettings settings)
        {
            var confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(settings.ApplicationId)
                .WithTenantId(settings.TenantId)
                .WithClientSecret(settings.ClientSecret)
                .Build();

            var authProvider = new ClientCredentialProvider(confidentialClientApplication);

            return new GraphServiceClient(authProvider);
        }
    }
}
