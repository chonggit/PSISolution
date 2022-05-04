using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using PSI.Administration.Identity;

namespace PSI.NHibernate.Mappings;

public class UserLoginMappingMsSql : ClassMapping<UserLogin>
{

    public UserLoginMappingMsSql()
    {
        Table("AspNetUserLogins");
        ComposedId(id =>
        {
            id.Property(e => e.LoginProvider, prop =>
            {
                prop.Column("LoginProvider");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
            });
            id.Property(e => e.ProviderKey, prop =>
            {
                prop.Column("ProviderKey");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
            });
        });
        Property(e => e.ProviderDisplayName, prop =>
        {
            prop.Column("ProviderDisplayName");
            prop.Type(NHibernateUtil.String);
            prop.Length(32);
            prop.NotNullable(true);
        });
        Property(e => e.UserId, prop =>
        {
            prop.Column("UserId");
            prop.Type(NHibernateUtil.Int32);
            prop.NotNullable(true);
        });
    }

}