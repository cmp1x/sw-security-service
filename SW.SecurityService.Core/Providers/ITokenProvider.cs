namespace SW.SecurityService.Core.Providers
{
    public interface ITokenProvider
    {
        string GetNewToken();
    }
}