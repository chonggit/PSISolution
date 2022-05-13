using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSI.Administration.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.EntityFramework.Mapping
{
    internal class UserLoginMappings : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("AspNetUserLogins");

            builder.HasKey(x => new {x.LoginProvider, x.ProviderKey });
            builder.Property(x => x.LoginProvider).HasMaxLength(32);
            builder.Property(x => x.ProviderKey).HasMaxLength(32);
            builder.Property(x=>x.ProviderDisplayName).HasMaxLength(32).IsRequired();
        }
    }
}
