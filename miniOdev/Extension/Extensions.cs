using miniOdev.Middlewares;

namespace miniOdev.Extension
{
    static public class Extensions
    {
        public static IApplicationBuilder UseQuartzStopStart(this IApplicationBuilder applicationBuilder, ServiceProvider serviceProvider)
        {
            return applicationBuilder.UseMiddleware<QuartzStopStartMiddleware>(serviceProvider);
        }
    }
}
