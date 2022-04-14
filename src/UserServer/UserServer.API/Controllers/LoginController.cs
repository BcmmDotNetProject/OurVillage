using Microsoft.AspNetCore.Mvc;
using UserServer.API.Common;
using UserServer.Models.UserInformation;

namespace UserServer.API.Controllers
{
    /// <summary>
    /// 登录控制器。
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// 日志记录器。
        /// </summary>
        private readonly ILogger<LoginController> _logger;

        /// <summary>
        /// 服务提供容器。
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 生成一个登录控制器实例。
        /// </summary>
        /// <param name="logger">日志器。</param>
        /// <param name="serviceProvider">服务提供者。</param>
        public LoginController(ILogger<LoginController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 用户登录。
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public LoginInformation Login(string userName, string password)
        {
            if (!CheckUserInfo(userName, password))
            {
                return new LoginInformation(false, null, "用户名或密码不正确。");
            }

            var jwtToken = _serviceProvider.GetService<IJwtToken>();

            if (jwtToken is null)
            {
                return new LoginInformation(false, null, "登录错误，获取令牌失败。");
            }

            try
            {
                var token = jwtToken.GetToken();
                return new LoginInformation(true, token, "登录成功！");
            }
            catch (Exception e)
            {
                return new LoginInformation(false, null, e.ToString());
            }
        }

        /// <summary>
        /// 校验用户信息。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <param name="password">用户密码。</param>
        /// <returns>校验成功返回 true，失败返回 false 。</returns>
        private bool CheckUserInfo(string userName, string password)
        {
            return userName.Equals("lanxiaofang") && password.Equals("94263264");
        }
    }
}