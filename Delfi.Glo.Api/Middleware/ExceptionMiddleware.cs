﻿using Delfi.Glo.Api.Exceptions;
using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Web;
using Serilog;
using Serilog.Context;
using System.Net;

namespace Delfi.Glo.Api.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        ILogger<ExceptionMiddleware> nlogger;
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> nlogger) 
        {
            this.nlogger = nlogger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                string errorId = Guid.NewGuid().ToString();
                var errorResult = new ErrorResult
                {
                    Source = exception.TargetSite?.DeclaringType?.FullName,
                    Exception = exception.Message.Trim(),
                    ErrorId = errorId,
                    SupportMessage = string.Format(ErrorMessageConstants.IDROUTEPARAMETER, errorId)
                };
                errorResult.Messages.Add(exception.Message);
                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                        exception = exception.InnerException;
                }
                switch (exception)
                {
                    case CustomException e:
                        errorResult.StatusCode = (int)e.StatusCode;
                        if (e.ErrorMessages is not null) errorResult.Messages = e.ErrorMessages;
                        break;
                    case KeyNotFoundException: errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default: errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var response = context.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = errorResult.StatusCode;
                    await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
                }
                nlogger.LogError(errorResult.Exception);
            }
        }
    }
}
