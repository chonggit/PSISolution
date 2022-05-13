using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSI.Administration.Identity;


namespace PSI.EntityFramework.Mapping
{
    internal class UserRoleMappings : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("AspNetUserRoles");
            builder.HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
