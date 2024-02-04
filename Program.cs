using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ActiveCity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeAreaFolder("Admin", "/");
    // options.Conventions.AuthorizeFolder("/Private");
    // options.Conventions.AllowAnonymousToPage("/Private/PublicPage");
    // options.Conventions.AllowAnonymousToFolder("/Private/PublicPages");
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
    });

if (builder.Environment.IsDevelopment()) {
    builder.Services.AddDbContext<ActiveCityContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("ActiveCityContext") ?? throw new InvalidOperationException("Connection string 'ActiveCityContext' not found.")));
} else {
    builder.Services.AddDbContext<ActiveCityContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ActiveCityContext")));
}
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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
