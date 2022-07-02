using Microsoft.AspNetCore.Identity;

namespace PSI.Administration.Identity
{
    /// <summary>
    /// Identity User
    /// 系统用户
    /// </summary>
    public class User : IdentityUser<int>
    {
        /// <summary>
        /// 系统默认管理员
        /// </summary>
        public const string Manager = "manager";
    }
}
