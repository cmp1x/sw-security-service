namespace SW.SecurityService.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SW.SecurityService.Core.Services;
    using SW.SecurityService.Web.Models;

    [ApiController]
    [Route("[controller]")]
    public class ValidatorController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public ValidatorController(
            IAuthenticationService authenicationService)
        {
            this.authenticationService = authenicationService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Credentials credentials)
        {
            var authenticatedUser = this.authenticationService
                .AuthenticateUser(credentials.User, credentials.Password);

            if(authenticatedUser is null)
            {
                return this.BadRequest();
            }

            return this.Ok(authenticatedUser);
        }
    }
}
