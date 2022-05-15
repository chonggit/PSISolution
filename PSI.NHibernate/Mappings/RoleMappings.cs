using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PSI.Administration.Identity;
using NHibernate;

namespace PSI.NHibernate.Mappings;

public class RoleMappingMsSql : ClassMapping<Role>
{
    public RoleMappingMsSql()
    {
        Table("AspNetRoles");
        Id(e => e.Id, id =>
        {
            id.Column("Id");
            // id.Type(NHibernateUtil.Int32);
            id.Generator(Generators.Increment);
        });
        Property(e => e.Name, prop =>
        {
            prop.Column("Name");
            // prop.Type(NHibernateUtil.String);
            prop.Length(64);
            prop.NotNullable(true);
            prop.Unique(true);
        });
        Property(e => e.NormalizedName, prop =>
        {
            prop.Column("NormalizedName");
            // prop.Type(NHibernateUtil.String);
            prop.Length(64);
            prop.NotNullable(true);
            prop.Unique(true);
        });
        Property(e => e.ConcurrencyStamp, prop =>
        {
            prop.Column("ConcurrencyStamp");
            // prop.Type(NHibernateUtil.String);
            prop.Length(36);
            prop.NotNullable(false);
        });
    }

}