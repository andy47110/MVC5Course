using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //return View("About");
            //return View("Index","_Layout");
            return View();
        }

        public ActionResult Unknow()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        /// <summary>
        /// 需要沒有Layout的View
        /// 可以寫PartialView即可(AJAX常用)
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialAbout()
        {
            ViewBag.title = "PartialAbout";
            ViewBag.Message = "Your application description page.";

            if (Request.IsAjaxRequest())
            {
                //如果是AJAX來的就用PartialView
                return PartialView("About");
            }
            else
            {
                return View("About");
            }
        }

        public ActionResult SuccessRedirect()
        {
            return PartialView("SuccessRedirect", "/");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetFile()
        {
            return File(Server.MapPath( "~/Content/Koala.jpg"), "image/png", "Koala.png");
        }

        public ActionResult GetJson()
        {
            //關閉延遲載入
            db.Configuration.LazyLoadingEnabled = false;

            return Json(db.Product.Take(10), JsonRequestBehavior.AllowGet);
        }
    }
}