using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net;
using VoiceAcademyWEB.Business;
using VoiceAcademyWEB.Models;
using System.Configuration;
using Microsoft.Extensions.Caching.Memory;
using VoiceAcademyWEB.Utility;
using VoiceAcademyWEB.Business.Interface;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(configuration);
builder.Services.AddScoped<IAuthorizationProvider, AuthorizationProvider>();
builder.Services.AddScoped<IUsersProvider, UsersProvider>();
builder.Services.AddScoped<IPlayListProvider, PlayListProvider>();
builder.Services.AddScoped<IChaptersProvider, ChaptersProvider>();
builder.Services.AddScoped<IDegreeProvider, DegreeProvider>();
builder.Services.AddScoped<IPodcastChannelProvider, PodcastChannerlProvider>();
builder.Services.AddScoped<TokenAuthorizationFilter>();
builder.Services.AddMemoryCache();

var appSettings = builder.Configuration.GetSection("ApiSettings");
builder.Services.AddHttpClient("ApiHttpClient", client =>
{
    client.BaseAddress = new Uri(appSettings.GetValue<string>("BaseAddress"));
});

builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
