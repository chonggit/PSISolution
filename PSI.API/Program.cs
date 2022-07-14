using PSI.Administration.Identity;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddIdentity();

WebApplication app = builder.Build();

app.MapControllers();
app.Run();
