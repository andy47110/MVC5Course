using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class LocalOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //只要不是Local連上的，就指定做特定事件。Ex: Debug Action使用
            if (!filterContext.RequestContext.HttpContext.Request.IsLocal)
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}