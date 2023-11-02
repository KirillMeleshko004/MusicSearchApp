using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

var builder = WebApplication.CreateBuilder(args);

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
