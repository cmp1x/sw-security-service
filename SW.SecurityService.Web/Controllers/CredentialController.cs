namespace SW.SecurityService.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using SW.SecurityService.CredentialRepository.Models;
    using SW.SecurityService.CredentialRepository.Repository;
    using SW.SecurityService.Web.Models;

    [ApiController]
    [Route("[controller]")]
    public class CredentialController : ControllerBase
    {
        private readonly ICredentialRepository credentialRepository;
        private readonly IMapper mapper;

        public CredentialController(
            ICredentialRepository credentialRepository,
            IMapper mapper)
        {
            this.credentialRepository = credentialRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Credentials credential)
        {
            var targetCredential = credential;
            var mappedCredentialDb = this.mapper.Map<CredentialsDb>(targetCredential);
            this.credentialRepository.Create(mappedCredentialDb);

            var postedUser = this.credentialRepository.GetCredential(credential.User);
            var mappedPostedCredential = this.mapper.Map<Credentials>(postedUser);

            return Ok($"User {mappedPostedCredential.User} posted");
        }
    }
}
