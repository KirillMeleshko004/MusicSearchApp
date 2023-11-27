using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using MusicSearchApp.Models.DB;
using System.Text;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.Services;
using MusicSearchApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region Configure services

//Receiving connection string from appsettings.json
string connection = builder.Configuration.GetConnectionString("DevConnection")!;
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

//Configure Identity Services
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;
    })
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
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

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

//Configure root path for production client app
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "Client/build";
});

#region Configure custom services

builder.Services.AddSingleton<IAuthTokenGenerator, JWTTokenGenerator>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddScoped<ProfileEditService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<AlbumUploadingService>();
builder.Services.AddScoped<MusicPlayService>();
builder.Services.AddScoped<RequestService>();
builder.Services.AddScoped<MusicControlService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<RequestService>();
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<NewsService>();
builder.Services.AddScoped<SubscriptionService>();

#endregion

#endregion

var app = builder.Build();

// app.Use(async (context, next) =>
// {
//     System.Console.WriteLine(context.Request.Path);
//     System.Console.WriteLine(context.Request.PathBase);
//     await next();
// });

#region Configure application

app.UseAuthentication();

//Authenticated, but not authorized HERE

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(
        Directory.GetCurrentDirectory(),
        "Data")),
});

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