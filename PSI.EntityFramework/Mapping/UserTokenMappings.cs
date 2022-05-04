using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSI.Administration.Identity;


namespace PSI.EntityFramework.Mapping
{
    internal class UserTokenMappings : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            builder.Property(t => t.LoginProvider).HasMaxLength(32);
            builder.Property(t => t.Name).HasMaxLength(32);
            builder.ToTable("AspNetUserTokens");
        }
    }
}
