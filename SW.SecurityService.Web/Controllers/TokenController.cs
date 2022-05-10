namespace SW.SecurityService.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SW.SecurityService.Core.Services;

    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {

        private readonly ITokenService tokenService;

        public TokenController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpGet]
        [Route("{token}")]
        public ActionResult Get(string token)
        {
            var User = this.tokenService.Get(token);
            if (User is null)
            {
                return NotFound();
            }
            return Ok(User);
        }
    }
}
