using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Orders;
using RichsSnackRack.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMediator(options: option => option.ServiceLifetime = ServiceLifetime.Scoped);
string mySqlConnectionString = (Environment.GetEnvironmentVariable("CONTAINER") == "true") == true
    ? builder.Configuration.GetConnectionString("SnackDbConnection")!
    : builder.Configuration.GetConnectionString("LocalHost")!;

builder.Services.AddDbContext<SnackRackDbContext>((serviceProvider, optionsBuilder) =>
{
    var performanceLogger = serviceProvider.GetRequiredService<ILogger<PerformanceInterceptor>>();
    optionsBuilder.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString), optionsBuilder => optionsBuilder
        .EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null));
    optionsBuilder.AddInterceptors(new PerformanceInterceptor(performanceLogger));
    optionsBuilder.EnableDetailedErrors();
    optionsBuilder.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    SnackRackDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<SnackRackDbContext>();
    await dbContext.Database.MigrateAsync();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

public partial class Program { }
