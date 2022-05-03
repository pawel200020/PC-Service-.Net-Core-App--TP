using Authn.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//depndecy injection
Microsoft.Extensions.Configuration.ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllersWithViews();
Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/Denied";
    options.Events = new CookieAuthenticationEvents()
    
    {
        OnSigningIn = async context =>
        {
            //dodanie dla boba Admina w momencie jego logowania
            var principal = context.Principal;
            if(principal.HasClaim(c=> c.Type == ClaimTypes.NameIdentifier))
            {
                if(principal.Claims.FirstOrDefault(c=>c.Type== ClaimTypes.NameIdentifier).Value == "bob")
                {
                    var claimIdentity = principal.Identity as ClaimsIdentity;
                    claimIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                }
            }
            await Task.CompletedTask;
        },
        OnSignedIn = async context =>
        {
            await Task.CompletedTask;
        },
        OnValidatePrincipal = async context =>
        {
            await Task.CompletedTask;
        }
    };
});

var app = builder.Build();
//rest 
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
