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

        [LocalOnly]
        public ActionResult Debug()
        {
            return View();
        }

        [SharedViewBag]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            throw new ArgumentException("Error Handled!!");
            //return View();
        }

        [LocalOnly]
        [SharedViewBag(MyProperty = "")]
        /// <summary>
        /// 需要沒有Layout的View
        /// 可以寫PartialView即可(AJAX常用)
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialAbout()
        {
            ViewBag.title = "PartialAbout";
            //ViewBag.Message = "Your application description page.";

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

        public ActionResult VT()
        {
            ViewBag.IsEnabled = true;
            return View();
        }

        public ActionResult RazorTest()
        {
            int[] data = new int[] { 1, 2, 3, 4, 5 };

            return PartialView(data);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}