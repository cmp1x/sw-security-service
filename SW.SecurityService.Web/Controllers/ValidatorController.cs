namespace SW.SecurityService.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SW.SecurityService.Core.Providers;
    using SW.SecurityService.Core.Services;
    using SW.SecurityService.Web.Models;

    [ApiController]
    [Route("[controller]")]
    public class ValidatorController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly ITokenProvider tokenProvider;
        private readonly IAuthenticationService authenticationService;

        public ValidatorController(
            ITokenService tokenService,
            ITokenProvider tokenProvider,
            IAuthenticationService authenicationService)
        {
            this.tokenService = tokenService;
            this.tokenProvider = tokenProvider;
            this.authenticationService = authenicationService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Credentials credentials)
        {
            if (this.authenticationService.IsProperPassword(credentials.User, credentials.Password))
            {
                var token = this.tokenProvider.GetNewToken();
                this.tokenService.Set(
                    token,
                    credentials.User);

                return Ok(token);
            }

            return BadRequest();
        }
    }
}
