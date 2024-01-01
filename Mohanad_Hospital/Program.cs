using Hospital.DataAccess.Data;
using Hospital.DataAccess.Repository;
using Hospital.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Hospital.Utility;
using Microsoft.Extensions.Options;
using Stripe;
using Microsoft.AspNetCore.Localization;
using static WebProje.Services.LanguageService;
using System.Globalization;
using System.Reflection;
using WebProje.Services;
var builder = WebApplication.CreateBuilder(args);

#region
builder.Services.AddSingleton<LanguageService>();
// Configure Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Configure MVC with Localization and DataAnnotationsLocalization
builder.Services.AddMvc()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    {
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
        return factory.Create(nameof(SharedResource), assemblyName.Name);
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{

    var supportedCultures = new List<CultureInfo>
    {
    new CultureInfo("en-US"),
    new CultureInfo("tr-TR"),

    };
    options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});



#endregion


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<StripeUtility>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.Password.RequiredLength = 1;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false; })
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddScoped<IUnitOfWork, UintOfWork>();

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
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseRouting();
app.UseAuthentication(); ;
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
 pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();