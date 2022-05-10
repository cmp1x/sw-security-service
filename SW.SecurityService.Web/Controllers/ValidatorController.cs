namespace SW.SecurityService.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SW.SecurityService.Core.Services;
    using SW.SecurityService.Web.Models;
    using System;

    [ApiController]
    [Route("[controller]")]
    public class ValidatorController : ControllerBase
    {
        private readonly ITokenService tokenService;

        public ValidatorController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Credentials credentials)
        {
            // Should be rework to db password check
            if (credentials.Password != null)
            {
                var token = Guid.NewGuid().ToString();
                this.tokenService.Set(
                    token,
                    credentials.User);

                return Ok(token);
            }

            return BadRequest();
        }
    }
}
