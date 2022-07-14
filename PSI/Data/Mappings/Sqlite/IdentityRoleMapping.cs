using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PSI.Administration.Identity;

namespace PSI.Data.Mappings.Sqlite
{
    internal class IdentityRoleMapping : ClassMapping<Role>
    {
        public IdentityRoleMapping()
        {
            Table("aspnet_roles");
            Id(e => e.Id, id =>
            {
                id.Column("id");
                id.Type(NHibernateUtil.Int32);
                id.Generator(Generators.Identity);
            });
            Property(e => e.Name, prop =>
            {
                prop.Column("name");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.NormalizedName, prop =>
            {
                prop.Column("normalized_name");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.ConcurrencyStamp, prop =>
            {
                prop.Column("concurrency_stamp");
                prop.Type(NHibernateUtil.String);
                prop.Length(36);
                prop.NotNullable(false);
            });
        }
    }
}
