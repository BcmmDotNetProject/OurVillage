namespace UserServer.Models.UserInformation
{
    /// <summary>
    /// 用户登录信息。
    /// </summary>
    /// <param name="LoginStatus">登录状态。</param>
    /// <param name="Token">令牌。</param>
    /// <param name="Message">登录信息。。</param>
    public record LoginInformation(bool LoginStatus, string? Token, string Message);
}
