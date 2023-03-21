using miniOdev.Helpers;

namespace miniOdev.Middlewares
{
    public class QuartzStopStartMiddleware
    {
        RequestDelegate _next;
        ServiceProvider _builder;
        public QuartzStopStartMiddleware(RequestDelegate next, ServiceProvider builder)
        {
            _next = next;
            _builder = builder;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);

            if (context.Request.Path == "/Home/JobPage" && context.Request.Method == "POST" )
            {
                SchedulerHelper.ResetJob(_builder);
                Console.WriteLine("post jobpage");
            }
        }
    }
}
