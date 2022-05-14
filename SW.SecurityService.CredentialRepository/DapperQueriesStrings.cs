namespace SW.SecurityService.CredentialRepository
{
    public static class DapperQueriesStrings
    {
        public static string GetCredential { get; } = "SELECT [User], [Password] FROM [SWCredentials].[dbo].[UserCredential] WHERE [User] = @User";

        public static string CreateCredential { get; } = "INSERT INTO [SWCredentials].[dbo].[UserCredential] ([User], [Password]) VALUES (@User, @Password)";

    }
}
