using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSI.Administration.Identity;


namespace PSI.EntityFramework.Mapping
{
    internal class UserTokenMappings : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("AspNetUserTokens");
            builder.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
            builder.Property(x => x.LoginProvider).HasMaxLength(32);
            builder.Property(x => x.Name).HasMaxLength(32);
            builder.Property(x => x.Value).HasMaxLength(256).IsRequired();
        }
    }
}
