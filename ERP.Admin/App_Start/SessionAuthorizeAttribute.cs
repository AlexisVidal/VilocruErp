using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Admin.App_Start
{
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        string produccionurl = System.Configuration.ConfigurationManager.AppSettings["UrlProduccion"];
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                return httpContext.Session["IdUsuario"] != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string loginurl = "/" + produccionurl + "/account/login";
            filterContext.Result = new RedirectResult(loginurl);       /*PRODUCTION MODE*/
        }
    }
}