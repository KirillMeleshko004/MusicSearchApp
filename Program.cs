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
    configuration.RootPath = "Client/build";
});

var app = builder.Build();

app.UseSpaStaticFiles();
app.MapControllers();

//Array of path strings
var excludedPaths = new PathString[] { "/api" };

app.UseWhen(
    //if path is not excluded
    (ctx) =>
    {
        var path = ctx.Request.Path;
        return !Array.Exists(excludedPaths, excluded => path.StartsWithSegments(excluded, StringComparison.OrdinalIgnoreCase));
    }, 
    //then use spa services
    then =>
    {
        //use static files from Client build folder in production
        if (builder.Environment.IsProduction())
        {
            then.UseSpaStaticFiles();
        }

        then.UseSpa(cfg =>
        {
            //while development start React app
            if (builder.Environment.IsDevelopment())
            {
                cfg.Options.SourcePath = "Client";
                cfg.UseReactDevelopmentServer("start");
            }
        });
    }
);


app.Run();
