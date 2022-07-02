using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PSI.Administration.Identity
{
    public static class IdentityExtensions
    {
        public static IdentityBuilder AddIdentity(this IServiceCollection services)
        {
            return services.AddIdentityCore<User>()
                  .AddRoles<Role>()
                  .AddRoleStore<RoleStore>()
                  .AddUserStore<UserStore>();
        }
    }
}
