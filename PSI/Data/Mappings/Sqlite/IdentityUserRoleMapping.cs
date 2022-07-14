using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using PSI.Administration.Identity;

namespace PSI.Data.Mappings.Sqlite
{
    internal class IdentityUserRoleMapping : ClassMapping<UserRole>
    {
        public IdentityUserRoleMapping()
        {
            Table("aspnet_user_roles");
            ComposedId(id =>
            {
                id.Property(e => e.UserId, prop =>
                {
                    prop.Column("user_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.RoleId, prop =>
                {
                    prop.Column("role_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
        }
    }
}
