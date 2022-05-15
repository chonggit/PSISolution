using NHibernate.Mapping.ByCode.Conformist;
using PSI.Administration.Identity;
using NHibernate;

namespace PSI.NHibernate.Mappings;

public class UserRoleMappingMsSql : ClassMapping<UserRole>
{
    public UserRoleMappingMsSql()
    {
        Table("AspNetUserRoles");
        ComposedId(id =>
        {
            id.Property(e => e.UserId, prop =>
            {
                prop.Column("UserId");
                // prop.Type(NHibernateUtil.Int32);
            });
            id.Property(e => e.RoleId, prop =>
            {
                prop.Column("RoleId");
                // prop.Type(NHibernateUtil.Int32);
            });
        });
    }
}