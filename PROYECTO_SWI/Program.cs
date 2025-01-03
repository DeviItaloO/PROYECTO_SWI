﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PROYECTO_SWI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PROYECTO_SWIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("eConnection") ?? throw new InvalidOperationException("Connection string 'eConnection' not found.")));

builder.Services.AddDistributedMemoryCache();//sesion
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
