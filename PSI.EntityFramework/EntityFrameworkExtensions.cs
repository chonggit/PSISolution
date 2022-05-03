using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PSI.Data;

namespace PSI.EntityFramework
{
    /// <summary>
    /// Entity Frameword Service Collection Extensions
    /// </summary>
    public static class EntityFrameworkExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services.AddDbContext<DbContext, PSIDbContext>(optionsAction);
            services.AddScoped<IDbSession, DbSession>();
        }
    }
}
