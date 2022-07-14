using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using PSI.Administration.Identity;

namespace PSI.Data.Mappings.Sqlite
{
    internal class IdentityUserLoginMapping : ClassMapping<UserLogin>
    {
        public IdentityUserLoginMapping()
        {
            Table("aspnet_user_logins");
            ComposedId(id =>
            {
                id.Property(e => e.LoginProvider, prop =>
                {
                    prop.Column("login_provider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.ProviderKey, prop =>
                {
                    prop.Column("provider_key");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.ProviderDisplayName, prop =>
            {
                prop.Column("provider_display_name");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
            Property(e => e.UserId, prop =>
            {
                prop.Column("user_id");
                prop.Type(NHibernateUtil.Int32);
                prop.NotNullable(true);
            });
        }

    }
}
