using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSI.Data;
using Microsoft.EntityFrameworkCore;

namespace PSI.EntityFramework
{
    /// <summary>
    /// Entity Frameword Service Collection Extensions
    /// </summary>
    public static class EntityFrameworkExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null)
        {
            services.AddDbContext<DbContext, PSIDbContext>(optionsAction);
            services.AddScoped<IDbSession, DbSession>();
        }
    }
}
