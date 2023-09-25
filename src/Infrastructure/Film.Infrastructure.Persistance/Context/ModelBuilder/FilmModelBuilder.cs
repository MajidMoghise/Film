using Microsoft.EntityFrameworkCore;

namespace Film.Infrastructure.Persistance.Context.ModelsBuilder
{
    public static class FilmModelBuilder
    {
        public static void FilmBuilder(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuider)
        {
            modelBuider.Entity<Domain.Enities.Film>(entity =>
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
                entity.Property(e => e.Hash)
                     .IsRequired();
                entity.Property(e => e.IsEnabled)
                     .IsRequired();
                entity.HasOne(e => e.Category)
                      .WithMany()
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.Property(p => p.LastUpdate)
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("getdate()");

                entity.Property(e => e.RowVersion)
                      .IsRowVersion();

            });

        }
    }
}
