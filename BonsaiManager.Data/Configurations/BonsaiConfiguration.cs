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
    public class BonsaiConfiguration : IEntityTypeConfiguration<Bonsai>
    {
        public void Configure(EntityTypeBuilder<Bonsai> builder)
        {
            builder.ToTable("Bonsais");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Style)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Notes)
                .HasMaxLength(1000);

            builder.Property(b => b.ImageUrl)
                .HasMaxLength(500);

            builder.HasOne(b => b.Species)
                .WithMany(s => s.Bonsais)
                .HasForeignKey(b => b.SpeciesId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.CareRecords)
                .WithOne(c => c.Bonsai)
                .HasForeignKey(c => c.BonsaiId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
