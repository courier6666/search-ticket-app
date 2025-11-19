using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SearchTicketApp.Data;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Data.Seed;
using SearchTicketApp.Extensions;
using SearchTicketApp.Factories;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Options;
using SearchTicketApp.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TicketSqliteDb");

builder.Services.Configure<AdminCredentials>(builder.Configuration.GetSection("AdminCredentials"));

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ConfigureJsonSerializerOptions();
});

builder.Services.AddDbContext<TicketDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddSingleton<JsonSerializerOptions>((serviceProvider) =>
{
    var jsonSerializerOptions = new JsonSerializerOptions();
    jsonSerializerOptions.ConfigureJsonSerializerOptions();
    return jsonSerializerOptions;
});

builder.Services.AddIdentity<User, IdentityRole<int>>().
    AddEntityFrameworkStores<TicketDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextAccessor, UserContextAccessor>();
builder.Services.AddScoped<IOnSaleTicketService, OnSaleTicketService>();
builder.Services.AddScoped<IOnSaleTicketContextSearchService, OnSaleTicketContextSearchService>();
builder.Services.AddScoped<IPurchasedTicketService, PurchasedTicketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await SeedRoles.SeedRolesAsync(app);
await SeedUsers.SeedUsersAsync(app);
await SeedTickets.SeedOnSaleTicketsAsync(app);

await app.RunAsync();
