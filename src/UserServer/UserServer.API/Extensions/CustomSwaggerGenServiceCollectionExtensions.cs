using Microsoft.OpenApi.Models;

namespace UserServer.API.Extensions
{
    /// <summary>
    /// 自定义扩展 SwaggerGen。
    /// </summary>
    internal static class CustomSwaggerGenServiceCollectionExtensions
    {
        /// <summary>
        /// 添加自定义的 swagger 服务。
        /// 需要配置 jwt 认证，利于调试。
        /// </summary>
        /// <param name="services">服务容器。</param>
        /// <returns>返回服务容器。</returns>
        public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new List<string>()
                } });
            });

            return services;
        }
    }
}
