﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSI.Administration.Identity;

namespace PSI.EntityFramework.Mapping
{
    internal class UserClaimMappings : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("AspNetUserClaims");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ClaimType).HasMaxLength(1024).IsRequired();
            builder.Property(x => x.ClaimValue).HasMaxLength(1024).IsRequired();
        }
    }
}
