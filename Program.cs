using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using MusicSearchApp.Models.DB;

var builder = WebApplication.CreateBuilder(args);

//Receiving connection string from appsettings.json
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllersWithViews();

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "Client/dict";
});

var app = builder.Build();

app.UseSpaStaticFiles();
app.MapGet("/", () => "Hello World!");


app.UseSpa(spa =>
{
    spa.Options.SourcePath="Client";
    spa.UseReactDevelopmentServer("start");
});

app.Run();
