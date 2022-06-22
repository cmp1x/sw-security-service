namespace SW.SecurityService.Core.Enums
{
    public enum AuthenticationAnswerStatus
    {
        Authorized = 1,
        WrongPassword = 2,
        UserNotExist = 3,
        SystemFault = 4
    }
}
