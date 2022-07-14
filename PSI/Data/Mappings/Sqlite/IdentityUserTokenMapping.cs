using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using PSI.Administration.Identity;

namespace PSI.Data.Mappings.Sqlite
{
    internal class IdentityUserTokenMapping : ClassMapping< UserToken>
    {
        public IdentityUserTokenMapping ()
        {
            Table("aspnet_user_tokens");
            ComposedId(id => {
                id.Property(e => e.UserId, prop => {
                    prop.Column("user_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.LoginProvider, prop => {
                    prop.Column("login_provider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.Name, prop => {
                    prop.Column("name");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.Value, prop => {
                prop.Column("value");
                prop.Type(NHibernateUtil.String);
                prop.Length(256);
                prop.NotNullable(true);
            });
        }

    }
}
