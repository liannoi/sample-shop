using System.Data.Entity;
using Shop.Domain.Entities;

namespace Shop.Persistence.System
{
    public class ShopContext : DbContext
    {
        public ShopContext()
            : base("name=ShopContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SalePos> SalePos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Good>()
                .Property(e => e.GoodName)
                .IsUnicode(false);

            modelBuilder.Entity<Good>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Good>()
                .Property(e => e.GoodCount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Good>()
                .HasMany(e => e.Photos)
                .WithRequired(e => e.Good)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Good>()
                .HasMany(e => e.SalePos)
                .WithRequired(e => e.Good)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasMany(e => e.SalePos)
                .WithRequired(e => e.Sale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalePos>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 0);
        }
    }
}