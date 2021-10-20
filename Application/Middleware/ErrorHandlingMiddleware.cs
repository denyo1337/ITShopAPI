using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException notfound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfound.Message);
            }
            catch(EmptyListException emptyList)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(emptyList.Message);
            }
            catch(BannedAccountException banned)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(banned.Message);
            }
            catch(BadFormatException format)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(format.Message);
            }
            catch(NickNameAlreadyTakenException taken)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(taken.Message);
            }
            catch(WrongPasswordException wrongPassword)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(wrongPassword.Message);
            }catch(OutOfStoreException outofStore)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(outofStore.Message);
            }
            catch(BadRequestException badRequest)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(badRequest.Message);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
