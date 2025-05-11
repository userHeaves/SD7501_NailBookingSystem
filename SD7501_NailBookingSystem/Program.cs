using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SD7501_NailBookingSystem.Data;
using SD7501_NailBookingSystem.DataAccess.Repository;
using SD7501_NailBookingSystem.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using SD7501_NailBookingSystem.Utilities;
using Microsoft.AspNetCore.Identity.UI.Services;
using SD7501_NailBookingSystem.Models;
using Microsoft.Extensions.DependencyInjection;
using SD7501_NailBookingSystem.ContactSupportService;

var builder = WebApplication.CreateBuilder(args);

//Google credentials
builder.Services.Configure<GoogleOAuthSettings>(builder.Configuration.GetSection("Authentication:Google"));

//MailLink for Contact page
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<SD7501_NailBookingSystem.ContactSupportService.IEmailService, SD7501_NailBookingSystem.ContactSupportService.MailsService>();

// Configure Google Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    // Access credentials from configuration (user secrets)
    var googleOptions = builder.Configuration.GetSection("Authentication:Google").Get<GoogleOAuthSettings>();

    options.ClientId = googleOptions.ClientId;
    options.ClientSecret = googleOptions.ClientSecret;

    // Optionally, configure callback path (if needed)
    options.CallbackPath = "/signin-google";
});

// Add services to the container.
builder.Services.AddControllersWithViews(); //Controller views
// Pass the connection string from appsettings.json to DbContext using dependency injection (DI)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

//Adding Razor Page views for identity framework
builder.Services.AddRazorPages();

//Adding scope - for DI
builder.Services.AddScoped<IUnityOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Identity DBcontext framework
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages(); //Razor Pages

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
