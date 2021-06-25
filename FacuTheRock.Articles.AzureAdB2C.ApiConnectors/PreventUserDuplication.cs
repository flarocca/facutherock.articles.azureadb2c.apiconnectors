using FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Requests;
using FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Responses;
using FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FacuTheRock.Articles.AzureAdB2C.ApiConnectors
{
    public class PreventUserDuplication
    {
        private readonly IUsersService _usersService;

        public PreventUserDuplication(IUsersService usersService) =>
            _usersService = usersService;

        [FunctionName("PreventUserDuplication")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] RequestContent req,
            ILogger log)
        {
            if (string.IsNullOrEmpty(req.Email) || !req.Email.Contains("@"))
            {
                return new BadRequestObjectResult(new ShowBlockPageResponseContent("Email name is mandatory."));
            }

            try
            {
                var exists = await _usersService.ExistsUserWithEmailAsync(req.Email);

                if (exists)
                {
                    return new OkObjectResult(new ShowBlockPageResponseContent("An user with this email already exists."));
                }
            }
            catch (Exception e)
            {
                log.LogError("Error executing MS Graph request: ", e);
                return new OkObjectResult(new ShowBlockPageResponseContent("There was a problem with your request."));
            }

            return new OkObjectResult(new ContinueResponseContent());
        }
    }
}
