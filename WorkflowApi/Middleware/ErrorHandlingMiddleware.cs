using Microsoft.AspNetCore.Mvc;
using System.Text;
using WorkflowApi.Exceptions;
using WorkflowApi.Models;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        //var requestReader = new StreamReader(context.Request.Body);
        //var requestContent = requestReader.ReadToEndAsync();
        //_logger.LogInformation($"Request Body: {requestContent}");
        //_logger.LogInformation(context.Response.StatusCode.ToString());
        //var ResponseReader = new StreamReader(context.Response.Body);
        //var ResponseContent = await ResponseReader.ReadToEndAsync();
        //_logger.LogInformation($"Response Body: {ResponseContent}");

        try
        {
            
            await next.Invoke(context);
            
        }
        catch (BadRequestException badRequestException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;
            var details= new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = badRequestException.Message
            };
            await context.Response.WriteAsync(details.Serialize());
        }

    }
}