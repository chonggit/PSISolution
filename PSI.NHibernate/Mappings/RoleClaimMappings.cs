using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PSI.Administration.Identity;
using NHibernate;

namespace PSI.NHibernate.Mappings;

public class RoleClaimMappingMsSql : ClassMapping<RoleClaim>
{
    public RoleClaimMappingMsSql()
    {
        Table("AspNetRoleClaims");
        Id(e => e.Id, id =>
        {
            id.Column("Id");
            id.Type(NHibernateUtil.Int32);
            id.Generator(Generators.Increment);
        });
        Property(e => e.ClaimType, prop =>
        {
            prop.Column("ClaimType");
            prop.Type(NHibernateUtil.String);
            prop.Length(1024);
            prop.NotNullable(true);
        });
        Property(e => e.ClaimValue, prop =>
        {
            prop.Column("ClaimValue");
            prop.Type(NHibernateUtil.String);
            prop.Length(1024);
            prop.NotNullable(true);
        });
        Property(e => e.RoleId, prop =>
        {
            prop.Column("RoleId");
            prop.Type(NHibernateUtil.Int32);
            prop.NotNullable(true);
        });
    }
}