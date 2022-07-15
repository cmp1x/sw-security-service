﻿namespace SW.SecurityService.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SW.SecurityService.Core.Enums;
    using SW.SecurityService.Core.Services;
    using SW.SecurityService.Web.Models;

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
    }
}
