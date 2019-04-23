using Microsoft.AspNet.Identity.EntityFramework;
using Sales_App.Controllers;
using Sales_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Sales_App.Filters
{
    public class VerifySession : ActionFilterAttribute, IAuthenticationFilter
    {
      /**  public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                if (filterContext.Controller is AccountController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Account/Login");
                }
            }


            base.OnActionExecuting(filterContext);
            
        }*/

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if(user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

          
        }
    }
}