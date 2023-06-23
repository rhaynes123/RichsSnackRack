using System;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Menu.Models;
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
    }
}

