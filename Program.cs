using mercedes.Models;
using mercedes.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Elastic.Apm;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Elastic.Apm.NetCoreAll;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddOpenTelemetryTracing(configuration =>
// {
//     configuration
//         .AddAspNetCoreInstrumentation()
//         .SetResourceBuilder(ResourceBuilder.CreateDefault()
//             .AddService("MyWebApp")
//             .AddTelemetrySdk())
//         .AddOtlpExporter(options =>
//         {
//             options.Endpoint = new Uri("http://172.17.0.4:4317"); // SigNoz Endpoint
//         });
// });


IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<NorthwindContext>(option=> option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAllElasticApm(configuration);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
