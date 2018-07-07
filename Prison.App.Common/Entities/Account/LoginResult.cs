namespace Prison.App.Common.Entities.Account
{
    public enum LoginResult:byte
    {
        Success = 0,
        InvalidPassword = 1,
        UserNotFound=2,
        Failure = 3,
    }
}
