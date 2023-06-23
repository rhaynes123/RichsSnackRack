using System;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Menu;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.Persistence;

	public class SnackRackDbContext: DbContext
	{
		public SnackRackDbContext(DbContextOptions<SnackRackDbContext> options) : base(options: options)
		{
		}
		public DbSet<Snack> Snacks { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Snack>(entity =>
            {
                entity.HasKey(snack => snack.Id);
                entity.Property(snack => snack.Name);
                entity.Property(snack => snack.ImageUrl);
                entity.Property(movie => movie.Price);
                entity.Property(snack => snack.Active)
                .HasDefaultValue(1);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(order => order.Id);
                entity.Property(order => order.SnackId);
                entity.Property(order => order.OrderDate);
                entity.Property(order => order.OrderStatus)
                .HasConversion<int>();
            });
    }
}

