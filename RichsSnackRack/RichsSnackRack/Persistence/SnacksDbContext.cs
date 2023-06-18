using System;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Menu;

namespace RichsSnackRack.Persistence;

	public class SnacksDbContext: DbContext
	{
		public SnacksDbContext(DbContextOptions<SnacksDbContext> options) : base(options: options)
		{
		}
		public DbSet<Snack> Snacks { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Snack>(entity =>
            {
                entity.HasKey(snack => snack.Id);
                entity.Property(snack => snack.Name);
                entity.Property(snack => snack.ImageUrl);
                entity.Property(movie => movie.Price);

            });
        }
}

