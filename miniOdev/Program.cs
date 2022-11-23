using BL.Concrete;
using BL.Models;
using DAL.Concrete;
using DAL.Models;
using MAP;
using miniOdev.Extension;
using miniOdev.Middlewares;
using System.Globalization;
using Quartz;
using Quartz.Impl;
using RabbitMQ.Core.Concrete;
using RabbitMQ.Core.Abstract;
using RabbitMQ.Core.Data;
using RabbitMQ.Core.Entities;
using miniOdev.Helpers;

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
builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();
builder.Services.AddScoped<IRabbitMQConfiguration, RabbitMQConfiguration>();
builder.Services.AddScoped<IObjectConvertFormat, ObjectConvertToFormatManager>();
builder.Services.AddScoped<IMailSender, MailSender>();
builder.Services.AddScoped<IDataModel<User>, UsersDataModel>();
builder.Services.AddScoped<ISmtpConfiguration, SmtpConfiguration>();
builder.Services.AddScoped<IPublisherService, PublisherManager>();
builder.Services.AddScoped<IConsumerService, ConsumerManager>();
builder.Services.AddScoped<IJobServices, JobServices>();
builder.Services.AddScoped<IJobRepository, JobRepository>();

builder.Services.AddQuartz(q =>
{
    // Joblarýmý oluþturduðum servisi baþlatýyorum
    SchedulerHelper.ZamanlayiciMail();
    q.UseMicrosoftDependencyInjectionJobFactory();
});
// ASP.NET Core hosting
builder.Services.AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});



builder.Services.IdentityServerAyarlari();
builder.Services.CookieAyarlari();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
