using UserServer.API.Common;
using UserServer.API.Extensions;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// ��ӿ������
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://example.com",
                "http://www.contoso.com");
        });
});

// ע�����
builder.Services.AddTransient<IJwtToken, JwtToken>();

// �����Ȩ����
builder.AddCustomAuthentication();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#if DEBUG
// ����Զ���� Swagger ����
builder.Services.AddCustomSwaggerGen();
#endif

var app = builder.Build();

#if DEBUG
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // UseSwaggerUI �����˾�̬�ļ��м����
    app.UseSwaggerUI();
}
#endif

app.UseHttpsRedirection();

// ʹ�ÿ�������
app.UseCors(MyAllowSpecificOrigins);

// ʹ�ü�Ȩ����
app.UseAuthentication();//��ǰ

// ʹ����Ȩ����
app.UseAuthorization();//�ں�

app.MapControllers();

app.Run();
