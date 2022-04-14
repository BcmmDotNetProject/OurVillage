namespace UserServer.API.Common;

/// <summary>
/// JWT Token 接口。
/// </summary>
internal interface IJwtToken
{
    /// <summary>
    /// 获取 Token。
    /// </summary>
    /// <returns>返回一个有效的Token。</returns>
    string GetToken();
}