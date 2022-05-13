using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSI.Administration.Identity;


namespace PSI.EntityFramework.Mapping
{
    internal class UserMappings : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AspNetUsers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).HasMaxLength(64).IsRequired();
            builder.HasIndex(x => x.UserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.Property(x => x.NormalizedUserName).HasMaxLength(64).IsRequired();
            builder.HasIndex(x => x.NormalizedUserName).HasDatabaseName("NormalizedUserNameIndex").IsUnique();
            builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
            builder.Property(x => x.NormalizedEmail).HasMaxLength(256).IsRequired();
            builder.Property(x => x.EmailConfirmed).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(128);
            builder.Property(x => x.PhoneNumberConfirmed).IsRequired();
            builder.Property(x => x.LockoutEnabled).IsRequired();
            builder.Property(x => x.LockoutEnd);
            builder.Property(x => x.AccessFailedCount).IsRequired();
            builder.Property(x => x.ConcurrencyStamp).HasMaxLength(32);
            builder.Property(x => x.PasswordHash).HasMaxLength(256);
            builder.Property(x => x.TwoFactorEnabled).IsRequired();
            builder.Property(x => x.SecurityStamp).HasMaxLength(64);

            builder.HasMany<UserClaim>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
            builder.HasMany<UserLogin>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
            builder.HasMany<UserToken>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
            builder.HasMany<UserRole>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        }
    }
}
