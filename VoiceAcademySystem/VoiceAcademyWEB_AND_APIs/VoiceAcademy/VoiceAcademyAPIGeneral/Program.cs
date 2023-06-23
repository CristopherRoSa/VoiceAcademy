using VoiceAcademyAPI.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VoiceAcademyAPI.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoiceAcademyAPI.Business.Interface;
using VoiceAcademyAPIGeneral.Business;
using VoiceAcademyAPIGeneral.Business.Interface;
using VoiceAcademyAPIGeneral.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
var key = configuration["JWT:Key"];
var cadConn = configuration.GetConnectionString("mysql");
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
    };
});
/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("roleType", "admin"));
    options.AddPolicy("User", policy => policy.RequireClaim("roleType", "userGeneral"));
    options.AddPolicy("UvUser", policy => policy.RequireClaim("roleType", "UvUser"));
});*/

builder.Services.AddDbContext<VoiceacademydbContext>(options =>
                options.UseMySql(
                    cadConn, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"
                )));
builder.Services.AddScoped<IUsersProvider, UsersProvider>();
builder.Services.AddScoped<IPlayListsProvider, PlayListProvider>();
builder.Services.AddScoped<IChaptersProvider, ChaptersProvider>();
builder.Services.AddScoped<IPodcastProvider, PodcastProvider>();
builder.Services.AddScoped<IDegreeProvider, DegreeProvider>();
builder.Services.AddScoped<IRegionProvider, RegionProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    // Obtener los encabezados de la solicitud
    var headers = context.Request.Headers;

    // Recorrer los encabezados y mostrarlos en la consola
    foreach (var (headerName, headerValues) in headers)
    {
        Console.WriteLine($"{headerName}: {string.Join(", ", headerValues)}");
    }

    await next.Invoke();
});

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
