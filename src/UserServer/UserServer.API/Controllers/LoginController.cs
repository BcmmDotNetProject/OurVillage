using Microsoft.AspNetCore.Mvc;
using UserServer.API.Common;
using UserServer.Models.UserInformation;

namespace UserServer.API.Controllers
{
    /// <summary>
    /// ��¼��������
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// ��־��¼����
        /// </summary>
        private readonly ILogger<LoginController> _logger;

        /// <summary>
        /// �����ṩ������
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ����һ����¼������ʵ����
        /// </summary>
        /// <param name="logger">��־����</param>
        /// <param name="serviceProvider">�����ṩ�ߡ�</param>
        public LoginController(ILogger<LoginController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// �û���¼��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public LoginInformation Login(string userName, string password)
        {
            if (!CheckUserInfo(userName, password))
            {
                return new LoginInformation(false, null, "�û��������벻��ȷ��");
            }

            var jwtToken = _serviceProvider.GetService<IJwtToken>();

            if (jwtToken is null)
            {
                return new LoginInformation(false, null, "��¼���󣬻�ȡ����ʧ�ܡ�");
            }

            try
            {
                var token = jwtToken.GetToken();
                return new LoginInformation(true, token, "��¼�ɹ���");
            }
            catch (Exception e)
            {
                return new LoginInformation(false, null, e.ToString());
            }
        }

        /// <summary>
        /// У���û���Ϣ��
        /// </summary>
        /// <param name="userName">�û�����</param>
        /// <param name="password">�û����롣</param>
        /// <returns>У��ɹ����� true��ʧ�ܷ��� false ��</returns>
        private bool CheckUserInfo(string userName, string password)
        {
            return userName.Equals("lanxiaofang") && password.Equals("94263264");
        }
    }
}