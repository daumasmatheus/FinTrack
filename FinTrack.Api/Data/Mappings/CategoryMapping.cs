using FinTrack.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinTrack.Api.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            
            builder.HasKey(b => b.Id);

            builder.Property(x => x.Title).IsRequired()
                                          .HasColumnType("NVARCHAR")
                                          .HasMaxLength(80);

            builder.Property(x => x.Description).IsRequired(false)
                                          .HasColumnType("NVARCHAR")
                                          .HasMaxLength(255);

            builder.Property(x => x.UserId).IsRequired()
                                           .HasColumnType("VARCHAR")
                                           .HasMaxLength(160);
        }
    }
}
