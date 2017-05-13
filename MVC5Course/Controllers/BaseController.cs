using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModel;

namespace MVC5Course.Controllers
{
    public abstract class BaseController : Controller
    {
        protected FabricsEntities db = new FabricsEntities();

        /// <summary>
        /// Debug Action
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 設計給所有Controller可以使用
        /// </remarks>
        public ActionResult Debug()
        {
            return Content("Hello World");
        }

        /// <summary>
        /// 對應不存在的Action 導回首頁
        /// 一般還是建議 找不到就顯示404
        /// </summary>
        /// <param name="actionName"></param>
        //protected override void HandleUnknownAction(string actionName)
        //{
        //    this.RedirectToAction("Index","Home").ExecuteResult(this.ControllerContext);
        //}
    }
}