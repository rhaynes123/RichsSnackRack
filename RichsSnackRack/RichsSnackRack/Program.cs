using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string mySqlConnectionString = builder.Configuration.GetConnectionString("SnackDbConnection")!;
builder.Services.AddDbContext<SnacksDbContext>((serviceProvider, optionsBuilder) =>
{
    optionsBuilder.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString), optionsBuilder => optionsBuilder
        .EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null));

    optionsBuilder.EnableDetailedErrors();
    optionsBuilder.EnableSensitiveDataLogging();
});
var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    SnacksDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<SnacksDbContext>();
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

