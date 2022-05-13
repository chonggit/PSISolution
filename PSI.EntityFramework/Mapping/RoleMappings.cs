using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSI.Administration.Identity;

namespace PSI.EntityFramework.Mapping
{
    internal class RoleMappings : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("AspNetRoles");

            builder.HasKey(x => x.Id); 
            builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
            builder.Property(x => x.NormalizedName).HasMaxLength(64).IsRequired();
            builder.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();

            builder.HasIndex(x => x.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

            builder.HasMany<UserRole>().WithOne().HasForeignKey(x => x.RoleId).IsRequired();
            builder.HasMany<RoleClaim>().WithOne().HasForeignKey(x => x.RoleId).IsRequired();
        }
    }
}
