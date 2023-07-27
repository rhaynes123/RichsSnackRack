using System;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Menu;
using RichsSnackRack.Persistence;
/*
 * https://benchmarkdotnet.org
 * https://www.briangetsbinary.com/software%20engineering/dotnet/csharp/performance/2022/05/26/benchmarkdotnet-ef-core-vs-ef-6-part-1.html
 */
namespace RichsSnackRack.PerformanceTests.Menu.QueryTests
{
    [MemoryDiagnoser]
    [MaxColumn]
    [MinColumn]
    public sealed class GetAllMenuQueryTests
    {
        private SnackRackDbContext DbContext;
        public GetAllMenuQueryTests()
        {
            DbContextOptionsBuilder<SnackRackDbContext> builder = new();
            builder
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            DbContext = new SnackRackDbContext(builder.Options);
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();
        }
        [Benchmark(Baseline = true)]
        public async Task GetAllMenuQueryReturnsFromHandler()
        {
            GetAllMenuQuery menuQuery = new();
            GetAllMenuQueryHandler handler = new(DbContext);
            await handler.Handle(menuQuery, default);
        }

    }
}

