
using KedaiOnline.Domain.Exceptions;

namespace KedaiOnline.API.Middlewares
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);

            }

            catch (NotFoundException notfound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfound.Message);

                logger.LogWarning(notfound.Message);
            }

            catch (ForbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access forbidded");
            }

            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500; // Internal Server Error
                await context.Response.WriteAsync("Something went wrong");
            }
       
        }
    }
}
