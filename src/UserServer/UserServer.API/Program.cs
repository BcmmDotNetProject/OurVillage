using UserServer.API.Common;
using UserServer.API.Extensions;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// 添加跨域服务。
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://example.com",
                "http://www.contoso.com");
        });
});

// 注册服务。
builder.Services.AddTransient<IJwtToken, JwtToken>();

// 添加授权服务。
builder.AddCustomAuthentication();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#if DEBUG
// 添加自定义的 Swagger 服务。
builder.Services.AddCustomSwaggerGen();
#endif

var app = builder.Build();

#if DEBUG
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // UseSwaggerUI 启用了静态文件中间件。
    app.UseSwaggerUI();
}
#endif

app.UseHttpsRedirection();

// 使用跨域请求。
app.UseCors(MyAllowSpecificOrigins);

// 使用鉴权服务。
app.UseAuthentication();//在前

// 使用授权服务。
app.UseAuthorization();//在后

app.MapControllers();

app.Run();
