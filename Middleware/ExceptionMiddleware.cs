using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate Next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch
            {
                await HandleExceptionAsync(context);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new { Message = "Ocorreu um erro inesperado! Tente novamente mais tarde" });
        }
    }
}
