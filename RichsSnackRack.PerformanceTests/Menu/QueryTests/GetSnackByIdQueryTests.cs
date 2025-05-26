using System;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Persistence;

namespace RichsSnackRack.PerformanceTests.Menu.QueryTests
{
    [MemoryDiagnoser]
    [MaxColumn]
    [MinColumn]
    [CsvExporter]
    [KeepBenchmarkFiles]
    public class GetSnackByIdQueryTests : IDisposable
	{
        private SnackRackDbContext DbContext;

        [Params(1,5, 100,200, 9_999)]
        public int Ids { get; set; }

        public GetSnackByIdQueryTests()
		{
            DbContextOptionsBuilder<SnackRackDbContext> builder = new();
            builder
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            DbContext = new SnackRackDbContext(builder.Options);
            
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();

            var snacks = Enumerable.Range(1, 10_000).Select(id =>
            {
                var random = new Random();
                return new Snack { Id = random.Next(), Price = 5.99m, Name = "Wings" };
            });

            DbContext.Snacks.AddRange(snacks);

            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
        }

        [Benchmark(Baseline = true)]
        public async Task GetSnackByIdFirstOrDefaultAsync()
        {
            await DbContext.Snacks.FirstOrDefaultAsync(snack => snack.Id == Ids);
        }

        [Benchmark]
        public async Task GetSnackByIdSingleOrDefaultAsync()
        {
            await DbContext.Snacks.SingleOrDefaultAsync(snack => snack.Id == Ids);
        }
        [Benchmark]
        public async Task GetSnackByIdFindAsync()
        {
            await DbContext.Snacks.FindAsync(Ids);
        }

    }
}

