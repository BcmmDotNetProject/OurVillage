using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserServer.API.Common
{
    /// <summary>
    /// JWT 授权类型。
    /// </summary>
    internal class JwtToken : IJwtToken
    {
        /// <summary>
        /// 配置器。
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 创建一个 JWT Token 实例。
        /// </summary>
        /// <param name="configuration">配置器。</param>
        public JwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 获取 Token。
        /// </summary>
        /// <returns>返回一个 Token 字符串。</returns>
        public string GetToken()
        {
            // 1. 定义需要使用到的Claims
            var claims = new[]
            {
                new Claim("Id", "9527"),
                new Claim("Name", "Admin")
            };

            // 2. 从 appsettings.json 中读取 SecretKey。
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            // var secretKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["JWT:SecretKey"]));

            // 3. 选择加密算法。
            var algorithm = SecurityAlgorithms.HmacSha256;

            // 4. 生成Credentials。
            var signingCredentials = new SigningCredentials(secretKey, algorithm);

            // 5. 从 appsettings.json 中读取 Expires 。
            var expires = Convert.ToDouble(_configuration["JWT:Expires"]);

            // 6. 根据以上，生成token
            var token = new JwtSecurityToken(
                //Issuer
                _configuration["JWT:Issuer"],
                //Audience
                _configuration["JWT:Audience"],
                //Claims,
                claims,
                //NotBefore
                DateTime.Now,
                //Expires
                DateTime.Now.AddDays(expires),
                //Credentials
                signingCredentials
            );

            // 7. 将 token 变为 string。
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
