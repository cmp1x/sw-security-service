namespace SW.SecurityService.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SW.SecurityService.Core.Services;
    using SW.SecurityService.Web.Models;
    using System;

    [ApiController]
    [Route("[controller]")]
    public class AuthenticatorController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticatorController(
            IAuthenticationService authenicationService)
        {
            this.authenticationService = authenicationService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Credentials credentials)
        {
            try
            {
                var authenticatedUser = this.authenticationService
                    .AuthenticateUser(credentials.UserName, credentials.Password);

                return this.Ok(authenticatedUser);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
