using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nac.Dal.Configuration;
using Nac.Dal.EfStructures;
using Nac.Dal.Initialization;
using Nac.Models.System;
using Nac.Services.Logging.Configuration;

const string CookieScheme = "NacScheme";
const string CookieName = "NacUserCookie";

var builder = WebApplication.CreateBuilder(args);

// configure logging
builder.ConfigureSerilog();
builder.Services.RegisterLoggingInterfaces();

// configure ForwardedHeadersOptions
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// add services to the container.
builder.Services.AddControllersWithViews();

// configure DB connection
var connectionString = builder.Configuration.GetConnectionString(NacConstants.CONNECTION_STRING_KEY);
builder.Services.RegisterDalServices(connectionString!);

var sessionCookieTimeout = builder.Configuration.GetValue<TimeSpan>("SessionCookieTimeout");
if (sessionCookieTimeout < TimeSpan.Parse("00:01:00"))
{
    sessionCookieTimeout = TimeSpan.Parse("2.00:00:00");
}

builder.Services.AddAuthentication(CookieScheme)
    .AddCookie(CookieScheme, options =>
    {
        options.Cookie.Name = CookieName;
        options.AccessDeniedPath = "/user/denied";
        options.LoginPath = "/user/login";
        options.ExpireTimeSpan = sessionCookieTimeout;
        options.SlidingExpiration = true;
    });
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// for custom tags
builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

var app = builder.Build();

app.LogWelcomeMessage();

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //If in development environment, display debug info
    app.UseDeveloperExceptionPage();

    //Initialize the database
    if (app.Configuration.GetValue<bool>(NacConstants.REBUILD_DATA_BASE_KEY))
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<NacDbContext>();
        await SampleDataInitializer.ClearAndReseedDatabaseAsync(dbContext);
    }
    else if (app.Configuration.GetValue<bool>(NacConstants.CLEAR_DATA_BASE_KEY))
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<NacDbContext>();
        await SampleDataInitializer.ClearForWebAppDatabaseAsync(dbContext);
    }
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UsePathBase(app.Configuration.GetValue<string>("ApplicationPathBase"));
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
