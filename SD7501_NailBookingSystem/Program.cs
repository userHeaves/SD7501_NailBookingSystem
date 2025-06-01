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
using Stripe;

var builder = WebApplication.CreateBuilder(args);

//MailLink for Contact page
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<SD7501_NailBookingSystem.ContactSupportService.IEmailService, SD7501_NailBookingSystem.ContactSupportService.MailsService>();

//Stripe
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

// Configure Google Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
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

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Adding Razor Page views for identity framework
builder.Services.AddRazorPages();

//Adding scope - for DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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

//Stripes - middleware -- fetches secretkey and assign configuration
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseRouting();

// Identity DBcontext framework
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages(); //Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
