using System.Threading.Tasks;

namespace FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Services
{
    public interface IUsersService
    {
        Task<bool> ExistsUserWithEmailAsync(string email);
    }
}
