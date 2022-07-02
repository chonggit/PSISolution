using Microsoft.AspNetCore.Identity;

namespace PSI.Administration.Identity
{
    /// <summary>
    /// Identity Role
    /// 系统角色
    /// </summary>
    public class Role : IdentityRole<int>
    {
        /// <summary>
        /// 系统默认管理员角色
        /// </summary>
        public const string Administrators = "Administrators";
    }
}
