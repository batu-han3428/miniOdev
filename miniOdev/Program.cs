using BL.Concrete;
using BL.Models;
using DAL.Concrete;
using DAL.Models;
using MAP;
using miniOdev.Middlewares;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => {
    policy.WithOrigins(new[] { "https://localhost:7271/", "https://localhost:3000", "https://172.34.1.78:3000", "https://localhost:3001", "http://127.0.0.1:5500" })
            .AllowAnyHeader()
            .AllowAnyMethod()
                .AllowCredentials();
}));

builder.Services.AddScoped<IVeriGirisiServices, VeriGirisiServices>();
builder.Services.AddScoped<IVeriGirisiRepository, VeriGirisiRepository>();


builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new("tr-TR");

    CultureInfo[] cultures = new CultureInfo[]
    {
        new("tr-TR"),
        new("en-US"),
        new("fr-FR")
    };

    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

builder.Services.AddScoped<RequestLocalizationCookiesMiddleware>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

app.UseRequestLocalization();
app.UseRequestLocalizationCookies();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
