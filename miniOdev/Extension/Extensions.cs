using miniOdev.Middlewares;

namespace miniOdev.Extension
{
    static public class Extensions
    {
        public static IApplicationBuilder UseQuartzStopStart(this IApplicationBuilder applicationBuilder, ServiceProvider serviceProvider)
        {
            return applicationBuilder.UseMiddleware<QuartzStopStartMiddleware>(serviceProvider);
        }

        public static IApplicationBuilder UseIpAddress(this IApplicationBuilder applicationBuilder, ServiceProvider serviceProvider)
        {
            return applicationBuilder.UseMiddleware<IpAddressMiddleware>(serviceProvider);
        }     
    }
}
