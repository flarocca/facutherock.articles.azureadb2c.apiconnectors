using FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System.Linq;
using System.Threading.Tasks;

namespace FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Services
{
    public class UsersService : IUsersService
    {
        private readonly IGraphServiceClient _graphClient;
        private readonly B2CSettings _settings;

        public UsersService(
            IGraphServiceClient graphClient,
            IOptions<B2CSettings> options)
        {
            _graphClient = graphClient;
            _settings = options.Value;
        }

        public async Task<bool> ExistsUserWithEmailAsync(string email)
        {
            var taskResult = await Task.WhenAll(
                GetUsersByIdentityAsync(email),
                GetUsersByEmailAsync(email));

            var result = taskResult
                .SelectMany(user => user);

            return result.Any();
        }

        private async Task<User[]> GetUsersByIdentityAsync(string email)
        {
            var result = await _graphClient.Users
                .Request()
                .Filter($"identities/any(c:c/issuerAssignedId eq '{email}' and c/issuer eq '{_settings.TenantId}')")
                .GetAsync();

            return result.ToArray();
        }

        private async Task<User[]> GetUsersByEmailAsync(string email)
        {
            var result = await _graphClient.Users
                .Request()
                .Filter($"otherMails/any(c:c eq '{email}') and UserType eq 'Member'")
                .GetAsync();

            return result.ToArray();
        }
    }
}
