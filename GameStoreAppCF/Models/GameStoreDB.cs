namespace GameStoreAppCF.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GameStoreDB : DbContext
    {
        public GameStoreDB()
            : base("name=GameStoreDB")
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.Game)
                .WithOptional(e => e.Author)
                .HasForeignKey(e => e.Author_ID);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Review)
                .WithOptional(e => e.Game)
                .HasForeignKey(e => e.Game_ID);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Order)
                .WithMany(e => e.Game)
                .Map(m => m.ToTable("Orders_Games"));

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Game)
                .WithOptional(e => e.Genre)
                .HasForeignKey(e => e.Genre_ID);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.Game)
                .WithOptional(e => e.Manufacturer)
                .HasForeignKey(e => e.Manufacturer_ID);

            modelBuilder.Entity<Type>()
                .HasMany(e => e.Game)
                .WithOptional(e => e.Type)
                .HasForeignKey(e => e.Type_ID);
        }
    }
}
