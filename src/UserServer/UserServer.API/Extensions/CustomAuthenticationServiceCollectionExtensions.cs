using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UserServer.API.Extensions
{
    /// <summary>
    /// 自定义授权服务扩展。
    /// </summary>
    internal static class CustomAuthenticationServiceCollectionExtensions
    {
        /// <summary>
        /// 添加自定义授权服务。
        /// </summary>
        /// <param name="builder">构造器。</param>
        /// <returns>返回授权服务构造器。</returns>
        public static AuthenticationBuilder AddCustomAuthentication(this WebApplicationBuilder builder)
        {
            return builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    // 验证发布者。
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],

                    // 验证接收者。
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],

                    // 验证时间。
                    ValidateLifetime = true,

                    // 验证安全密钥。
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
                };
            });
        }
    }
}
