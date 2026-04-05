using BonsaiManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.Data.Configurations
{
    public class CareRecordConfiguration : IEntityTypeConfiguration<CareRecord>
    {
        public void Configure(EntityTypeBuilder<CareRecord> builder)
        {
            builder.ToTable("CareRecords");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CareType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(c => c.Date)
                .IsRequired();

            builder.Property(c => c.Notes)
                .HasMaxLength(1000);
        }
    }
}
