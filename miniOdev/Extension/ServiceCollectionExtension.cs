using DOMAIN.Context;
using DOMAIN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace miniOdev.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection IdentityServerAyarlari(this IServiceCollection services)
        {
            services.AddDbContext<SqlDbContext>(opt => opt.UseSqlServer(@"server=94.73.151.19;Database=u0800400_dbOdev;User Id=u0800400_usOdev;Password=MiniOdev34.....;"));

            services.AddIdentity<CustomUser, IdentityRole>()
                       .AddEntityFrameworkStores<SqlDbContext>() // Hangi Database ile calisacagini belirtiyoruz
                       .AddDefaultTokenProviders(); // email confirm yada parola degisikligi esnasinda uretilecek olan token degerini verir.

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true; // Sifre icerisinde rakam olsun mu ?
                options.Password.RequireLowercase = true; // Kucuk harf olsun mu ?
                options.Password.RequiredLength = 6; // Sifre kac karakterli olsun ?
                options.Password.RequireNonAlphanumeric = true;
                options.Lockout.MaxFailedAccessAttempts = 5; // Kac kere yanlis girebilir
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10); // 10 dakika boyunca yeni giris yapamaz

                options.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöpqrsştuüvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSTUÜVWXYZ0123456789-._@+";

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;



            });


            return services;
        }

        public static IServiceCollection CookieAyarlari(this IServiceCollection services)
        {

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login"; // Default Login Ekrani 
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;   //Default time 20 dakika. 20 dk. bir şey yapmazsa üye atılır

                options.Cookie.HttpOnly = true; // Guvenlik ile ilgili . Tarayicimizdaki diger scriptler cookie'yi okuyamasin.
                options.Cookie.Name = "Bakirkoy.BilgeAdam.Cookie";
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict; // Bizim tarayicimiz disindaki kullanimi engeller
                options.ExpireTimeSpan = TimeSpan.FromMinutes(40); // Cookie nin gecerlilik suresi

            });

            return services;
        }
    }
}
