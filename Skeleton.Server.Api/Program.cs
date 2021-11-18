using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Skeleton.Server.Infrastructure;
using Skeleton.Shared.Domain.IdentityEntity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
    .AddOData(cfg => cfg.AddRouteComponents("odata", EdmModelBuilder.GetEdmModel())
        .Select()
        .OrderBy()
        .Filter()
        .Expand()
        .Count()
        .SetMaxTop(1000)
        .TimeZone = TimeZoneInfo.Utc
    );

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddDbContext<AppDbContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("Skeleton"));
});

builder.Services.AddIdentity<Uzivatel, Role>(cfg =>
{
    cfg.Password.RequireDigit = false;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Password.RequireUppercase = false;
    cfg.Password.RequiredLength = 4;

    cfg.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
    opt.Cookie.HttpOnly = true;

    opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    opt.SlidingExpiration = true;

    opt.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };

    opt.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

builder.Services.AddCors(stp =>
{
    stp.AddPolicy("cors", cfg =>
    {
        cfg.WithOrigins("http://localhost:7059").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.DocInclusionPredicate((name, api) => api.HttpMethod != null);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("cors");

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();


await app.RunAsync();
