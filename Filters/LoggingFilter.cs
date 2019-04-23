using System;
using System.IO;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lab_4.Filters
{
    public class LoggingFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            using (StreamWriter sw = File.AppendText("logger.txt"))
            {
                sw.WriteLine(context.HttpContext.Request.Path.ToString());
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}