using Microsoft.AspNetCore.Identity;

namespace PSI.Administration.Identity
{
    /// <summary>
    /// User　Login
    /// </summary>
    public class UserLogin : IdentityUserLogin<int>
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
