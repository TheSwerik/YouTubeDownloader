using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend.Service.Exception.Util;

internal class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not YouTubeDownloaderException youtubeDownloaderException) return;
        context.Result = new ObjectResult(youtubeDownloaderException.Body)
            { StatusCode = youtubeDownloaderException.StatusCode };
        context.ExceptionHandled = true;
    }

    public int Order => int.MaxValue - 10;
}