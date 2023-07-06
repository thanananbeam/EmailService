using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace EmailService.DataAccess.Attributes
{
    public class AttributeException : Exception
    {
        public IActionResult actionResult = new OkResult();

        public HttpStatusCode StatusCode;

        public AttributeException(HttpStatusCode statusCode) 
        {
            this.StatusCode = statusCode;
        }
    }

    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                // Controller
                foreach (ActionableAttribute attribute in context.Controller.GetType().GetCustomAttributes(typeof(ActionableAttribute)).OfType<ActionableAttribute>())
                {
                    attribute.OnActionExecuting(context);
                }

                // Action
                foreach (ActionableAttribute attribute in this.baseActionAttributes(context.ActionDescriptor as ControllerActionDescriptor))
                {
                    attribute.OnActionExecuting(context);
                }
            }
            catch (AttributeException e)
            {

                context.Result = e.actionResult;
                this.ExceptionHandler(context.HttpContext, e);
            }
        }

        public ActionableAttribute[] baseActionAttributes(ControllerActionDescriptor controllerActionDescriptor)
        {
            List<ActionableAttribute> result = new List<ActionableAttribute>();

            if (controllerActionDescriptor != null)
            {
                object[] attributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true);
                foreach (Attribute attribute in attributes)
                {
                    ActionableAttribute baseAttribute = attribute as ActionableAttribute;
                    if (baseAttribute != null )
                    {
                        result.Add(baseAttribute);
                    }
                }
            }

            return result.ToArray();
        }

        protected void ExceptionHandler(
            HttpContext httpContext,
            AttributeException exception)
        {
            
        }
    }
}
