using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PSI.Administration.Identity;

namespace PSI.NHibernate.Mappings;

public class UserMappingMsSql : ClassMapping<User>
{
    public UserMappingMsSql()
    {
        Table("AspNetUsers");
        Id(e => e.Id, id =>
        {
            id.Column("Id");
            id.Type(NHibernateUtil.Int32);
            // id.Generator(Generators.Increment);
        });
        Property(e => e.UserName, prop =>
        {
            prop.Column("UserName");
            // prop.Type(NHibernateUtil.String);
            prop.Length(64);
            prop.NotNullable(true);
            prop.Unique(true);
        });
        Property(e => e.NormalizedUserName, prop =>
        {
            prop.Column("NormalizedUserName");
            // prop.Type(NHibernateUtil.String);
            prop.Length(64);
            prop.NotNullable(true);
            prop.Unique(true);
        });
        Property(e => e.Email, prop =>
        {
            prop.Column("Email");
            // prop.Type(NHibernateUtil.String);
            prop.Length(256);
            prop.NotNullable(true);
        });
        Property(e => e.NormalizedEmail, prop =>
        {
            prop.Column("NormalizedEmail");
            // prop.Type(NHibernateUtil.String);
            prop.Length(256);
            prop.NotNullable(true);
        });
        Property(e => e.EmailConfirmed, prop =>
        {
            prop.Column("EmailConfirmed");
            // prop.Type(NHibernateUtil.Boolean);
            prop.NotNullable(true);
        });
        Property(e => e.PhoneNumber, prop =>
        {
            prop.Column("PhoneNumber");
            // prop.Type(NHibernateUtil.String);
            prop.Length(128);
            prop.NotNullable(false);
        });
        Property(e => e.PhoneNumberConfirmed, prop =>
        {
            prop.Column("PhoneNumberConfirmed");
            // prop.Type(NHibernateUtil.Boolean);
            prop.NotNullable(true);
        });
        Property(e => e.LockoutEnabled, prop =>
        {
            prop.Column("LockoutEnabled");
            // prop.Type(NHibernateUtil.Boolean);
            prop.NotNullable(true);
        });
        Property(e => e.LockoutEnd, prop =>
        {
            prop.Column("LockoutEnd");
            prop.Type(NHibernateUtil.DateTime);
            prop.NotNullable(false);
        });
        Property(e => e.AccessFailedCount, prop =>
        {
            prop.Column("AccessFailedCount");
            // prop.Type(NHibernateUtil.Int32);
            prop.NotNullable(true);
        });
        Property(e => e.ConcurrencyStamp, prop =>
        {
            prop.Column("ConcurrencyStamp");
            // prop.Type(NHibernateUtil.String);
            prop.Length(36);
            prop.NotNullable(false);
        });
        Property(e => e.PasswordHash, prop =>
        {
            prop.Column("PasswordHash");
            // prop.Type(NHibernateUtil.String);
            prop.Length(256);
            prop.NotNullable(false);
        });
        Property(e => e.TwoFactorEnabled, prop =>
        {
            prop.Column("TwoFactorEnabled");
            // prop.Type(NHibernateUtil.Boolean);
            prop.NotNullable(true);
        });
        Property(e => e.SecurityStamp, prop =>
        {
            prop.Column("SecurityStamp");
            // prop.Type(NHibernateUtil.String);
            prop.Length(64);
            prop.NotNullable(false);
        });
    }
}