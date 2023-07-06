using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace EmailService.DataAccess.Attributes
{
    public class ActionableAttribute : Attribute
    {

        virtual public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        virtual public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }
    }
}
