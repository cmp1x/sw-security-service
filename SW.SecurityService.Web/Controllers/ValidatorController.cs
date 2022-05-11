namespace SW.SecurityService.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SW.SecurityService.Core.Providers;
    using SW.SecurityService.Core.Services;
    using SW.SecurityService.Web.Models;
    using System;

    [ApiController]
    [Route("[controller]")]
    public class ValidatorController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly ITokenProvider tokenProvider; 

        public ValidatorController(ITokenService tokenService, ITokenProvider tokenProvider)
        {
            this.tokenService = tokenService;
            this.tokenProvider = tokenProvider;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Credentials credentials)
        {
            // Should be rework to db password check
            if (credentials.Password != null)
            {
                var token = this.tokenProvider.NewGuidInString();
                this.tokenService.Set(
                    token,
                    credentials.User);

                return Ok(token);
            }

            return BadRequest();
        }
    }
}
