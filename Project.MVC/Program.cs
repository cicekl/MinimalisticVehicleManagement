using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using Project.MVC.DependencyInjection;
using Project.MVC.Mapping;
using Project.Service.DataAccess;
using Project.Service.Services;
using System.Runtime.Loader;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var kernel = new StandardKernel();
builder.Services.AddSingleton<IServiceProviderFactory<IServiceCollection>>(new NinjectServiceProviderFactory(kernel));
builder.Services.AddScoped<IVehicleService, VehicleService>();
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddDbContext<VehicleDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("VehicleDBConnection")));
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
builder.Services.AddAutoMapper(typeof(MappingProfile));


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
    name: "deleteConfirmed",
    pattern: "VehicleMakes/DeleteConfirmed",
    defaults: new { controller = "VehicleMakes", action = "DeleteConfirmed" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
