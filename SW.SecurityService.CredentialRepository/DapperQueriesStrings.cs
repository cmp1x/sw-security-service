namespace SW.SecurityService.CredentialRepository
{
    public static class DapperQueriesStrings
    {
        public static string GetCredential { get; } = "SELECT UserId, UserName, Password FROM UserCredential WHERE UserName = @UserName";

        public static string CreateCredential { get; } = "INSERT INTO UserCredential (UserId, UserName, Password) VALUES (@UserId, @UserName, @Password)";

    }
}
