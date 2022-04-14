var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
app.UseEndpoints((endPoint) =>
{
});
app.MapGet("/", () => "Hello World!");

app.Run();