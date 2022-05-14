namespace SW.SecurityService.Web
{
    using AutoMapper;
    using SW.SecurityService.CredentialRepository.Models;
    using SW.SecurityService.Web.Models;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Credentials, CredentialsDb>().ReverseMap();
            
            CreateMap<CredentialsDb, Credentials>().ReverseMap();
        }
    }
}
