using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using MusicSearchApp.Models.DB;
using System.Text;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.Services;
using System.Net;

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

app.UseStaticFiles();
app.UseSpaStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    {               
        //disable index.html caching    
        if (context.File.Name == "index.html" ) {
            context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
            context.Context.Response.Headers.Add("Expires", "-1");
        }
    }
});
app.UseAuthentication();

app.UseStatusCodePages(async context => 
    {
        var request = context.HttpContext.Request;
        var response = context.HttpContext.Response;

        if (response.StatusCode == (int)HttpStatusCode.Unauthorized)   
        {
            response.Redirect("/login");
        }
    }
);

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
            cfg.Options.SourcePath = "Client";
            //while development start React app
            if (builder.Environment.IsDevelopment())
            {
                cfg.UseProxyToSpaDevelopmentServer("http://localhost:8081/");
                cfg.UseReactDevelopmentServer("start");

                //disable index.html caching
                cfg.Options.DefaultPageStaticFileOptions = new StaticFileOptions()
                {
                    OnPrepareResponse = context =>
                    {                   
                        if (context.File.Name == "index.html" ) {
                            context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                            context.Context.Response.Headers.Add("Expires", "-1");
                        }
                    }
                };
            }
        });
    }
);


#endregion

app.Run();