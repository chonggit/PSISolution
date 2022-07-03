using Microsoft.EntityFrameworkCore;
using PSI.Administration.Identity;
using PSI.EntityFramework;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEntityFramework(options =>
{
    options.UseInMemoryDatabase("PSISolution");
});
builder.Services.AddIdentity();

WebApplication app = builder.Build();

app.MapControllers();
app.Run();
