using BackgroundJobs.Models;
using BackgroundJobs.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackgroundJobs.Data
{
    public class ModelMap : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.Property(m => m.Id)
                .HasColumnName("Id_Model")
                .IsRequired();

            builder.Property(m => m.Name).IsRequired();

            builder.ToTable("ModelFlow");
        }
    }
}
