using System;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Persistence;

namespace RichsSnackRack.IntegrationTests
{
	public class IntegrationDbContextFixture: IDisposable
	{
        public readonly SnackRackDbContext dbContext;
        public IntegrationDbContextFixture()
		{
            DbContextOptionsBuilder<SnackRackDbContext> builder = new();
            builder
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            dbContext = new SnackRackDbContext(builder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        public void LoadTestData()
        {
            try
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                var fakemovies = Enumerable.Range(1, 5).Select(id =>
                {
                    var random = new Random();
                    return new Order { SnackId = random.Next(), OrderStatus = RichsSnackRack.Orders.Models.Enums.OrderStatus.NA};
                }).ToList();

                dbContext.Orders.AddRange(fakemovies);
                dbContext.SaveChanges();
            }
            catch (System.InvalidOperationException)
            {
                Dispose();
            }

        }
    }
}

