using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace EmailService.DataAccess.Attributes
{
    

    [Flags]
    public enum AuthorizationType
    {
        None = 0,
        Admin = 1,
    };


    public class AuthorizationAttribute : ActionableAttribute
    {
        

        public AuthorizationType authorizationType = AuthorizationType.None;

        public AuthorizationAttribute(AuthorizationType authorizationType)
        {
            this.authorizationType = authorizationType;
        }

        override public void OnActionExecuting(ActionExecutingContext context)
        {
            // Validate
            if (this.authorizationType != AuthorizationType.None)
            {
                try
                {
                    context.HttpContext.Items.Add("UserInfo", JWT.ValidateToken(context.HttpContext ));
                }
                catch (Exception e)
                {
                    //if (e.Message == nameof(HttpStatusCode.Unauthorized))
                    //{
                    //    throw new AttributeException(HttpStatusCode.Unauthorized)
                    //    {
                    //        actionResult = new UnauthorizedResult()
                    //    };
                    //}
                    //else if (e.Message == nameof(HttpStatusCode.RequestTimeout)) 
                    //{
                    //    throw new AttributeException(HttpStatusCode.RequestTimeout)
                    //    {
                    //        actionResult = new JsonResult(new { 
                    //            status = (int)HttpStatusCode.RequestTimeout,
                    //        })
                    //    };
                    //}
                    //else
                    //{
                    //    throw new AttributeException(HttpStatusCode.BadRequest)
                    //    {
                    //        actionResult = new BadRequestResult()
                    //    };
                    //}

                    throw new AttributeException(HttpStatusCode.Unauthorized)
                    {
                        actionResult = new UnauthorizedResult()
                    };
                }
            }
        }

        override public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
