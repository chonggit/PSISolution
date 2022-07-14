using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using PSI.Administration.Identity;
using NHibernate.Mapping.ByCode;

namespace PSI.Data.Mappings.Sqlite
{
    internal class IdentityRoleClaimMapping : ClassMapping<RoleClaim>
    {
        public IdentityRoleClaimMapping()
        {
            Table("aspnet_role_claims");
            Id(e => e.Id, id =>
            {
                id.Column("id");
                id.Type(NHibernateUtil.Int32);
                id.Generator(Generators.Identity);
            });
            Property(e => e.ClaimType, prop =>
            {
                prop.Column("claim_type");
                prop.Type(NHibernateUtil.String);
                prop.Length(1024);
                prop.NotNullable(true);
            });
            Property(e => e.ClaimValue, prop =>
            {
                prop.Column("claim_value");
                prop.Type(NHibernateUtil.String);
                prop.Length(1024);
                prop.NotNullable(true);
            });
            Property(e => e.RoleId, prop =>
            {
                prop.Column("role_id");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
        }
    }
}
