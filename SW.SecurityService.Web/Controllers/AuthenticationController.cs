namespace SW.SecurityService.Web.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using SW.SecurityService.Core.Enums;
    using SW.SecurityService.Core.Services;
    using SW.SecurityService.Web.Models;
    
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(
            IAuthenticationService authenicationService)
        {
            this.authenticationService = authenicationService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Credentials credentials)
        {
            var authenticatedUser = this.authenticationService
                .AuthenticateUser(credentials.UserName, credentials.Password);

            if (authenticatedUser.Status == AuthenticationAnswerStatus.WrongPassword)
            {
                return this.Unauthorized($"Password is wrong");
            }

            if (authenticatedUser.Status == AuthenticationAnswerStatus.UserNotExist)
            {
                return this.Unauthorized($"There are no credentials of '{credentials.UserName}' in the database");
            }

            return this.Ok(authenticatedUser.userRedis);
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout([FromBody] string token)
        {
            throw new NotImplementedException("We need to implement logout functionality");
        }
    }
}
