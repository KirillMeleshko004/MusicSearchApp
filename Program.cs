using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using MusicSearchApp.Models.DB;
using System.Text;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.Services;

var builder = WebApplication.CreateBuilder(args);

#region Configure services

//Receiving connection string from appsettings.json
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            
            ValidIssuer = builder.Configuration["jwt:Issuer"],
            ValidAudience = builder.Configuration["jwt:Audience"],
            IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"]!)),
        };
    });


builder.Services.AddControllersWithViews();

//Configure root path for production client app
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "Client/build";
});

#region Configure custom services

builder.Services.AddSingleton<IAuthTokenGenerator, JWTTokenGenerator>();

#endregion

#endregion

var app = builder.Build();

#region Configure application

app.UseSpaStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

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

#endregion

app.Run();