using Forum.Context;
using Forum.Entities;
using Microsoft.AspNetCore.Diagnostics;
using System.Security.Claims;

namespace Forum.Utilities
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GlobalExceptionHandler(IServiceScopeFactory scopeFactory, IHttpContextAccessor httpContextAccessor)
        {
            _scopeFactory = scopeFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                ExceptionLog exceptionLog = new()
                {
                    RequestPath = httpContext.Request?.Path,
                    Source = exception.Source,
                    Message = exception.Message,
                    StackTrace = exception.StackTrace,
                    InnerMessage = exception.InnerException?.Message,
                    InnerStackTrace = exception.InnerException?.StackTrace,
                    UserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                context.ExceptionLogs.Add(exceptionLog);

                try
                {
                    await context.SaveChangesAsync(cancellationToken);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }
    }
}
