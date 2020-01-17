using BackgroundJobs.Models;
using BackgroundJobs.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackgroundJobs.Data
{
    public class NodeMap : IEntityTypeConfiguration<Node>
    {
        public void Configure(EntityTypeBuilder<Node> builder)
        {
            builder.Property(m => m.Id)
                .HasColumnName("Id_Node")
                .IsRequired();

            builder.Property(m => m.ParentId)
                .HasColumnName("Id_Parent")
                .IsRequired(false);

            builder.Property(m => m.ModelId)
                .HasColumnName("Id_Model")
                .IsRequired();

            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Input).IsRequired();
            builder.Property(m => m.Output).IsRequired(false);            
            builder.Property(m => m.Operation).HasColumnType("int").IsRequired();


            builder.HasOne(m => m.ParentReference)
                .WithMany(m => m.LinkedNodes)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // É a mesma coisa que usar
            //  builder.HasMany(m => m.LinkedNodes)
            //    .WithOne(m => m.ParentReference)
            //    .HasForeignKey(e => e.ParentId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Model>()
                .WithMany()
                .HasForeignKey(model => model.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("NodeFlow");
        }
    }
}
