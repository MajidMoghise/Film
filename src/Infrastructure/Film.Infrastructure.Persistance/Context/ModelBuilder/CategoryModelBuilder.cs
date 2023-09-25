using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Film.Infrastructure.Persistance.Context.ModelsBuilder
{
    public static class CategoryModelBuilder
    {
        public static void CategoryBuilder(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuider)
        {
            modelBuider.Entity<Domain.Enities.Category>(entity =>
            {
                entity.HasKey(e => e.Code);
                entity.Property(e => e.Code)
                      .IsRequired()
                      .ValueGeneratedNever();
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.Description)
                      .IsRequired();
                entity.Property(e => e.Priority)
                      .IsRequired();
                entity.Property(e => e.IsEnabled)
                      .IsRequired();
                entity.Property(e => e.Hash)
                     .IsRequired();
                entity.Property(p => p.LastUpdate)
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("getdate()");
            
                entity.Property(e => e.RowVersion).IsRowVersion();

            });
        }
    }
}
