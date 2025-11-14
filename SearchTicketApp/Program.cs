using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SearchTicketApp.Data;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Extensions;
using SearchTicketApp.Factories;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TicketSqliteDb");

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.ConfigureJsonSerializerOptions();
});

builder.Services.AddDbContext<TicketDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddIdentity<User, IdentityRole<int>>().
    AddEntityFrameworkStores<TicketDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextAccessor, UserContextAccessor>();
builder.Services.AddScoped<IOnSaleTicketService, OnSaleTicketService>();
builder.Services.AddScoped<IPurchasedTicketService, PurchasedTicketService>();
builder.Services.AddScoped<IOnSaleTicketSearchService, OnSaleTicketSearchService>();

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

app.Run();
