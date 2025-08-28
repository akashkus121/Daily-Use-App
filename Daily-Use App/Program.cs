using Daily_Use_App.Data;
using Daily_Use_App.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (!string.IsNullOrWhiteSpace(conn))
    {
        options.UseSqlServer(conn);
    }
    else
    {
        options.UseSqlite("Data Source=dailyuse.db");
    }
});


builder.Services.AddScoped<IWeatherService, DummyWeatherService>(); // replace with real API later
builder.Services.AddScoped<ISuggestionService, SimpleSuggestionService>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
name: "default",
pattern: "{controller=Dashboard}/{action=Index}/{id?}");


app.Run();