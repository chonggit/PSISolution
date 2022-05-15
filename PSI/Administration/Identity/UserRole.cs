using Microsoft.AspNetCore.Identity;

namespace PSI.Administration.Identity
{
    /// <summary>
    /// 用户组
    /// </summary>
    public class UserRole : IdentityUserRole<int>
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
