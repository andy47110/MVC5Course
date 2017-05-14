using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class SharedViewBagAttribute : ActionFilterAttribute
    {
        public string MyProperty { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "ActionFilter 應用";
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}