using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSI.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.EntityFramework.Mapping
{
    internal class ItemsMapping : IEntityTypeConfiguration<Items>
    {
        public void Configure(EntityTypeBuilder<Items> builder)
        {
            builder.HasKey(item => item.ItemCode);
            builder.Property(item => item.ItemCode).HasMaxLength(20);
            builder.Property(item => item.ItemName).HasMaxLength(100).IsRequired();
        }
    }
}
