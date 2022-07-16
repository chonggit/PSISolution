using PSI.Administration.Identity;
using PSI.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddNHibernate((config, services) =>
{
    config.UseSqlite(services.GetRequiredService<IConfiguration>().GetConnectionString("db"));
});
builder.Services.AddIdentity();

WebApplication app = builder.Build();

app.MapControllers();
app.Run();
