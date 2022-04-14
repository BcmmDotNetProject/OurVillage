namespace UserServer.Models.UserInformation
{
    /// <summary>
    /// 用户模型。
    /// </summary>
    /// <param name="Id">用户 ID 。</param>
    /// <param name="Name">用户名。</param>
    public record User(string Id, string Name);
}
