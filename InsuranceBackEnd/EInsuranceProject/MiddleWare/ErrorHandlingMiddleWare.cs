using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using System.Net;
using System.Text.Json;

namespace EInsuranceProject.MiddleWare
{
    public class ErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(DataNotFoundExcpetion ex)
            {
                await HandleException(context,ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleException(context, (int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        private static Task HandleException(HttpContext context,int code ,string message)
        {

            var result = JsonSerializer.Serialize(new ErrorDetails()
            {
                StatusCode = code,
                Message = message,
            });
            context.Response.StatusCode = code;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
