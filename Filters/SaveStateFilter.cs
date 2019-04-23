using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Lab_4.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Lab_4.Filters
{
    public class SaveStateFilter : Attribute, IActionFilter
    {
        string _key;


        public SaveStateFilter(string key)
        {
            _key = key;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState != null && context.ModelState.Count > 0)
            {
                Dictionary<string, string> pairs = new Dictionary<string, string>();
                if (context.HttpContext.Session.Keys.Contains(_key))
                    pairs = context.HttpContext.Session.Get<Dictionary<string, string>>(_key);
                foreach (var item in context.ModelState)
                {
                    if (pairs.ContainsKey(item.Key)) pairs.Remove(item.Key);
                    pairs.Add(item.Key, item.Value.AttemptedValue);
                }
                context.HttpContext.Session.Set(_key, pairs);
            }
        }
    }
}