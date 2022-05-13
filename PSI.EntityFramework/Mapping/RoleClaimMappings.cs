using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSI.Administration.Identity;

namespace PSI.EntityFramework.Mapping
{
    internal class RoleClaimMappings : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("AspNetRoleClaims");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ClaimType).HasMaxLength(1024).IsRequired();
            builder.Property(x => x.ClaimValue).HasMaxLength(1024).IsRequired();
        }
    }
}
