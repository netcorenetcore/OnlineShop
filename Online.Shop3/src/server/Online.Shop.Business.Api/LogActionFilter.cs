using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Nest;

namespace Online.Shop.Business.Api
{
    public class LogActionFilter : IAsyncActionFilter
    {
        private readonly IElasticClient _elasticClient;
        
        public LogActionFilter(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logDto = new ActionLogDto();
            logDto.RequestTime = DateTime.Now;
            logDto.Id = Guid.NewGuid();
            logDto.ActionName = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            logDto.ControllerName = ((ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            logDto.RequestUrl = context.HttpContext.Request.GetDisplayUrl();
            logDto.TraceIdentifier = context.HttpContext.TraceIdentifier;
            logDto.IpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            var executedContext = await next();
            logDto.HttpStatus = executedContext.HttpContext.Response.StatusCode;
            logDto.ExceptionMessage = executedContext.Exception?.Message;
            logDto.ResponseTime = DateTime.Now;
            logDto.RequestDuration = (logDto.ResponseTime - logDto.RequestTime).TotalMilliseconds;
            var result = await _elasticClient.IndexAsync(logDto, descriptor => descriptor.Index("vaka-action-log"));
        }
    }

    public class ActionLogDto
    {
        public Guid Id { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public int HttpStatus { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string RequestUrl { get; set; }
        public double RequestDuration { get; set; }
        public string TraceIdentifier { get; set; }
        public string ExceptionMessage { get; set; }

        public string IpAddress { get; set; }
    }

}
