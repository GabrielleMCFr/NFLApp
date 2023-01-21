using Microsoft.EntityFrameworkCore;
using Leagues.Data;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// configure session state cookie handler
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddRazorPages();

// connect to db
builder.Services.AddDbContext<LeagueContext>(options =>
   options.UseSqlite(builder.Configuration.GetConnectionString("LeagueContext")));

var app = builder.Build();

// call the initializer to populate the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LeagueContext>();
    // context.Database.EnsureCreated();
    DbInitializer.Initialize(context);

    //var players = context.Players.ToList();
    //if (players.Count == 0)
    //{
    //    AddPlayers.AddAllPlayers(context);
    //}
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

// allow session state via cookies
app.UseSession();

app.MapRazorPages();

app.Run();

