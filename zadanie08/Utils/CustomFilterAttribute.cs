using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zadanie08.Utils
{
    public class CustomFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            
           var ipAddress = context.HttpContext.Connection.RemoteIpAddress;

            ipAddress = System.Net.Dns.GetHostEntry(ipAddress).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            ipAddress.ToString();

            var result = context.Result;

            if(result is PageResult)
            {
                var page = ((PageResult)result);
                page.ViewData["filterMessage"] = "Your IP address is: " + ipAddress;
            }

            await next.Invoke();

        }
    }
}
