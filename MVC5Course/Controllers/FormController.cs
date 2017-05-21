using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class FormController : Controller
    {
        private FabricsEntities db = new FabricsEntities();
        // GET: Form
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(db.Product.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            var product = db.Product.Find(id);

            //tyrupdateModel => 延遲載入
            if (TryUpdateModel(product, includeProperties: new string[] { "ProductName)" }))
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}